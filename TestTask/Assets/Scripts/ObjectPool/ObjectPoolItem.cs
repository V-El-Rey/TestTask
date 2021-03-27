using System;
using UnityEngine;

namespace Pool
{
    [Serializable]
    public class ObjectPoolItem
    {
        public int amountToPool;
        public GameObject objectToPool;
    }
}
