using BaseScripts;
using Config;
using Interface;
using UnityEngine;

namespace Infantry
{
    public class InfantryUnitController : BaseEnemyController
    {
        
        public InfantryUnitController(GameConfig config, IModel model, Vector3 playerPosition):base(config,model,playerPosition)
        {
        }
        
        
        #region Methods

        public static void DoDamage(IModel model)
        {
            model.Health -= EnemyModelDamage;
        }
        
        #endregion
        
    }
}
