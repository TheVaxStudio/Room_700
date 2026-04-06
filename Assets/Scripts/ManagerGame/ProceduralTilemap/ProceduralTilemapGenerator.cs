using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityRandom = UnityEngine.Random;

public class ProceduralTilemapGenerator : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap MapTile;

    public Tilemap WallTilemap;

    public TileBase DoorTile;

    public TileBase FloorTile;

    public TileBase WallTile;

    [Header("Player Settings")]
    public GameObject PlayerPrefab;

    [Header("Door Settings")]
    public GameObject DoorPrefab;

    [Header("Bed Settings")]
    public GameObject BedPrefab;

    [Header("Key Settings")]
    public GameObject KeyPrefab;

    [Header("Generation Settings")]
    int Width = 100;

    int Height = 100;

    int MinRoomSize = 10;

    int MaxRoomSize = 50;

    int MaxRooms = 500;

    public bool UseRandomSeed = true;

    bool KeySpawned;

    public int Seed = 0;

    Random Rdn;

    void GenerateDungeon()
    {
        // Clear the tilemaps
        MapTile.ClearAllTiles();

        if (WallTilemap != null)
        {
            WallTilemap.ClearAllTiles();
        }

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

        // Add grid walls dividing the map into 5 sections, but leave corridors open
        for (int i = 1; i < 5; i++)
        {
            int WallX = i * Width / 5;

            for (int y = 0; y < Height; y++)
            {
                if (Map[WallX, y] != 0) // only place wall if not already floor (corridor)
                {
                    Map[WallX, y] = 1; // vertical walls
                }
            }
        }

        for (int i = 1; i < 5; i++)
        {
            int WallY = i * Height / 5;

            for (int x = 0; x < Width; x++)
            {
                if (Map[x, WallY] != 0) // only place wall if not already floor (corridor)
                {
                    Map[x, WallY] = 1; // horizontal walls
                }
            }
        }

        // Place tiles on the tilemaps
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
                    if (WallTilemap != null)
                    {
                        WallTilemap.SetTile(Position, WallTile);
                    }

                    else
                    {
                        MapTile.SetTile(Position, WallTile);
                    }
                }
            }
        }

        // Instantiate player in the center of the first room
        if (PlayerPrefab != null && Rooms.Count > 0)
        {
            Vector2Int PlayerSpawn = new Vector2Int(Rooms[0].x + Rooms[0].width / 2,
            Rooms[0].y + Rooms[0].height / 2);

            Vector3 WorldPosition = MapTile.CellToWorld(new Vector3Int(PlayerSpawn.x,
            PlayerSpawn.y, 0));

            Instantiate(PlayerPrefab, WorldPosition, Quaternion.identity);
        }

        // Instantiate door in the center of the last room
        if (DoorPrefab != null && Rooms.Count > 0)
        {
            RectInt LastRoom = Rooms[Rooms.Count - 1];

            Vector2Int DoorSpawn = new Vector2Int(LastRoom.x + LastRoom.width / 2,
            LastRoom.y + LastRoom.height / 2);

            Vector3 WorldPosition = MapTile.CellToWorld(new Vector3Int(DoorSpawn.x,
            DoorSpawn.y, 0));

            Instantiate(DoorPrefab, WorldPosition, Quaternion.identity);
        }

        // Instantiate bed in the center of the second room
        if (BedPrefab != null && Rooms.Count > 1)
        {
            RectInt SecondRoom = Rooms[1];

            Vector2Int BedSpawn = new Vector2Int(SecondRoom.x + SecondRoom.width / 2,
            SecondRoom.y + SecondRoom.height / 2);

            Vector3 WorldPosition = MapTile.CellToWorld(new Vector3Int(BedSpawn.x,
            BedSpawn.y, 0));

            Instantiate(BedPrefab, WorldPosition, Quaternion.identity);
        }

        // Spawn aleatório de chave em uma sala aleatória (exceto player, cama e porta)
        if (!KeySpawned && KeyPrefab != null && Rooms != null && Rooms.Count > 3 && MapTile != null)
        {
            int Min = 2;

            int Max = Rooms.Count - 1;
            
            if (Max > Min)
            {
                int KeyRoomIndex = Rdn.Next(Min, Max);

                if (KeyRoomIndex >= 0 && KeyRoomIndex < Rooms.Count)
                {
                    RectInt KeyRoom = Rooms[KeyRoomIndex];

                    Vector2Int KeySpawn = new Vector2Int(KeyRoom.x + KeyRoom.width / 2,
                    KeyRoom.y + KeyRoom.height / 2);

                    Vector3 KeyWorldPos = MapTile.CellToWorld(new Vector3Int(KeySpawn.x, KeySpawn.y, 0));

                    Instantiate(KeyPrefab, KeyWorldPos, Quaternion.identity);
                    
                    KeySpawned = true;
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
            for (int dy = 0; dy < 2; dy++)
            {
                int yPos = Y + dy;

                if (yPos < Height)
                {
                    Map[x, yPos] = 0;
                }
            }
        }
    }

    void CreateVerticalCorridor(int[,] Map, int YStart, int YEnd, int X)
    {
        int Start = Mathf.Min(YStart, YEnd);

        int End = Mathf.Max(YStart, YEnd);

        for (int y = Start; y <= End; y++)
        {
            for (int dx = 0; dx < 2; dx++)
            {
                int xPos = X + dx;

                if (xPos < Width)
                {
                    Map[xPos, y] = 0;
                }
            }
        }
    }
}