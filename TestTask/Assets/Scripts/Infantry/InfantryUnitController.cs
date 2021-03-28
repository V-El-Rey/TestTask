using System.Collections.Generic;
using BombEnemy;
using Config;
using Interface;
using Player;
using Pool;
using UnityEngine;

namespace Infantry
{
    public class InfantryUnitController : IEnemyController, IStartExecute, IUpdateExecute, IFixedUpdateExecute
    {
        private readonly List<InfantryUnit> _infantryList = new List<InfantryUnit>();
        private readonly Vector3 _playerPosition;
        private static float _infantryDamage;
        private readonly InfantryModel _infantryModel;
        
        
        
        public float TimeBeforeSpawn { get; set; }
        public float SpawnRatio { get; set; }
        
        public delegate void OnInfantryKilled();

        public event OnInfantryKilled ScoreAPoint;

        
        
        public InfantryUnitController(GameConfig config, InfantryModel model, Vector3 playerPosition)
        {
            SpawnRatio = config.enemySpawnRatio;
            _infantryModel = model;
            _infantryDamage = _infantryModel.Damage;
            _playerPosition = playerPosition;
            TimeBeforeSpawn = SpawnRatio;
        }
        
        public void Initialize()
        {
            var infantryList = ObjectPool.GetListFromPool("Infantry");
            foreach (var item in infantryList)
            {
                var unitView = item.GetComponent<InfantryView>();
                var unit = new InfantryUnit(_infantryModel, unitView, _playerPosition);
                GetRandomPosition(item);
                unitView.IsInactive = true;
                unitView.SetRandomPosition += GetRandomPosition;
                _infantryList.Add(unit);
            }
        }

        public void GetRandomPosition(GameObject gameObject)
        {
             var randomPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-8.0f, 8.0f), 0);
             gameObject.transform.position = randomPosition;
        }

        public static void DoDamage(IModel model)
        {
            model.Health -= _infantryDamage;
        }

        public void StartExecute()
        {
            Initialize();
            foreach (var item in _infantryList)
            {
                item.Spawn();
            }
        }

        public void FixedUpdateExecute()
        {
            foreach (var item in _infantryList)
            {
                item.FixedUpdateExecute();
                if (item._view.IsInactive)
                {
                    TimeBeforeSpawn += Time.fixedDeltaTime;
                    if (TimeBeforeSpawn > SpawnRatio)
                    {
                        item.Spawn();
                        TimeBeforeSpawn = 0.0f;
                    }
                }
            }
        }
        

        public void UpdateExecute()
        {
            foreach (var item in _infantryList)
            {
                item.UpdateExecute();
                if (item._view.EnemyHit)
                {
                    PlayerController.DoDamage(ref item.InfantryUnitHealth);
                    item._view.EnemyHit = false;
                    if (item.InfantryUnitHealth <= 0)
                    {
                        item._view.ReturnToPool();
                        ScoreAPoint?.Invoke();
                    }
                }
            }
        }
    }
}
