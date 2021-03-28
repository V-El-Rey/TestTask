using Interface;
using UnityEngine;

public interface IEnemy : IView
{
    void OnBecameInvisible();
    void OnTriggerEnter2D(Collider2D other);
    void ReturnToPool();
    
    bool EnemyHit { get; set; }
    bool IsInactive { get; set; }
    
}
