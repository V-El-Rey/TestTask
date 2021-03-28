using Interface;
using Player;
using UnityEditorInternal;
using UnityEngine;

namespace Infantry
{
    public class InfantryUnit : IUpdateExecute, IFixedUpdateExecute
    {
        private InfantryModel _model;
        public readonly InfantryView _view;
        private Vector3 _playerPosition;
        private float _attackPosition = 1.25f;
        private Vector3 _distanceToPlayer;
        public float InfantryUnitHealth;

        private float attackDelay = 3.0f;

        public delegate void UnitOnPosition();

        public static event UnitOnPosition DoInfantryDamage;

        public InfantryUnit(InfantryModel model, InfantryView view, Vector3 playerPosition)
        {
            _model = model;
            _view = view;
            _playerPosition = playerPosition;
        }
        
        public void Spawn()
        {
            _view.gameObject.SetActive(true);
            InfantryUnitHealth = _model.Health;
            _view.IsInactive = false;
        }
        
        private void MoveToPlayer()
        { 
            _distanceToPlayer = _playerPosition - _view.objectTransform.position;
            var directionToPlayer = _distanceToPlayer.normalized;
            if (_distanceToPlayer.sqrMagnitude > _attackPosition * _attackPosition)
            {
                _view.objectTransform.Translate(directionToPlayer * (Time.fixedDeltaTime * _model.Speed));
            }
        }
        

        public void FixedUpdateExecute()
        {
            MoveToPlayer();
        }

        public void UpdateExecute()
        {
            if (_distanceToPlayer.sqrMagnitude <= _attackPosition * _attackPosition)
            {
                attackDelay -= Time.deltaTime;
                if (attackDelay <= 0.0f)
                {
                    DoInfantryDamage?.Invoke();
                    attackDelay = 3.0f;
                }
            }
        }
    }
}
