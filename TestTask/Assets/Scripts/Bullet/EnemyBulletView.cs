using BaseScripts;
using UnityEngine;

namespace Bullet
{
    public class EnemyBulletView : BaseObjectView
    {
        
        #region UnityMethods
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                ReturnToPool();
            }
        }
        
        #endregion

        
        #region Methods
        
        private void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
        
        #endregion
        
        
    }
}
