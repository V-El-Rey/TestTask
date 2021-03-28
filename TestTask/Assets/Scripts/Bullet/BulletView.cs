using System;
using BaseScripts;
using UnityEngine;

public class BulletView : BaseObjectView
{
    public bool bulletHitTarget;
    private void OnBecameInvisible()
    {
        ReturnToPool();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        ReturnToPool();
        bulletHitTarget = true;
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        bulletHitTarget = false;
    }
}
