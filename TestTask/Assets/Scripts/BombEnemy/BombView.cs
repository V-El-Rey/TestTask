using BaseScripts;
using UnityEngine;


public class BombView : BaseObjectView
{

    public delegate void OnReturningToPool(GameObject obj);

    public event OnReturningToPool SetRandomPosition;

    #region UnityMethods

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case ("Player"):
            {
                ReturnToPool();
            }
                break;
            case ("Bullet"):
            {
                EnemyHit = true;

            } break;
        }
        
    }

    public void OnBecameInvisible()
    {
        ReturnToPool();
    }
    
    
    #endregion
    
}
