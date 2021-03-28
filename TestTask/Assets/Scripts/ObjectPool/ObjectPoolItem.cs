using System;
using UnityEngine;

namespace Pool
{
    [Serializable]
    public class ObjectPoolItem
    {

        #region Fields

        public int amountToPool;
        public GameObject objectToPool;

        #endregion

    }
}
