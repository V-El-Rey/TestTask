using Interface;
using Managers;
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

        #endregion
        
        
        #region UnityMethods
        
        public void StartExecute()
        {
            _inputManager = new InputManager(_camera);
        }


        public void UpdateExecute()
        {
            _isShooting = _inputManager.IsPlayerShooting();
            _rotateCoordinate = _inputManager.GetMousePosition();
            RotatePlayer();
        }

        #endregion
        
        
    }
}
