using Config;
using Interface;

namespace Infantry
{
    public class InfantryModel : IModel
    {
        private const float BaseSpeed = 0.2f;
        private const float BaseDamage = 3.0f;
        private const float BaseHealth = 50.0f;
        
        public float Speed;
        public float Health { get; set; }
        public float Damage { get; set; }
        
        public InfantryModel(GameConfig config)
        {
            Speed = BaseSpeed * config.enemySpeed;
            Damage = BaseDamage * config.enemyPower;
            Health = BaseHealth * config.enemyHealth;
        }
    }
}
