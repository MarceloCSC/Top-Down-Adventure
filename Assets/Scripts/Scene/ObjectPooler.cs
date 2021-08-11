using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Properties
    public static ObjectPooler Instance { get; private set; }
    #endregion


    protected virtual void Awake()
    {
        Instance = this;
        CreatePool();
    }

    protected void CreatePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject currentObject = Instantiate(pool.prefab);
                currentObject.SetActive(false);
                currentObject.transform.parent = transform;
                objectPool.Enqueue(currentObject);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnObject(string tag, Vector2 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            if (objectToSpawn.activeInHierarchy)
            {
                poolDictionary[tag].Enqueue(objectToSpawn);

                foreach (Pool pool in pools)
                {
                    if (pool.tag == tag)
                    {
                        objectToSpawn = Instantiate(pool.prefab);
                        objectToSpawn.transform.parent = transform;
                    }
                }
            }
            else
            {
                objectToSpawn.SetActive(true);
            }

            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        else
        {
            Debug.LogWarning($"There's no GameObject tagged as '{tag}' in the Object Pooler.");
            return null;
        }

    }

}
