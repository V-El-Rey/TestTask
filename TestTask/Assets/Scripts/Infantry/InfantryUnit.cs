using BaseScripts;
using Interface;
using UnityEngine;

namespace Infantry
{
    public class InfantryUnit : BaseEnemy
    {
        
        #region PrivateData
        
        private readonly float _attackDistance;
        private Vector3 _distanceToPlayer;
        private float _attackDelay;
        
        #endregion
        
        
        public delegate void UnitOnPosition();

        public static event UnitOnPosition DoInfantryDamage;

        public InfantryUnit(IModel model, BaseObjectView view, Vector3 playerPosition):base(model,view,playerPosition)
        {
            _attackDelay = 3.0f;
            _attackDistance = 1.25f;
        }

        
        #region Methods
        
        
        public override void FixedUpdateExecute()
        {
            MoveToPlayer(_attackDistance);
        }

        public override void UpdateExecute()
        {
            if (_distanceToPlayer.sqrMagnitude <= _attackDistance * _attackDistance)
            {
                View.objectRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
                _attackDelay -= Time.deltaTime;
                if (_attackDelay <= 0.0f)
                {
                    DoInfantryDamage?.Invoke();
                    _attackDelay = 3.0f;
                }
            }
        }
        
        #endregion
    }
}
