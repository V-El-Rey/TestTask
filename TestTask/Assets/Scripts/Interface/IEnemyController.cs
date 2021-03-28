using UnityEngine;

namespace Interface
{
    public interface IEnemyController : IController
    {
        void Initialize();
        void GetRandomPosition(GameObject gameObject);
        
        float TimeBeforeSpawn { get; set; }
        
        float SpawnRatio { get; set; }

    }
}
