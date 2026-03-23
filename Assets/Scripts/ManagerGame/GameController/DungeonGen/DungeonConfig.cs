using System;
using UnityEngine;

[Serializable]
public class DungeonConfig
{
    public int RoomCount;

    public Vector2Int MaxRoomSize;
    
    public Vector2Int MinRoomSize;

    public DungeonConfig(int RoomCount, Vector2Int MaxRoomSize, Vector2Int MinRoomSize)
    {
        this.RoomCount = RoomCount;

        this.MaxRoomSize = MaxRoomSize;

        this.MinRoomSize = MinRoomSize;
    }
}