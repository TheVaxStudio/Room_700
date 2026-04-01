using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralTilemapGenerator : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap tilemap;
    public TileBase floorTile;
    public TileBase wallTile;

    [Header("Generation Settings")]
    public int width = 50;
    public int height = 50;
    public int minRoomSize = 3;
    public int maxRoomSize = 10;
    public int maxRooms = 10;
    public bool useRandomSeed = true;
    public int seed = 0;

    private System.Random random;

    void Start()
    {
        if (useRandomSeed)
        {
            seed = Random.Range(0, int.MaxValue);
        }
        random = new System.Random(seed);
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Clear the tilemap
        tilemap.ClearAllTiles();

        // Create a 2D array to represent the map
        int[,] map = new int[width, height];

        // Initialize with walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = 1; // 1 = wall
            }
        }

        // Generate rooms
        List<RectInt> rooms = new List<RectInt>();
        for (int i = 0; i < maxRooms; i++)
        {
            int roomWidth = random.Next(minRoomSize, maxRoomSize + 1);
            int roomHeight = random.Next(minRoomSize, maxRoomSize + 1);
            int roomX = random.Next(1, width - roomWidth - 1);
            int roomY = random.Next(1, height - roomHeight - 1);

            RectInt newRoom = new RectInt(roomX, roomY, roomWidth, roomHeight);

            bool overlaps = false;
            foreach (RectInt room in rooms)
            {
                if (newRoom.Overlaps(room))
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                rooms.Add(newRoom);

                // Carve out the room
                for (int x = roomX; x < roomX + roomWidth; x++)
                {
                    for (int y = roomY; y < roomY + roomHeight; y++)
                    {
                        map[x, y] = 0; // 0 = floor
                    }
                }
            }
        }

        // Connect rooms with corridors
        for (int i = 1; i < rooms.Count; i++)
        {
            Vector2Int prevCenter = new Vector2Int(rooms[i - 1].x + rooms[i - 1].width / 2, rooms[i - 1].y + rooms[i - 1].height / 2);
            Vector2Int currCenter = new Vector2Int(rooms[i].x + rooms[i].width / 2, rooms[i].y + rooms[i].height / 2);

            if (random.Next(0, 2) == 0)
            {
                // Horizontal then vertical
                CreateHorizontalCorridor(map, prevCenter.x, currCenter.x, prevCenter.y);
                CreateVerticalCorridor(map, prevCenter.y, currCenter.y, currCenter.x);
            }
            else
            {
                // Vertical then horizontal
                CreateVerticalCorridor(map, prevCenter.y, currCenter.y, prevCenter.x);
                CreateHorizontalCorridor(map, prevCenter.x, currCenter.x, currCenter.y);
            }
        }

        // Place tiles on the tilemap
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (map[x, y] == 0)
                {
                    tilemap.SetTile(position, floorTile);
                }
                else
                {
                    tilemap.SetTile(position, wallTile);
                }
            }
        }
    }

    void CreateHorizontalCorridor(int[,] map, int xStart, int xEnd, int y)
    {
        int start = Mathf.Min(xStart, xEnd);
        int end = Mathf.Max(xStart, xEnd);
        for (int x = start; x <= end; x++)
        {
            map[x, y] = 0;
        }
    }

    void CreateVerticalCorridor(int[,] map, int yStart, int yEnd, int x)
    {
        int start = Mathf.Min(yStart, yEnd);
        int end = Mathf.Max(yStart, yEnd);
        for (int y = start; y <= end; y++)
        {
            map[x, y] = 0;
        }
    }
}