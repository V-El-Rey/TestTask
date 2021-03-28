using Config;
using Interface;

namespace BombEnemy
{
    public class BombModel : IModel
    {
        private const float BaseSpeed = 1.0f;
        private const float BaseDamage = 5.0f;
        private const float BaseHealth = 20.0f;

        public float Speed;
        public float Damage { get; set; }
        public float Health { get; set; }
    
        public BombModel(GameConfig config)
        {
            Speed = BaseSpeed * config.enemySpeed;
            Damage = BaseDamage * config.enemyPower;
            Health = BaseHealth * config.enemyHealth;
        }
    }
}
