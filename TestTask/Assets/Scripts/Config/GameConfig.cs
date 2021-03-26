using System;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "Configs/GameCfg", fileName = "GameCfg", order = 1)]
    public class GameConfig : ScriptableObject
    {
        #region PrivateData

        private const float DefaultValue = 1.0f;

        #endregion

        #region Fields

        public float playerHealth;
        public float playerPower;
        public float enemyHealth;
        public float enemySpeed;
        public float enemyPower;
        public float enemySpawnRatio;

        #endregion


        #region Methods

        public void ResetValues()
        {
            playerHealth = DefaultValue;
            playerPower = DefaultValue;
            enemyHealth = DefaultValue;
            enemySpeed = DefaultValue;
            enemyPower = DefaultValue;
            enemySpawnRatio = DefaultValue;
        }

        #endregion

    }
}
