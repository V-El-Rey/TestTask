using Interface;
using UnityEngine;

namespace BaseScripts
{
    public class BaseEnemy
    {
        
        #region PrivateData
        
        private Vector3 directionToPlayer;
        private Vector3 distanceToPlayer;
        private float defaultAttackDistance;
        private float _maxHealth;
        
        #endregion
        
        
        #region Fields
        
        public float BaseObjectHealth;
        public float _currentHealth;
        
        #endregion
        
        
        #region Properties
        
        public BaseObjectView View { get; set; }
        private IModel Model { get; set; }
        private Vector3 PlayerPosition { get; set; }
        
        #endregion

        
        public BaseEnemy(IModel model,BaseObjectView view, Vector3 playerPosition)
        {
            Model = model;
            PlayerPosition = playerPosition;
            View = view;
        }

        
        #region Methods
        
        public virtual void MoveToPlayer(float distance)
        {
            distanceToPlayer = PlayerPosition - View.objectTransform.position;
            directionToPlayer = distanceToPlayer.normalized;
            if (directionToPlayer.sqrMagnitude > distance * distance)
            {
                View.objectTransform.Translate(directionToPlayer * (Time.fixedDeltaTime * Model.Speed));
            }
            
        }
        
        public virtual void Spawn()
        {
            View.gameObject.SetActive(true);
            BaseObjectHealth = Model.Health;
            _maxHealth = BaseObjectHealth;
            _currentHealth = _maxHealth;
            HealthbarUpdate();
            View.IsInactive = false;
        }
        
        public virtual void HealthbarUpdate()
        {
            var normalizedFillAmount = _currentHealth / _maxHealth;
            View.healthBar.fillAmount = normalizedFillAmount;
        }

        public virtual void FixedUpdateExecute()
        {
            MoveToPlayer(defaultAttackDistance);
        }

        public virtual void UpdateExecute()
        {
            
        }
        
        
        #endregion
    }
}
