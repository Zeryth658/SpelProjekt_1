using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveDirection;


    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        
        
    }
}
