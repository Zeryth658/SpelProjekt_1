using UnityEngine;

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
    
    GridNode[,] grid;

    void Awake()
    {
        Instance = this;
        BuildGrid();
    }

    public void BuildGrid()
    {
        grid = new GridNode[width, height];
        for (int x = 0; x < width; x++)
        {
            
        }
    }
    
}
