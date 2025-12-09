using UnityEngine;

public class GridNode 
{
    public int x;
    public int y;
    public bool walkable;
    public Vector2 worldPos;
    public float terrainCost = 1f; // 1 = normal less equals slower

    public float gScore = float.PositiveInfinity;
    public float hScore = 0f;
    public GridNode parent = null;
    
    public float FScore => gScore + hScore;

    public GridNode(int x, int y, Vector2 worldPos, bool walkable = true, float terrainCost = 1f)
    {
        this.x = x;
        this.y = y;
        this.walkable = walkable;
        this.worldPos = worldPos;
        this.walkable = walkable;
        this.terrainCost = terrainCost;
    }

    public void ResetPathfinding()
    {
        gScore = float.PositiveInfinity;
        hScore = 0f;
        parent = null; 
    }

}
