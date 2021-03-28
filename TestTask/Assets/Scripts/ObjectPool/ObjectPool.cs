using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ObjectPool
    {
        public static List<GameObject> _pooledObjects;
        private static List<ObjectPoolItem> _itemsToPool;
    
        
        public void Initialize(List<ObjectPoolItem> objectList)
        {
            _itemsToPool = objectList;
            _pooledObjects = new List<GameObject>();
            foreach (var item in _itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    var obj = Object.Instantiate(item.objectToPool);
                    AddObjectToPool(obj);
                }
            }
        }

        public static GameObject GetObjectFromPool(string tag)
        {
            foreach (var t in _pooledObjects)
            {
                if (!t.activeInHierarchy && t.CompareTag(tag))
                {
                    return t;
                }
            }
            return null;
        }

        public static List<GameObject> GetListFromPool(string tag)
        {
            var returnList = new List<GameObject>();
            foreach (var item in _pooledObjects)
            {
                if (item.CompareTag(tag))
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }

        private static void AddObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }
}
