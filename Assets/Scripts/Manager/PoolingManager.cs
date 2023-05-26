using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Manager
{
    public class PoolingManager : MonoBehaviour
    {
        public static PoolingManager Instance;
        
        public List<GameObject> pooledObjects;
        public GameObject projectilePrefab;
        public int amountToPool;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for(var i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(projectilePrefab);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }
        
        public GameObject GetPooledObject()
        {
            for(int i = 0; i < amountToPool; i++)
            {
                if(!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }
}