using System;
using System.Diagnostics;
using BaseScripts;
using Config;
using Interface;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class BombView : BaseObjectView, IEnemy
{
    public bool EnemyHit { get; set; }
    public bool IsInactive { get; set; }

    public delegate void OnReturningToPool(GameObject obj);

    public event OnReturningToPool SetRandomPosition;



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

    public void ReturnToPool()
    {
        IsInactive = true;
        gameObject.SetActive(false);
        SetRandomPosition?.Invoke(gameObject);
        EnemyHit = false;
    }


}
