using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class BaseObjectView : MonoBehaviour
    {
        
        #region Fields

        public Transform objectTransform;
        public Collider2D objectCollider2D;
        public Rigidbody2D objectRigidbody2D;
        public Image healthBar;
        public bool IsInactive;
        public bool EnemyHit;

        #endregion
        
        public delegate void OnReturningToPool(GameObject obj);

        public event OnReturningToPool SetRandomPosition;

        #region Methods
        
        public void ReturnToPool()
        {
            IsInactive = true;
            gameObject.SetActive(false);
            HelperMethods.GetRandomPosition(gameObject);
            EnemyHit = false;
        }
        
        #endregion
        

    }
}
