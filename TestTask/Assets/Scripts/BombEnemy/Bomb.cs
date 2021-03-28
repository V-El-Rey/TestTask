using System;
using System.Globalization;
using BaseScripts;
using Interface;
using UnityEngine;

namespace BombEnemy
{
    public class Bomb : BaseEnemy
    {
        
        #region PrivateData
        
        private readonly float _attackDistance;

        #endregion
        
        
        public Bomb(IModel model, BaseObjectView view, Vector3 playerPosition) : base(model, view, playerPosition)
        {
            _attackDistance = 0.0f;
        }

        
        #region Methods
        
        public override void FixedUpdateExecute()
        {
            MoveToPlayer(_attackDistance);
        }
        
        #endregion
    }
}
