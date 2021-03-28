using BaseScripts;
using UnityEngine;

namespace Infantry
{
    public class InfantryView : BaseObjectView
    {

        public new delegate void OnReturningToPool(GameObject obj);

        public new event OnReturningToPool SetRandomPosition;

        
        #region UnityMethods
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                EnemyHit = true;
            }
        }

        #endregion
        
        
        #region Methods
        
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            SetRandomPosition?.Invoke(gameObject);
            IsInactive = true;
            EnemyHit = false;
        }

        #endregion

    }
}
