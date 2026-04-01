using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityRandom = UnityEngine.Random;

// Version 3.0: Added automatic TilemapCollider2D and CompositeCollider2D setup for wall collisions
public class ProceduralTilemapGenerator : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap MapTile;

    public TileBase FloorTile;

    public TileBase WallTile;

    [Header("Generation Settings")]
    public int Width = 50;

    public int Height = 50;

    public int MinRoomSize = 3;

    public int MaxRoomSize = 10;

    public int MaxRooms = 10;

    public bool UseRandomSeed = true;

    public int Seed = 0;

    Random Rdn;

    void Awake()
    {
        // Ensure Tilemap has TilemapCollider2D
        if (MapTile.GetComponent<TilemapCollider2D>() == null)
        {
            MapTile.gameObject.AddComponent<TilemapCollider2D>();
        }

        // Ensure Tilemap has CompositeCollider2D
        if (MapTile.GetComponent<CompositeCollider2D>() == null)
        {
            CompositeCollider2D Composite = MapTile.gameObject.AddComponent<CompositeCollider2D>();

            Composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
            
            Composite.generationType = CompositeCollider2D.GenerationType.Manual;
        }

        // Set TilemapCollider2D to use Composite
        TilemapCollider2D MapTileCollider = MapTile.GetComponent<TilemapCollider2D>();

        if (MapTileCollider != null)
        {
            MapTileCollider.compositeOperation = Collider2D.CompositeOperation.Merge;
        }
    }

    void Start()
    {
        if (UseRandomSeed)
        {
            Seed = UnityRandom.Range(0, int.MaxValue);
        }

        Rdn = new Random(Seed);
        
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Clear the tilemap
        MapTile.ClearAllTiles();

        // Create a 2D array to represent the map
        int[,] Map = new int[Width, Height];

        // Initialize with walls
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Map[x, y] = 1; // 1 = wall
            }
        }

        // Generate rooms
        List<RectInt> Rooms = new List<RectInt>();

        for (int i = 0; i < MaxRooms; i++)
        {
            int RoomWidth = Rdn.Next(MinRoomSize, MaxRoomSize + 1);

            int RoomHeight = Rdn.Next(MinRoomSize, MaxRoomSize + 1);

            int RoomX = Rdn.Next(1, Width - RoomWidth - 1);
            
            int RoomY = Rdn.Next(1, Height - RoomHeight - 1);

            RectInt NewRoom = new RectInt(RoomX, RoomY, RoomWidth, RoomHeight);

            bool Overlaps = false;

            foreach (RectInt Room in Rooms)
            {
                if (NewRoom.Overlaps(Room))
                {
                    Overlaps = true;

                    break;
                }
            }

            if (!Overlaps)
            {
                Rooms.Add(NewRoom);

                // Carve out the room
                for (int x = RoomX; x < RoomX + RoomWidth; x++)
                {
                    for (int y = RoomY; y < RoomY + RoomHeight; y++)
                    {
                        Map[x, y] = 0; // 0 = floor
                    }
                }
            }
        }

        // Connect rooms with corridors
        for (int i = 1; i < Rooms.Count; i++)
        {
            Vector2Int PrevCenter = new Vector2Int(Rooms[i - 1].x + Rooms[i - 1].width / 2,
            Rooms[i - 1].y + Rooms[i - 1].height / 2);
            
            Vector2Int CurrCenter = new Vector2Int(Rooms[i].x + Rooms[i].width / 2,
            Rooms[i].y + Rooms[i].height / 2);

            if (Rdn.Next(0, 2) == 0)
            {
                // Horizontal then vertical
                CreateHorizontalCorridor(Map, PrevCenter.x, CurrCenter.x, PrevCenter.y);

                CreateVerticalCorridor(Map, PrevCenter.y, CurrCenter.y, CurrCenter.x);
            }

            else
            {
                // Vertical then horizontal
                CreateVerticalCorridor(Map, PrevCenter.y, CurrCenter.y, PrevCenter.x);

                CreateHorizontalCorridor(Map, PrevCenter.x, CurrCenter.x, CurrCenter.y);
            }
        }

        // Place tiles on the tilemap
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector3Int Position = new Vector3Int(x, y, 0);

                if (Map[x, y] == 0)
                {
                    MapTile.SetTile(Position, FloorTile);
                }

                else
                {
                    MapTile.SetTile(Position, WallTile);
                }
            }
        }
    }

    void CreateHorizontalCorridor(int[,] Map, int XStart, int XEnd, int Y)
    {
        int Start = Mathf.Min(XStart, XEnd);

        int End = Mathf.Max(XStart, XEnd);

        for (int x = Start; x <= End; x++)
        {
            Map[x, Y] = 0;
        }
    }

    void CreateVerticalCorridor(int[,] Map, int YStart, int YEnd, int X)
    {
        int Start = Mathf.Min(YStart, YEnd);

        int End = Mathf.Max(YStart, YEnd);

        for (int y = Start; y <= End; y++)
        {
            Map[X, y] = 0;
        }
    }
}