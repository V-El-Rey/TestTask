using Archer;
using BombEnemy;
using Infantry;
using Interface;
using Managers;
using Pool;
using UnityEngine;

namespace Player
{
    public class PlayerController : IStartExecute, IUpdateExecute
    {
        
        #region PrivateData

        public static PlayerModel _model;
        private readonly PlayerView _view;
        private readonly Camera _camera;
                
        private InputManager _inputManager;
        private bool _isShooting;
        private Vector3 _rotateCoordinate;
        private Vector3 _rotateDirection;
        private float _fireRate;
        
        #endregion

        public bool isPlayerDead;
        
        
        public PlayerController(Camera camera, PlayerModel model, PlayerView view)
        {
            _camera = camera;
            _model = model;
            _view = view;
            _inputManager = new InputManager(_camera);
            _view.OnBombDamageTaken += GetBombDamage;
            _view.OnBulletDamageTaken += GetArcherDamage;
            InfantryUnit.DoInfantryDamage += GetInfantryDamage;
        }


        #region Methods

        private void RotatePlayer()
        {
            _rotateDirection = _rotateCoordinate - _view.objectTransform.position;
            var rotationAngle = Mathf.Atan2(_rotateDirection.y, _rotateDirection.x) * Mathf.Rad2Deg;
            _view.transform.rotation = Quaternion.Euler(0, 0, rotationAngle + _model.rotationAngleOffset);
        }

        private void Fire()
        {
            var bullet = ObjectPool.GetObjectFromPool("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = _view.gunMuzzle.position;
                bullet.transform.rotation = _view.gunMuzzle.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().AddForce((_model.ShootForce) * _rotateDirection.normalized,
                    ForceMode2D.Impulse);
                
            }
        }

        private void GetBombDamage()
        {
            BombController.DoBombDamage(_model);
        }

        private void GetInfantryDamage()
        {
            InfantryUnitController.DoDamage(_model);
        }

        private void GetArcherDamage()
        {
            ArcherUnitController.DoDamage(_model);
        }

        private void IsPlayerAlive()
        {
            if (_model.Health <= 0)
            {
                isPlayerDead = true;
            }
        }

        public static void DoDamage(ref float model)
        {
            model -= _model.Damage;
        }

        #endregion
        
        
        #region UnityMethods
        
        public void StartExecute()
        {
            _fireRate = _model.RateOfFire;
        }


        public void UpdateExecute()
        {
            Debug.Log(_model.Health);
            _isShooting = _inputManager.IsPlayerShooting();
            _rotateCoordinate = _inputManager.GetMousePosition();
            RotatePlayer();
            _fireRate -= Time.deltaTime;
            if (_isShooting && _fireRate <= 0.0f)
            {
                Fire();
                _fireRate = _model.RateOfFire;
            }
            IsPlayerAlive();
        }

        #endregion
        
        
    }
}
