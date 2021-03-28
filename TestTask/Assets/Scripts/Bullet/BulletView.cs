using System;
using BaseScripts;
using UnityEngine;

public class BulletView : BaseObjectView
{
    
    #region UnityMethods
    
    private void OnBecameInvisible()
    {
        ReturnToPool();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        ReturnToPool();
    }
    
    #endregion

    
    
    #region Methods
    
    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
    
    #endregion
}
