using UnityEngine;

public class Bullet : MonoBehaviour, IDestroyOnImpact
{
    public float speed = 10f;
    public Vector2 moveDirection;


    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    public void DestroyMe()
    {
        Debug.Log($"{gameObject.name} killed");
        PoolManager.Despawn(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveDirection *(speed * Time.deltaTime);
        
    }
}
