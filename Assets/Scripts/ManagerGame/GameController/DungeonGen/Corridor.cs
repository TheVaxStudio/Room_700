using UnityEngine;

public class Corridor
{
    public Vector2Int StartPosition
    {
        get;

        set;
    }

    public Vector2Int EndPosition
    {
        get;

        set; 
    }

    public Corridor(Vector2Int StartPos, Vector2Int EndPos)
    {
        StartPosition = StartPos;
    
        EndPosition = EndPos;
    }

    public void GenerateCorridor()
    {
        // Implementation for generating the corridor between StartPosition and EndPosition
    }
}