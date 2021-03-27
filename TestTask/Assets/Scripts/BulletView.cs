using BaseScripts;
using UnityEngine;

public class BulletView : BaseObjectView
{
    private void OnBecameInvisible()
    {
        ReturnToPool();
    }
    
    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
