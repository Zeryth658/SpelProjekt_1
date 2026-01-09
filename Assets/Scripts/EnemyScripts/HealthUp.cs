using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] private float ExistanceTime = 0.5f;
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private float rightDrift = 0.3f;
    private float timer = 0f;
    public void Update()
    {
        if (timer >= ExistanceTime)
        {
            PoolManager.Despawn(gameObject);
            return;
        }
        timer += Time.deltaTime;
        Vector2 direction = Vector2.up + Vector2.right * rightDrift;
        transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);
    }
}
