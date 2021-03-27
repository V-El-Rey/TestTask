using System.Collections.Generic;
using Config;
using Player;
using Pool;
using UnityEngine;

namespace MainControllers
{
    public class MainGameController : MonoBehaviour
    {
        public GameConfig config;
        public Camera mainCamera;
        public List<ObjectPoolItem> gameObjectsToPool;

        private PlayerController _playerController;
        private ObjectPool _objectPool;

        private void Start()
        { 
            _objectPool = new ObjectPool();
            var playerModel = new PlayerModel(config);
           
            var playerView = FindObjectOfType<PlayerView>();
            _playerController = new PlayerController(mainCamera, playerModel, playerView);
            _playerController.StartExecute();
            
            _objectPool.Initialize(gameObjectsToPool);

        }

        private void Update()
        {
            _playerController.UpdateExecute();
        }
    }
}
