using UnityEngine;

public class Bullet : MonoBehaviour, IDestroyOnImpact
{
    public float speed = 10f;
    public Vector2 moveDirection;
    public Hitbox hitbox;

    public void Initialize(Vector2 direction, float damage, GameObject owner)
    {
        moveDirection = direction.normalized;
        hitbox.damage = damage;
        hitbox.owner = owner;
        hitbox.attackID = Random.Range(int.MinValue, int.MaxValue);
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
