using System;
using BaseScripts;
using Interface;
using UnityEngine;

namespace Player
{
    public class PlayerView : BaseObjectView, IView
    {

        #region Fields

        public Transform gunMuzzle;

        #endregion

        public delegate void OnAttacked();

        public event OnAttacked OnBombDamageTaken;
        public event OnAttacked OnBulletDamageTaken;

        public void OnTriggerEnter2D(Collider2D other)
        {
            switch(other.tag)
            {
                case ("Bomb"):
                {
                    OnBombDamageTaken?.Invoke();
                } break;
                case ("Enemy Bullet"):
                {
                    OnBulletDamageTaken?.Invoke();
                } break;
            }
        }
    }
}
