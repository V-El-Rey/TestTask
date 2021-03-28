using System.Collections.Generic;
using Archer;
using BombEnemy;
using Config;
using Infantry;
using Managers;
using Player;
using Pool;
using UnityEngine;
using UnityEngine.UI;

namespace MainControllers
{
    public class MainGameController : MonoBehaviour
    {
        public GameConfig config;
        public Camera mainCamera;
        public List<ObjectPoolItem> gameObjectsToPool;
        public float pointsToWin;
        public Canvas canvas;
        public Text winText;
        public Text loseText;

        private Vector3 _playerPosition;

        private PlayerController _playerController;
        private ObjectPool _objectPool;
        private BombController _bombController;
        private InfantryUnitController _infantryController;
        private ArcherUnitController _archerController;
        private ShowPointsManager _pointsManager;
        private EndGameManager _endGameManager;
        

        private void Start()
        {
            pointsToWin = 10 * config.pointsToWin;
            _objectPool = new ObjectPool();
            _objectPool.Initialize(gameObjectsToPool);
            
            var playerModel = new PlayerModel(config);
            var bombModel = new BombModel(config);
            var infantryModel = new InfantryModel(config);
            var archerModel = new ArcherModel(config);
           
            var playerView = FindObjectOfType<PlayerView>();
            _playerPosition = playerView.objectTransform.position;

            _bombController = new BombController(config, bombModel, _playerPosition);
            _infantryController = new InfantryUnitController(config, infantryModel, _playerPosition);
            _archerController = new ArcherUnitController(config, archerModel, _playerPosition );
            _playerController = new PlayerController(mainCamera, playerModel, playerView);
            _pointsManager = new ShowPointsManager(canvas, pointsToWin);
            _endGameManager = new EndGameManager(canvas, winText, loseText);
            
            
            _playerController.StartExecute();
            _bombController.StartExecute();
            _infantryController.StartExecute();
            _archerController.StartExecute();
            _pointsManager.StartExecute();

            _bombController.ScoreAPoint += _pointsManager.UpdateText;
            _infantryController.ScoreAPoint += _pointsManager.UpdateText;
            _archerController.ScoreAPoint += _pointsManager.UpdateText;

        }

        private void Update()
        {
            _playerController.UpdateExecute();
            _bombController.UpdateExecute();
            _infantryController.UpdateExecute();
            _archerController.UpdateExecute();
            _endGameManager.CheckWinCondition(_pointsManager);
            _endGameManager.CheckLoseCondition(_playerController);

        }

        private void FixedUpdate()
        {    
            
            _infantryController.FixedUpdateExecute();
            _bombController.FixedUpdateExecute();
            _archerController.FixedUpdateExecute();
        }
    }
}
