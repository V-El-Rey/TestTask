using Config;
using Player;
using UnityEngine;

namespace MainControllers
{
    public class MainGameController : MonoBehaviour
    {
        public GameConfig config;
        public Camera mainCamera;

        private PlayerController _playerController;
        private void Start()
        {
            var playerModel = new PlayerModel(config);
            var playerView = FindObjectOfType<PlayerView>();
            _playerController = new PlayerController(mainCamera, playerModel, playerView);
            _playerController.StartExecute();

        }

        private void Update()
        {
            _playerController.UpdateExecute();
        }
    }
}
