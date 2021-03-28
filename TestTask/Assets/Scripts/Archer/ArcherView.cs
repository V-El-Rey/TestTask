using BaseScripts;
using UnityEngine;

namespace Archer
{
    public class ArcherView : BaseObjectView, IEnemy
    {   
        
        
        #region Fields
        
        public Transform gunMuzzle;
        
        #endregion
        
        
        #region Properties
        
        public bool EnemyHit { get; set; }
             
        public bool IsInactive { get; set; }
        
        #endregion
        
        public delegate void OnReturningToPool(GameObject obj);

        public event OnReturningToPool SetRandomPosition;
        
        
        #region UnityMethods
        
        public void OnBecameInvisible()
        {
            
        }
        
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
