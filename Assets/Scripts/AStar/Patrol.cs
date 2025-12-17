using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(PathMovement))]
public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 0f; //How long they should wait at each waypoint
    public bool loopPath = true;
    private int currentWaypoint = 0;
    private float waitTimer = 0f;
    private PathMovement movement;
    private int direction = 1;
    
    public void BeginPatrol()
    {
        this.movement = GetComponent<PathMovement>();
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }
        currentWaypoint = 0;
        GoToNextPoint();
    }

    public void UpdateWayPoint()
    {
        if (waypoints == null || waypoints.Length == 0) return;
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
        
        if (loopPath)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        
        else
        {
            if (waypoints.Length < 2)
                return;
            currentWaypoint += direction;

            if (currentWaypoint >= waypoints.Length)
            {
                direction = -1;
                currentWaypoint = waypoints.Length - 2;
            }
            else if (currentWaypoint < 0)
            {
                direction = 1;
                currentWaypoint = 1;
            }
        }
        
        GoToNextPoint();
    }
    private void GoToNextPoint()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;
        Vector2 start = transform.position;
        Vector2 end = waypoints[currentWaypoint].position;
        List<Vector2> path = AStarPathfinder.FindPath(start, end, true);
        movement.SetPath(path);
    }
}
