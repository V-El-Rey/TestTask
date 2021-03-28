using BaseScripts;
using UnityEngine;

namespace Infantry
{
    public class InfantryView : BaseObjectView, IEnemy
    {       
        public bool EnemyHit { get; set; }
        public bool IsInactive { get; set; }
        
        
        public delegate void OnReturningToPool(GameObject obj);

        public event OnReturningToPool SetRandomPosition;
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

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            SetRandomPosition?.Invoke(gameObject);
            IsInactive = true;
            EnemyHit = false;
        }


    }
}
