using Interface;
using Managers;
using Pool;
using UnityEngine;

namespace Player
{
    public class PlayerController : IStartExecute, IUpdateExecute
    {


        #region PrivateData

        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly Camera _camera;
                
        private InputManager _inputManager;
        private bool _isShooting;
        private Vector3 _rotateCoordinate;
        private Vector3 _rotateDirection;
        private float fireRate;

        #endregion
        
        
        public PlayerController(Camera camera, PlayerModel model, PlayerView view)
        {
            _camera = camera;
            _model = model;
            _view = view;
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
                bullet.GetComponent<Rigidbody2D>().AddForce((_model.playerPower / 2) * _rotateDirection,
                    ForceMode2D.Impulse);
            }
        }

        #endregion
        
        
        #region UnityMethods
        
        public void StartExecute()
        {
            _inputManager = new InputManager(_camera);
            fireRate = PlayerModel.RateOfFire;
        }


        public void UpdateExecute()
        {
            _isShooting = _inputManager.IsPlayerShooting();
            _rotateCoordinate = _inputManager.GetMousePosition();
            RotatePlayer();
            fireRate -= Time.deltaTime;
            if (_isShooting && fireRate <= 0.0f)
            {
                Fire();
                fireRate = PlayerModel.RateOfFire;
            }
        }

        #endregion
        
        
    }
}
