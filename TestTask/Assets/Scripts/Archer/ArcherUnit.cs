using Interface;
using Pool;
using UnityEditor;
using UnityEngine;

namespace Archer
{
    public class ArcherUnit : IUpdateExecute, IFixedUpdateExecute
    {
        
        #region PrivateData
        
        private readonly Vector3 _playerPosition;
        private readonly float _attackDistance;
        private readonly ArcherModel _model;
        
        private Vector3 _distanceToPlayer;
        private Vector3 _rotateDirection;
        private float _attackDelay = 2.0f;
        private float _maxHealth;
        
        #endregion
        
        
        #region Fields
        
        public readonly ArcherView View;
        public float ArcherUnitHealth;
        public float _currentHealth;
        
        #endregion
        
        public ArcherUnit(ArcherModel model, ArcherView view, Vector3 playerPosition)
        {
            _model = model;
            View = view;
            _playerPosition = playerPosition;
            _attackDistance = 20.0f;
        }
        
        
        #region Methods
        
        public void Spawn()
        {
            _rotateDirection = Vector3.one;
            View.gameObject.SetActive(true);
            ArcherUnitHealth = _model.Health;
            _maxHealth = ArcherUnitHealth;
            _currentHealth = _maxHealth;
            HealthbarUpdate();
            View.IsInactive = false;
        }
    
        private void MoveToPlayer()
        { 
            _distanceToPlayer = _playerPosition - View.objectTransform.position;
            var directionToPlayer = _distanceToPlayer.normalized;
            if (_distanceToPlayer.sqrMagnitude > _attackDistance)
            {
                View.objectTransform.Translate(directionToPlayer * (Time.fixedDeltaTime * _model.Speed));
            }

        }
        
        public void HealthbarUpdate()
        {
            var normalizedFillAmount = _currentHealth / _maxHealth;
            View.healthBar.fillAmount = normalizedFillAmount;
        }
        

        private void Shoot()
        {
            var bullet = ObjectPool.GetObjectFromPool("Enemy Bullet");
            if (bullet != null)
            {
                bullet.transform.position = View.gunMuzzle.position;
                bullet.transform.rotation = View.gunMuzzle.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().AddForce(_model.ShootingForce * _rotateDirection,
                    ForceMode2D.Impulse);
            }
        }
    
        public void UpdateExecute()
        {
            if (_distanceToPlayer.sqrMagnitude <=  _attackDistance)
            {
                _attackDelay -= Time.deltaTime;
                if (_attackDelay <= 0.0f)
                {
                    Shoot();
                    _attackDelay = 3.0f;
                }
            }
        }

        public void FixedUpdateExecute()
        {
            MoveToPlayer();
        }
        
        #endregion
    }
}
