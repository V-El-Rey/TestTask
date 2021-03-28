using Config;
using Interface;

namespace Archer
{
    public class ArcherModel : IModel
    {
        private const float BaseSpeed = 0.5f;
        private const float BaseDamage = 1.0f;
        private const float BaseHealth = 35.0f;
        
        public float ShootingForce = 5.0f;
        
        public float Speed;
        public float Health { get; set; }
        
        public float Damage { get; set; }
        
        public ArcherModel(GameConfig config)
        {
            Speed = BaseSpeed * config.enemySpeed;
            Damage = BaseDamage * config.enemyPower;
            Health = BaseHealth * config.enemyHealth;
        }
    }
}
