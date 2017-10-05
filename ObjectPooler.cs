using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    public GameObject pooledGameObject;
    public int poolSize = 100;
    public bool willGrow = true;

    private List<GameObject> pooledGameObjects;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        pooledGameObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject) Instantiate(pooledGameObject);
            obj.SetActive(false);
            pooledGameObjects.Add(obj);
        }
    }

    public GameObject GetPooledGameObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pooledGameObjects[i].activeInHierarchy)
            {
                return pooledGameObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject) Instantiate(pooledGameObject);
            pooledGameObjects.Add(obj);
            return obj;
        }

        return null;
    }
}