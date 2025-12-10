using UnityEngine;
using System.Collections.Generic;


public class AStarPathfinder 
{
    public static List<Vector2> FindPath(Vector2 startPoint, Vector2 goalPoint, bool allowPartialPath = false)
    {
        GridManager gridManager  = GridManager.Instance;
        if (gridManager == null) return null;

        GridNode start = gridManager.NodeFromWorldPosition(startPoint);
        GridNode goal = gridManager.NodeFromWorldPosition(goalPoint);
        
        if (start == null || goal == null ) return null;
        if (!start.Walkable) return null;


        gridManager.ResetAllNodes();

        PriorityQueue open = new PriorityQueue();
        HashSet<GridNode> closed = new HashSet<GridNode>();
        
        start.GScore = 0;
        start.HScore = Heuristic(start, goal);
        open.Enqueue(start);
        
        GridNode bestNode = start;
        while (open.Count > 0)
        {
            GridNode currentNode = open.Dequeue();

            if (currentNode.Equals(goal))
            {
                return ReconstructPathWorld(currentNode);
            }
            
            closed.Add(currentNode);
            
            if (currentNode.HScore < bestNode.HScore) bestNode = currentNode;
            
            foreach (GridNode neighbor in gridManager.GetNeighbours(currentNode))
            {
                if (!neighbor.Walkable || closed.Contains(neighbor))  continue;
                
                float movementCostToNeighbor = DistanceBetween(currentNode, neighbor) * neighbor.TerrainCost;
                float tentativeGScore = currentNode.GScore + movementCostToNeighbor;

                if (tentativeGScore < neighbor.GScore)
                {
                    neighbor.Parent = currentNode;
                    neighbor.GScore = tentativeGScore;
                    neighbor.HScore = Heuristic(neighbor, goal);
                    
                    if (!open.Contains(neighbor)) open.Enqueue(neighbor);
                }
            }
        }

        if (allowPartialPath)
        {
            return ReconstructPathWorld(bestNode);
        }
        
        return null;

    }
    
    private static float Heuristic(GridNode a, GridNode b)
    {
        bool diagonal = GridManager.Instance.allowDiagonal;
        int dx = Mathf.Abs(a.X - b.X);
        int dy = Mathf.Abs(a.Y - b.Y);

        if (diagonal)
        {
            float fScore = (Mathf.Sqrt(2f) - 1f) * Mathf.Min(dx, dy) + Mathf.Max(dx, dy);
            return fScore;
        }
        
        return dx + dy;
        
    }
    
    static float DistanceBetween(GridNode a, GridNode b)
    {
        int dx = Mathf.Abs(a.X - b.X);
        int dy = Mathf.Abs(a.Y - b.Y);
        if (dx == 1 && dy == 1) return Mathf.Sqrt(2f);
        if (dx + dy == 1) return 1f;
   
        return Vector2.Distance(a.WorldPosition, b.WorldPosition);
    }
    private static List<Vector2> ReconstructPathWorld(GridNode end)
    {
        List<Vector2> path = new List<Vector2>();
        GridNode current = end;
        while (current != null)
        {
            path.Add(current.WorldPosition);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }
}
