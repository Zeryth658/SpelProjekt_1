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
    private OrbitingWeapon _orbitingWeapon;
    private Animator animator;
    
    public bool IsMoving => path != null && pathIndex < path.Count;
    
    public void Initialize(LayerMask obstacleMask)
    {
        this.obstacleMask = obstacleMask;
        this._orbitingWeapon = GetComponentInChildren<OrbitingWeapon>();
        animator = GetComponent<Animator>();
    }

    public void SetPath(List<Vector2> newPath, bool smooth = true)
    {
        if (newPath == null || newPath.Count == 0)
        {
            path = null;
            pathIndex = 0;
            return;
        } 
        if (smooth && obstacleMask != 0)
        {
            path = PathSmoothing.SmoothPath(newPath, obstacleMask, transform.position);
        }
        else
        {
            path = new List<Vector2>(newPath);
        }
        while (path.Count > 1 &&
               Vector2.Distance(transform.position, path[0]) <
               Vector2.Distance(transform.position, path[1]))
        {
            path.RemoveAt(0);
        }
        pathIndex = 0;
    }

    //Should be called as often as Update()
    public void UpdateMovement()
    {
        if (!IsMoving) return;
        animator.SetBool("IsMoving", true);
        Vector2 destination = path[pathIndex];
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPosition;
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        _orbitingWeapon.UpdateRotation(direction);
        
        float movementMagnitude = (newPosition - currentPosition).magnitude / Time.deltaTime;
        animator.SetFloat("Movement", movementMagnitude);
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
