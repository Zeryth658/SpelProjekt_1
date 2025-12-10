using System;
using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    public static GridManager Instance {get; private set;}
    
    [Header("Grid Settings")]
    public int width = 50;
    public int height = 50;
    public float nodeSize = 1f;
    public Vector2 origin = Vector2.zero;
    public bool allowDiagonal = true;
    public LayerMask obstacleMask;
    public float overlapBoxPadding = 0.9f; // OBS: NEEDS TO BE SMALLER THAN NODESIZE
    
    [Header("Debug")] 
    public bool drawGizmos = false;
    public Color walkableColor  =  new Color(0.2f,0.8f,0.2f,0.4f);
    public Color blockedColor =  new Color(0.8f,0.2f,0.2f,0.4f);
    
    private GridNode[,] _grid;

    void Awake()
    {
        Instance = this;
        BuildGrid();
    }

    public void BuildGrid()
    {
        _grid = new GridNode[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 worldPosition = origin + new Vector2(x * nodeSize + nodeSize * 0.5f, y * nodeSize + nodeSize * 0.5f);
                bool walkable = !Physics2D.OverlapBox(worldPosition, Vector2.one * (nodeSize * overlapBoxPadding), 0f, obstacleMask);
                float terrainCost = 1f; // I could change this to some value depending on the Tilemap possibly 
                _grid[x, y] = new GridNode(x, y, worldPosition, walkable, terrainCost);
            }
        }
    }

    public GridNode NodeFromWorldPosition(Vector2 worldPosition)
    {
        if (_grid ==  null) BuildGrid();
        
        float percentageOfDirectionX = (worldPosition.x - origin.x) / (width * nodeSize);
        float percentageOfDirectionY = (worldPosition.y - origin.y) / (height * nodeSize);
        percentageOfDirectionX = Mathf.Clamp01(percentageOfDirectionX);
        percentageOfDirectionY = Mathf.Clamp01(percentageOfDirectionY);

        int x = Mathf.Clamp(Mathf.FloorToInt(percentageOfDirectionX * width), 0, width - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt(percentageOfDirectionY * height), 0, height - 1);
        
        return _grid[x, y];
    }

    public IEnumerable<GridNode> GetNeighbours(GridNode node)
    {
        int leftNeighbourX = node.X - 1;
        int rightNeighbourX =  node.X + 1;
        int bottomNeighbourY =  node.Y - 1;
        int topNeighbourY = node.Y + 1;

        for (int x = leftNeighbourX; x <= rightNeighbourX; x++)
        {
            for (int y = bottomNeighbourY; y <= topNeighbourY; y++)
            {
                if (x == node.X && y == node.Y) continue;
                if (x < 0 || x >= width || y < 0 || y >= height) continue;

                if (!allowDiagonal && Mathf.Abs(x - node.X) + Mathf.Abs(y - node.Y) > 1) continue;
                
                yield return _grid[x, y];
            }
        }
        
    }
    
    public void ResetAllNodes()
    {
        if (_grid == null) return;
        for (int x = 0; x < _grid.GetLength(0); x++)
            for (int y = 0; y < _grid.GetLength(1); y++)
                _grid[x, y].ResetPathfinding();
    }
    
    public GridNode[,] GetGridArray()
    {
        return _grid;
    }
    
    public void SetTerrainCost(int x, int y, float cost)
    {
        if (_grid == null) BuildGrid();
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        _grid[x, y].TerrainCost = Mathf.Max(0.01f, cost);
    }
    
    public void SetWalkable(int x, int y, bool walkable)
    {
        if (_grid == null) BuildGrid();
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        _grid[x, y].Walkable = walkable;
    }
    
    void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        if (_grid == null) BuildGrid();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GridNode node = _grid[x, y];
                Gizmos.color = node.Walkable ? walkableColor : blockedColor;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeSize * 0.9f));
            }
        }
    }
}
