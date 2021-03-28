using Config;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainControllers
{
    public class MainMenuController : MonoBehaviour
    {

        #region PrivateData

        [SerializeField] private GameConfig config;
        [SerializeField] private Dropdown dropdown;
        [SerializeField] private InputField inputField;
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        #endregion


        #region Methods
        
        private void ConfigFiller(InputField input)
        {
            if (input.text.Length > 0)
            {
                switch (dropdown.captionText.text)
                {
                    case ("Player Health"):
                        float.TryParse(input.text, out config.playerHealth);
                        break;
                    case ("Player Power"):
                        float.TryParse(input.text, out config.playerPower);
                        break;
                    case ("Enemy Health"):
                        float.TryParse(input.text, out config.enemyHealth);
                        break;
                    case ("Enemy Speed"):
                        float.TryParse(input.text, out config.enemySpeed);
                        break;
                    case ("Enemy Power"):
                        float.TryParse(input.text, out config.enemyPower);
                        break;
                    case ("Enemy Spawn Ratio"):
                        float.TryParse(input.text, out config.enemySpawnRatio);
                        break;
                    case ("Points To Win"):
                        float.TryParse(input.text, out config.pointsToWin);
                        break;
                }
            }
        }
        
        
        private void LoadLevel()
        {
            SceneManager.LoadScene(1);
        }

        private void Quit()
        {
            Application.Quit();
        }
        
        #endregion
        

        #region UnityMethods

        private void Start()
        {
            config.ResetValues();
            inputField.onEndEdit.AddListener(delegate { ConfigFiller(inputField); });
            startButton.onClick.AddListener(LoadLevel);
            exitButton.onClick.AddListener(Quit);
        }
        

        #endregion
        
    }
}
