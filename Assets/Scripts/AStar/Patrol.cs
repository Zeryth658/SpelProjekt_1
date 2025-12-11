using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(PathMovement))]
public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 0f; //How long they should wait at each waypoint
    
    private int currentWaypoint = 0;
    private float waitTimer = 0f;
    private PathMovement movement;
    
    public void BeginPatrol()
    {
        this.movement = GetComponent<PathMovement>();
        currentWaypoint = 0;
        GoToNextPoint();
    }

    public void UpdateWayPoint()
    {
        if (!movement.IsMoving)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;
                AdvancePatrolPoint();
            }
        }
    }

    private void AdvancePatrolPoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        GoToNextPoint();
    }
    private void GoToNextPoint()
    {
        Vector2 start = transform.position;
        Vector2 end = waypoints[currentWaypoint].position;
        List<Vector2> path = AStarPathfinder.FindPath(start, end, true);
        movement.SetPath(path);
    }
}
