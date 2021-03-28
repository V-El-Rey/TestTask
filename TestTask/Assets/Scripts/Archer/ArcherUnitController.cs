using System.Collections.Generic;
using Config;
using Interface;
using Player;
using Pool;
using UnityEngine;

namespace Archer
{
    public class ArcherUnitController : IEnemyController, IStartExecute, IUpdateExecute, IFixedUpdateExecute
    {        
        
        
        #region PrivateData
        
        private readonly List<ArcherUnit> _archerList = new List<ArcherUnit>();
        private readonly Vector3 _playerPosition;
        private static float _archerDamage;
        private readonly ArcherModel _archerModel;
        
        #endregion
        
        
        #region Properties
        
        public float TimeBeforeSpawn { get; set; } 
        public float SpawnRatio { get; set; }
        
        #endregion
        
        public delegate void OnArcherKilled();

        public event OnArcherKilled ScoreAPoint;
        
        public ArcherUnitController(GameConfig config, ArcherModel model, Vector3 playerPosition)
        { 
            SpawnRatio = config.enemySpawnRatio;
            _archerModel = model;
            _archerDamage = _archerModel.Damage;
            _playerPosition = playerPosition;
            TimeBeforeSpawn = SpawnRatio;
        }
        
        #region Methods

        public void Initialize()
        {
            var archerList = ObjectPool.GetListFromPool("Archer");
            foreach (var item in archerList)
            {
                var unitView = item.GetComponent<ArcherView>();
                var unit = new ArcherUnit(_archerModel, unitView, _playerPosition);
                GetRandomPosition(item);
                unitView.IsInactive = true;
                unitView.SetRandomPosition += GetRandomPosition;
                _archerList.Add(unit);
            }
        }
        

        public void GetRandomPosition(GameObject gameObject)
        {
             var randomPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-8.0f, 8.0f), 0);
             gameObject.transform.position = randomPosition;
        }

        public static void DoDamage(IModel model)
        {
            model.Health -= _archerDamage;
        }
        
        public void StartExecute()
        {
            Initialize();
            foreach (var item in _archerList)
            {
                item.Spawn();
            }
        }

        public void UpdateExecute()
        {
            foreach (var item in _archerList)
            {
                item.UpdateExecute();
                if (item.View.EnemyHit)
                {
                    PlayerController.DoDamage(ref item.ArcherUnitHealth);
                    item.View.EnemyHit = false;
                    item._currentHealth = item.ArcherUnitHealth;
                    item.HealthbarUpdate();
                    if (item.ArcherUnitHealth <= 0)
                    {
                        item.View.ReturnToPool();
                        ScoreAPoint?.Invoke();
                    }
                }
            }
        }

        public void FixedUpdateExecute()
        {
            foreach (var item in _archerList)
            {
                item.FixedUpdateExecute();
                if (item.View.IsInactive)
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
        
        #endregion
        
        
    }
}
