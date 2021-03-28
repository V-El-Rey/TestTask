using Config;
using Interface;

namespace Player
{
    public class PlayerModel : IModel
    {

        #region PrivateData

        private const float BaseHealth = 100.0f;
        private const float BasePower = 10.0f;

        #endregion


        #region Fields

        public float RateOfFire = 0.3f;
        public float ShootForce = 30.0f;
        public float rotationAngleOffset = -90;

        #endregion


        #region Properties

        public float Speed { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }

        #endregion

        public PlayerModel(GameConfig config)
        {
            Health = BaseHealth * config.playerHealth;
            Damage = BasePower * config.playerPower;
        }


    }
}
