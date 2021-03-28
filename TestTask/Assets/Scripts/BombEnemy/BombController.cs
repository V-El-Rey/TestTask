using BaseScripts;
using Config;
using Interface;
using UnityEngine;

namespace BombEnemy
{
    public class BombController : BaseEnemyController
    {
        public BombController(GameConfig config, BombModel model, Vector3 playerPosition):base(config, model,playerPosition){}

        
        #region Methods
        
        public static void DoBombDamage(IModel model)
        {
            model.Health -= EnemyModelDamage;
        }
        
        #endregion
        
    }
}
