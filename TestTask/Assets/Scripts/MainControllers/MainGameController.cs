using System;
using System.Collections.Generic;
using Config;
using Interface;
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
        public BombView bombView;
        public InfantryView infantryView;

        private PlayerController _playerController;
        private ObjectPool _objectPool;

        private BaseEnemy bomb, infantry;

        private void Start()
        { 
            _objectPool = new ObjectPool();
            var playerModel = new PlayerModel(config);
           
            var playerView = FindObjectOfType<PlayerView>();
            _playerController = new PlayerController(mainCamera, playerModel, playerView);
            _playerController.StartExecute();
            IModel bombModel = new BombModel();
            IModel infantryModel = new InfantryModel();

            _objectPool.Initialize(gameObjectsToPool);

            bomb = new BombEnemy(bombModel, bombView);
            infantry = new InfantryEnemy(infantryModel, infantryView);

        }

        private void Update()
        {
            _playerController.UpdateExecute();            
            bomb.Move(1.0f);
            infantry.Move(2.0f);
        }

        private void FixedUpdate()
        {

        }
    }
}
