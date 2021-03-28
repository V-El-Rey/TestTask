using Interface;
using UnityEngine;

public interface IEnemy : IView
{
    
    #region Properties    
    bool EnemyHit { get; set; }
    bool IsInactive { get; set; }
    
    #endregion
    
    #region Methods
    
    void OnBecameInvisible();
    void OnTriggerEnter2D(Collider2D other);
    void ReturnToPool();
    
    #endregion
    

}
