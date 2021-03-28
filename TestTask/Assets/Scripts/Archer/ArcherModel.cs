using Config;
using Interface;

namespace Archer
{
    public class ArcherModel : IModel
    {

        #region PrivateData

        private const float BaseSpeed = 0.5f;
        private const float BaseDamage = 1.0f;
        private const float BaseHealth = 35.0f;

        #endregion

        
        #region Fields

        public float ShootingForce = 5.0f;
        
        #endregion
        
        
        #region Properties
        
        public float Speed { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        
        #endregion
        
        
        public ArcherModel(GameConfig config)
        {
            Speed = BaseSpeed * config.enemySpeed;
            Damage = BaseDamage * config.enemyPower;
            Health = BaseHealth * config.enemyHealth;
        }
    }
}
