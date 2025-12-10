using UnityEngine;

public class GridNode 
{
    public int X;
    public int Y;
    public Vector2 WorldPosition;
    public bool Walkable = true;
    public float TerrainCost = 1f; // 1 = normal less equals slower

    public float GScore = float.PositiveInfinity;
    public float HScore = 0f;
    public GridNode Parent = null;
    
    public float FScore => GScore + HScore;

    public GridNode(int x, int y, Vector2 worldPos, bool walkable = true, float terrainCost = 1f)
    {
        this.X = x;
        this.Y = y;
        this.Walkable = walkable;
        this.WorldPosition = worldPos;
        this.Walkable = walkable;
        this.TerrainCost = terrainCost;
    }

    public void ResetPathfinding()
    {
        GScore = float.PositiveInfinity;
        HScore = 0f;
        Parent = null; 
    }

}
