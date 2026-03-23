using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap FloorTilemap;

    public Tilemap WallTilemap;

    [Header("Tiles")]
    public TileBase FloorTile;
    
    public TileBase WallTile;

    [Header("Prefabs")]
    public GameObject PlayerPrefab;

    public GameObject NpcPrefab;

    public GameObject DoorPrefab;
    
    public List<GameObject> ItemPrefabs = new List<GameObject>();

    [Header("Dungeon Settings")]
    int MapWidth = 512;

    int MapHeight = 512;

    int RoomSize = 64;

    int RoomsPerRow = 4;

    int Spacing = 96;
    
    int CorridorWidth = 3;

    int Seed = 0;

    enum TileType
    {
        Empty,
        Floor,
        Wall
    }
    
    TileType[,] Map;
    
    List<Room> Rooms = new List<Room>();
    
    System.Random Rng;
    
    bool Generated = false;

    bool Walkable;

    void Update()
    {
        if (!Generated)
        {
            GenerateDungeon(MapHeight, MapWidth);

            Generated = true;
        }
    }

    void GenerateDungeon(int MapHeight, int MapWidth)
    {
        Rng = (Seed == 0) ? new System.Random() : new System.Random(Seed);

        Map = new TileType[MapWidth, MapHeight];
        
        Rooms.Clear();

        int TotalRooms = RoomsPerRow * RoomsPerRow;

        for (int I = 0; I < TotalRooms; I++)
        {
            int Gx = I % RoomsPerRow;

            int Gy = I / RoomsPerRow;

            int X = Gx * Spacing + Rng.Next(-5, 6);

            int Y = Gy * Spacing + Rng.Next(-5, 6);

            Room R = new Room(X, Y, RoomSize, RoomSize);

            Rooms.Add(R);
            
            FillArea(R.X, R.Y, R.W, R.H, TileType.Floor);
        }

        for (int I = 1; I < Rooms.Count; I++)
        {
            CreateCorridor(Rooms[I - 1].Center, Rooms[I].Center);
        }

        for (int X = 1; X < MapWidth - 1; X++)
        {
            for (int Y = 1; Y < MapHeight - 1; Y++)
            {
                if (Map[X, Y] == TileType.Floor)
                {
                    for (int Dx = -1; Dx <= 1; Dx++)
                    {
                        for (int Dy = -1; Dy <= 1; Dy++)
                        {
                            if (Map[X + Dx, Y + Dy] == TileType.Empty)
                            {
                                Map[X + Dx, Y + Dy] = TileType.Wall;
                            }
                        }
                    }
                }
            }
        }

        FloorTilemap.ClearAllTiles();

        WallTilemap.ClearAllTiles();

        for (int X = 0; X < MapWidth; X++)
        {
            for (int Y = 0; Y < MapHeight; Y++)
            {
                if (Map[X, Y] == TileType.Floor)
                {
                    FloorTilemap.SetTile(new Vector3Int(X, Y, 0), FloorTile);
                }

                else if (Map[X, Y] == TileType.Wall)
                {
                    WallTilemap.SetTile(new Vector3Int(X, Y, 0), WallTile);
                }
            }
        }

        this.Walkable = true;

        this.MapWidth = MapWidth;

        this.MapHeight = MapHeight;

        for (int X = 0; X < MapWidth; X++)
        {
            for (int Y = 0; Y < MapHeight; Y++)
            {
                Walkable = true;
            }
        }

        if (Rooms.Count > 0)
        {
            Vector3 Start = new Vector3(Rooms[0].Center.x + 0.5f, Rooms[0].Center.y + 0.5f, 0);

            if (PlayerPrefab)
            {
                Instantiate(PlayerPrefab, Start, Quaternion.identity);
            }
            
            if (NpcPrefab)
            {
                Instantiate(NpcPrefab, Start + Vector3.right * 2, Quaternion.identity);
            }
        }

        if (Rooms.Count > 1 && DoorPrefab)
        {
            Room Last = Rooms[^1];

            Vector3 Pos = new Vector3(Last.Center.x, Last.Center.y, 0);
            
            Instantiate(DoorPrefab, Pos, Quaternion.identity);
        }

        for (int I = 0; I < Rooms.Count && I < ItemPrefabs.Count; I++)
        {
            Room R = Rooms[I];

            int Margin = 4;

            int Ix = Rng.Next(R.X + Margin, R.X + R.W - Margin);

            int Iy = Rng.Next(R.Y + Margin, R.Y + R.H - Margin);

            Vector3 ItemPos = new Vector3(Ix + 0.5f, Iy + 0.5f, 0);
            
            Instantiate(ItemPrefabs[I], ItemPos, Quaternion.identity);
        }

        Debug.Log($"Dungeon gerada com {Rooms.Count} salas. Seed: {Seed}");
    }

    void FillArea(int StartX, int StartY, int W, int H, TileType Type)
    {
        int Sx = Mathf.Max(0, StartX);

        int Sy = Mathf.Max(0, StartY);

        int Ex = Mathf.Min(MapWidth, StartX + W);

        int Ey = Mathf.Min(MapHeight, StartY + H);
        
        for (int X = Sx; X < Ex; X++)
        {
            for (int Y = Sy; Y < Ey; Y++)
            {
                Map[X, Y] = Type;
            }
        }
    }

    void CreateCorridor(Vector2Int A, Vector2Int B)
    {
        int X = A.x, Y = A.y;
        
        while (X != B.x)
        {
            FillArea(X, Y, CorridorWidth, CorridorWidth, TileType.Floor);
            
            X += (B.x > X) ? 1 : -1;
        }

        while (Y != B.y)
        {
            FillArea(X, Y, CorridorWidth, CorridorWidth, TileType.Floor);

            Y += (B.y > Y) ? 1 : -1;
        }
    }

    [System.Serializable]
    public class Room
    {
        public int X, Y, W, H;
        
        public Vector2Int Center => new Vector2Int(X + W / 2, Y + H / 2);

        public Room(int X, int Y, int W, int H)
        {
            this.X = X; this.Y = Y; this.W = W; this.H = H;
        }
    }
}