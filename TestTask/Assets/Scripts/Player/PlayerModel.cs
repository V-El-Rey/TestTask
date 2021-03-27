using Config;

namespace Player
{
    public class PlayerModel
    {
        private const float BaseHealth = 100.0f;
        private const float BasePower = 10.0f;

        public float playerHealth;
        public float playerPower;
        
        public PlayerModel(GameConfig config)
        {
            playerHealth = BaseHealth * config.playerHealth;
            playerPower = BasePower * config.playerPower;
        }
    }
}
