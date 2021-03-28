using BaseScripts;
using UnityEngine;

namespace Archer
{
    public class ArcherView : BaseObjectView, IEnemy
    {        
        public bool EnemyHit { get; set; }
             
        public bool IsInactive { get; set; }

        public Transform gunMuzzle;
        
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
