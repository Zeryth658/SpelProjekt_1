using UnityEngine;
using System.Collections.Generic;
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    
    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<GameObject, Pool> prefabLookUp = new Dictionary<GameObject, Pool>();

    private void Awake()
    {
        Instance = this;

        // Initialize all pools and adds them
        foreach (Pool pool in pools)
        {
            pool.InitializePool();
            prefabLookUp.Add(pool.prefab, pool);
        }
    }

    // Spawns an object from a prehab using its preexisting pool or makes a new object if it didnt have one
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Pool pool = Instance.prefabLookUp[prefab];

        PoolObject anObject = pool.GetObject();
        anObject.transform.SetPositionAndRotation(position, rotation);
        
        return anObject.gameObject;
    }

    // Returns object to its pool unless its not a pool in which case its destroyed
    public static void Despawn(GameObject gameObject)
    {
        PoolObject aPool = gameObject.GetComponent<PoolObject>();
        
        // So long it is a Pool it returns it to the Pool
        if (aPool != null)
        {
            aPool.ReturnToPool();
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
