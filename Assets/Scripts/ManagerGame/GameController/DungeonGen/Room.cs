using UnityEngine;

public class Room
{
    public Vector2Int Position
    {
        get;

        set; 
    }

    public Vector2Int Size
    {
        get;

        set; 
    }

    public Room(Vector2Int Position, Vector2Int Size)
    {
        this.Position = Position;

        this.Size = Size;
    }

    public void GenerateRoom()
    {
        // Implementation for generating the room in the dungeon
    }
}