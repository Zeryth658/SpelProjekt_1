using UnityEngine;
using System.Collections.Generic;
public class PathMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float reachThreshold = 0.1f;
    
    private List<Vector2> path;
    private int pathIndex = 0;
    private LayerMask obstacleMask;
   
    
    public bool IsMoving => path != null && pathIndex < path.Count;
    
    public void Initialize(LayerMask obstacleMask)
    {
        this.obstacleMask = obstacleMask;
    }

    public void SetPath(List<Vector2> newPath, bool smooth = false)
    {
        if (newPath == null || newPath.Count == 0)
        {
            path = null;
            pathIndex = 0;
            return;
        } 
        if (smooth && obstacleMask != 0)
        {
            path = PathSmoothing.SmoothPath(newPath, obstacleMask);
        }
        else
        {
            path = new List<Vector2>(newPath);
        }
        
        pathIndex = 0;
    }

    //Should be called as often as Update()
    public void UpdateMovement()
    {
        if (!IsMoving) return;
        
        Vector2 destination = path[pathIndex];
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destination) <= reachThreshold)
        {
            pathIndex++;
        }
    }
    
    public Vector2 GetCurrentTarget()
    {
        return IsMoving ? path[pathIndex] : (Vector2)transform.position;
    }


}
