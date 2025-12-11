using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;     
    private Vector2 movement;
    private EnemyShooter shooter;
    private Transform target ;

    void Awake()
    {
        
        shooter = GetComponent<EnemyShooter>();
        target = shooter.target;

    }

    public void MovementUpdate()
    {
        
        
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
