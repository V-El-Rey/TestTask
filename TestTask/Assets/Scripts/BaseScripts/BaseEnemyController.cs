using System.Collections.Generic;
using BombEnemy;
using Config;
using Interface;
using Player;
using Pool;
using UnityEngine;


namespace BaseScripts
{
    public class BaseEnemyController : IControllerStartExecute, IControllerUpdateExecute
    {
        
        #region PrivateData
        
        private readonly List<BaseEnemy> _enemyList = new List<BaseEnemy>();
        private readonly Vector3 _playerPosition;
        protected static float EnemyModelDamage;
        private readonly IModel _model;
        private float _timeBeforeSpawn;
        private readonly float _spawnRatio;

        #endregion
        
        
        public delegate void OnEnemyKilled();

        public event OnEnemyKilled ScoreAPoint;

        protected BaseEnemyController(GameConfig config, IModel model, Vector3 playerPosition)
        {
            _spawnRatio = config.enemySpawnRatio;
            _model = model;
            EnemyModelDamage = _model.Damage;
            _playerPosition = playerPosition;
            _timeBeforeSpawn = 0.5f;
            _timeBeforeSpawn = _timeBeforeSpawn * _spawnRatio;
        }
        
        
        #region Methods
        
        private void Initialize(string tag)
        {
            var enemyList = ObjectPool.GetListFromPool(tag);
            foreach (var item in enemyList)
            {
                var enemyView = item.GetComponent<BaseObjectView>();
                var enemy = new BaseEnemy(_model, enemyView, _playerPosition);
                HelperMethods.GetRandomPosition(item);
                enemyView.IsInactive = true;
                enemyView.SetRandomPosition += HelperMethods.GetRandomPosition;
                _enemyList.Add(enemy);
            }
        }

        public void ControllerStartExecute(string tag)
        {
            Initialize(tag);
            foreach (var item in _enemyList)
            {
                if (item.View.IsInactive)
                {
                    item.Spawn();
                }
            }
        }

        public virtual void ControllerUpdateExecute()
        {
            foreach (var item in _enemyList)
            {
                item.UpdateExecute();
                if (item.View.EnemyHit)
                {
                    PlayerController.DoDamage(ref item.BaseObjectHealth);
                    item.View.EnemyHit = false;
                    item._currentHealth = item.BaseObjectHealth;
                    item.HealthbarUpdate();
                    if (item.BaseObjectHealth <= 0)
                    {
                        item.View.ReturnToPool();
                        ScoreAPoint?.Invoke();
                    }
                }
            }
        }

        public void FixedUpdateExecute()
        {
            foreach (var item in _enemyList)
            { 
                item.FixedUpdateExecute();
                if (item.View.IsInactive)
                { 
                    _timeBeforeSpawn += Time.fixedDeltaTime; 
                    if (_timeBeforeSpawn > _spawnRatio) 
                    { 
                        item.Spawn(); 
                        _timeBeforeSpawn = 0.0f;
                    }
                } 
                if (item.View.EnemyHit)
                {
                    PlayerController.DoDamage(ref item.BaseObjectHealth);
                    item._currentHealth = item.BaseObjectHealth;
                    item.HealthbarUpdate();
                    item.View.EnemyHit = false;
                    if (item.BaseObjectHealth <= 0)
                    {
                        item.View.ReturnToPool();
                        ScoreAPoint?.Invoke();
                    }
                }
            }
        }
        
        #endregion
        
    }
}
