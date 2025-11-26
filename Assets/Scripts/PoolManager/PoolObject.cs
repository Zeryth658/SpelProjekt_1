using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public Pool pool;

    // Returns the object back into the Pool 
    public void ReturnToPool()
    {
        pool.ReturnObjectToPool(this);
    }
}
