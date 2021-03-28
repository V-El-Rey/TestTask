using System.Collections.Generic;
using Config;
using Interface;
using Managers;
using Player;
using Pool;
using UnityEngine;

namespace BombEnemy
{
    public class BombController : IFixedUpdateExecute,IStartExecute, IUpdateExecute, IEnemyController
    {
        private readonly List<Bomb> _bombList = new List<Bomb>();
        private readonly Vector3 _playerPosition;

        private static float _bombModelDamage;
        public float TimeBeforeSpawn { get; set; }
        public float SpawnRatio { get; set; }
        
        private readonly BombModel _bombModel;

        public delegate void OnBombKilled();

        public event OnBombKilled ScoreAPoint;
        

        public BombController(GameConfig config, BombModel model, Vector3 playerPosition)
        {
            SpawnRatio = config.enemySpawnRatio;
            _bombModel = model;
            _bombModelDamage = _bombModel.Damage;
            _playerPosition = playerPosition;
            TimeBeforeSpawn = 0.5f;
            TimeBeforeSpawn = TimeBeforeSpawn * SpawnRatio;
        }

        public void Initialize()
        {
            var bombObjectList = ObjectPool.GetListFromPool("Bomb");
            foreach (var item in bombObjectList)
            {
                var bombView = item.GetComponent<BombView>();
                var bomb = new Bomb(_bombModel, bombView, _playerPosition);
                GetRandomPosition(item);
                bombView.IsInactive = true;
                bombView.SetRandomPosition += GetRandomPosition;
                _bombList.Add(bomb);
            }
        }

        public void GetRandomPosition(GameObject gameObject)
        {
            var randomPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-8.0f, 8.0f), 0);
            gameObject.transform.position = randomPosition;
        }

        

        public static void DoBombDamage(IModel model)
        {
            model.Health -= _bombModelDamage;
        }

        public void StartExecute()
        {
            Initialize();
            foreach (var item in _bombList)
            {
                if (item._view.IsInactive)
                {
                    item.Spawn();
                }
            }
        }

        public void UpdateExecute()
        {
            
        }

        public void FixedUpdateExecute()
        {  
            foreach (var item in _bombList) 
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
                if (item._view.EnemyHit)
                {
                    PlayerController.DoDamage(ref item.BombObjectHealth);
                    item._view.EnemyHit = false;  
                    if (item.BombObjectHealth <= 0)
                    {
                        item._view.ReturnToPool();
                        ScoreAPoint?.Invoke();
                    }
                } 
            }
        }
    }
}
