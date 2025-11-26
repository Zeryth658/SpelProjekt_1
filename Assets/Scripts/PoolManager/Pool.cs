using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Pool 
{
    public GameObject prefab;
    public Transform parent;
    public int initialPoolSize = 10;

    private readonly Queue<PoolObject> objects = new Queue<PoolObject>();

    public void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    // creates a new instanse of an object an adds it to the pool disabling it also.
    private PoolObject CreateNewObject()
    {
        GameObject anObject = GameObject.Instantiate(prefab, parent);
        PoolObject aPool = anObject.AddComponent<PoolObject>();
        aPool.pool  = this;
        anObject.SetActive(false);
        objects.Enqueue(aPool);
        return aPool;

        
    }
    
    // gets an object from the pool if there is none it creatres a new object
    public PoolObject GetObject()
    {
        if (objects.Count == 0)
            CreateNewObject();

        PoolObject anObject = objects.Dequeue();
        anObject.gameObject.SetActive(true);
        return anObject;
    }
    
    //  Returns the object back into the pool disabling it in the process
    public void ReturnObjectToPool(PoolObject anObject)
    {
        anObject.gameObject.SetActive(false);
        objects.Enqueue(anObject);
    }
    
}
