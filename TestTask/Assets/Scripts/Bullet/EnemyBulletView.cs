using BaseScripts;
using UnityEngine;

namespace Bullet
{
    public class EnemyBulletView : BaseObjectView
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
