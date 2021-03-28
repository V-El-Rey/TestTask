using System;
using Interface;
using UnityEngine;

namespace BombEnemy
{
    public class Bomb : IFixedUpdateExecute, IDisposable
    {
        private BombModel _model;
        public readonly BombView _view;
        private readonly Vector3 _playerPosition;
        public float BombObjectHealth;
    

        public Bomb(BombModel model,BombView view, Vector3 playerPosition)
        {
            _model = model;
            _playerPosition = playerPosition;
            _view = view;
        }

        private void MoveToPlayer()
        {
            var distanceToPlayer = _playerPosition - _view.objectTransform.position;
            var directionToPlayer = distanceToPlayer.normalized;
            _view.objectTransform.Translate(directionToPlayer * (Time.fixedDeltaTime * _model.Speed));
        }

        public void Spawn()
        {
            _view.gameObject.SetActive(true);
            BombObjectHealth = _model.Health;
            _view.IsInactive = false;
        }

        public void FixedUpdateExecute()
        {
            MoveToPlayer();
        }

        public void Dispose()
        {
        
        }
    }
}
