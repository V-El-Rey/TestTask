using System;
using Interface;
using UnityEngine;

namespace Managers
{
    public class InputManager : IController
    {


        #region PrivateData

        private readonly Camera _camera;

        #endregion
       
        
        public InputManager(Camera camera)
        {
            _camera = camera;
        }
        

        #region Methods

        public bool IsPlayerShooting()
        {
            return Input.GetMouseButton(0);
        }

        public Vector2 GetMousePosition()
        {
            var mousePosition = new Vector3(_camera.ScreenToWorldPoint(Input.mousePosition).x,
                _camera.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
            return mousePosition;
        }

        #endregion
    }
}
