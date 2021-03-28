using Interface;
using Pool;
using UnityEditor;
using UnityEngine;

namespace Archer
{
    public class ArcherUnit : IUpdateExecute, IFixedUpdateExecute
    {
        private Vector3 _distanceToPlayer;
        private ArcherModel _model;
        private float _attackDistance;
        private Vector3 _rotateDirection;
        public readonly ArcherView _view;
        private Vector3 _playerPosition;
        public float ArcherUnitHealth;
        
        private float attackDelay = 2.0f;
        
        
        public ArcherUnit(ArcherModel model, ArcherView view, Vector3 playerPosition)
        {
            _model = model;
            _view = view;
            _playerPosition = playerPosition;
            _attackDistance = 20.0f;
        }
        
        public void Spawn()
        {
            _rotateDirection = Vector3.one;
            _view.gameObject.SetActive(true);
            
            if (_distanceToPlayer.sqrMagnitude < _attackDistance)
            {
                _view.EnemyHit = true;
            }
            ArcherUnitHealth = _model.Health;
            _view.IsInactive = false;
        }
    
        private void MoveToPlayer()
        { 
            _distanceToPlayer = _playerPosition - _view.objectTransform.position;
            var directionToPlayer = _distanceToPlayer.normalized;
            if (_distanceToPlayer.sqrMagnitude > _attackDistance)
            {
                _view.objectTransform.Translate(directionToPlayer * (Time.fixedDeltaTime * _model.Speed));
            }

        }

        private void RotateUnit()
        {
            _rotateDirection = _playerPosition - _view.transform.position;
            var rotationAngle = Mathf.Atan2(_rotateDirection.y, _rotateDirection.x) * Mathf.Rad2Deg;
            _view.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }

        private void Shoot()
        {
            var bullet = ObjectPool.GetObjectFromPool("Enemy Bullet");
            if (bullet != null)
            {
                bullet.transform.position = _view.gunMuzzle.position;
                bullet.transform.rotation = _view.gunMuzzle.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().AddForce(_model.ShootingForce * _rotateDirection,
                    ForceMode2D.Impulse);
            }
        }
    
        public void UpdateExecute()
        {
            if (_distanceToPlayer.sqrMagnitude <=  _attackDistance)
            {
                attackDelay -= Time.deltaTime;
                if (attackDelay <= 0.0f)
                {
                    Shoot();
                    attackDelay = 3.0f;
                }
            }
        }

        public void FixedUpdateExecute()
        {
            MoveToPlayer();
            //RotateUnit();
        }
    }
}
