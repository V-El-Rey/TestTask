using Interface;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class EndGameManager
    {
        
        #region PrivateData
        
        private Canvas _canvas;
        private Text _winText;
        private Text _loseText;

        #endregion
        
        
        public EndGameManager(Canvas canvas, Text winText, Text loseText)
        {
            _canvas = canvas;
            _winText = winText;
            _loseText = loseText;
        }


        #region Methods

        public void CheckWinCondition(IScoreManager controller)
        {
            if (controller.Win)
            {
                Time.timeScale = 0;
                _winText.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
        
        public void CheckLoseCondition(PlayerController controller)
        {
            if (controller.isPlayerDead)
            {
                Time.timeScale = 0;
                _loseText.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
        
        #endregion
        
        
    }
}
