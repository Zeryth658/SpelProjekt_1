using UnityEngine;

public class Bullet : MonoBehaviour, IDestroyOnImpact
{
    public float speed = 10f;
    public Vector2 moveDirection;
    public Hitbox hitbox;
    public float bulletLifeTime = 5f;

    public void Initialize(Vector2 direction, float damage, GameObject owner, float spd, float lifetime)
    {
        bulletLifeTime = lifetime;
        speed = spd;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{gameObject.name} entered {other.gameObject.name}");
        if (other.tag == "Wall")
        {
            DestroyMe();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveDirection *(speed * Time.deltaTime);
        
    }
}
