using UnityEngine;
using System.Collections.Generic;
public class PathRequester : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private List<Vector2> _path;
    private int _currentIndex = 0;
    public void RequestPathfinding()
    {
        if (target == null || GridManager.Instance == null) return;
        _path = AStarPathfinder.FindPath(transform.position, target.position, allowPartialPath: true);
    }

    public void MoveInAStar()
    {
        if (_path != null && _currentIndex < _path.Count)
        {
            Vector2 goal = _path[_currentIndex];
            Vector2 pos = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            transform.position = pos;
            if ((Vector2)transform.position == goal)
                _currentIndex++;
        }
    }
    
    void OnDrawGizmos()
    {
        
        if (_path == null) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < _path.Count - 1; i++)
            Gizmos.DrawLine(_path[i], _path[i + 1]);
    }
}
