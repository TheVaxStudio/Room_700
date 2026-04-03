using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

public class Procedural3DTilemapGenerator : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public GameObject FloorPrefab; // Prefab with SpriteRenderer for floor tile

    public GameObject WallPrefab;  // Prefab with SpriteRenderer for wall tile

    [Header("Object Prefabs")]
    public GameObject PlayerPrefab;

    public GameObject DoorPrefab;

    public GameObject BedPrefab;
    
    public GameObject KeyPrefab;

    [Header("Terrain Settings")]
    public Terrain SourceTerrain;

    public bool UseMinecraftTerrain = false;
    
    public float BlockHeight = 1.0f; // altura de cada bloco no eixo Y

    [Header("Generation Settings")]
    public int Width = 50;

    public int Height = 50;

    public int Depth = 3; // Number of floors

    public int MinRoomSize = 5;

    public int MaxRoomSize = 20;

    public int MaxRooms = 50;

    public bool UseRandomSeed = true;
    
    public int Seed = 0;

    Random Rdn;
    
    bool KeySpawned;

    void Start()
    {
        if (UseRandomSeed)
        {
            Seed = UnityRandom.Range(0, int.MaxValue);
        }

        Rdn = new Random(Seed);

        Generate3DDungeon();
    }

    void Generate3DDungeon()
    {
        // Create a 3D array to represent the map: 0 = floor, 1 = wall
        int[,,] Map = new int[Width, Height, Depth];

        // Initialize with walls
        for (int X = 0; X < Width; X++)
        {
            for (int Y = 0; Y < Height; Y++)
            {
                for (int Z = 0; Z < Depth; Z++)
                {
                    Map[X, Y, Z] = 1; // wall
                }
            }
        }

        // Generate rooms on each floor
        List<Room3D> Rooms = new List<Room3D>();

        for (int Floor = 0; Floor < Depth; Floor++)
        {
            for (int I = 0; I < MaxRooms / Depth; I++)
            {
                int RoomWidth = Rdn.Next(MinRoomSize, MaxRoomSize + 1);

                int RoomHeight = Rdn.Next(MinRoomSize, MaxRoomSize + 1);

                int RoomX = Rdn.Next(1, Width - RoomWidth - 1);
                
                int RoomY = Rdn.Next(1, Height - RoomHeight - 1);

                Room3D NewRoom = new Room3D(RoomX, RoomY, Floor, RoomWidth, RoomHeight);

                bool Overlaps = false;
                
                foreach (Room3D Room in Rooms)
                {
                    if (Room.Floor == Floor && NewRoom.Overlaps(Room))
                    {
                        Overlaps = true;
                        
                        break;
                    }
                }

                if (!Overlaps)
                {
                    Rooms.Add(NewRoom);

                    // Carve out the room
                    for (int X = RoomX; X < RoomX + RoomWidth; X++)
                    {
                        for (int Y = RoomY; Y < RoomY + RoomHeight; Y++)
                        {
                            Map[X, Y, Floor] = 0; // floor
                        }
                    }
                }
            }
        }

        // Connect rooms with corridors (simplified, horizontal/vertical on same floor)
        // For simplicity, connect rooms on the same floor
        var RoomsByFloor = new Dictionary<int, List<Room3D>>();

        foreach (var Room in Rooms)
        {
            if (!RoomsByFloor.ContainsKey(Room.Floor))
            {
                RoomsByFloor[Room.Floor] = new List<Room3D>();
            }

            RoomsByFloor[Room.Floor].Add(Room);
        }

        foreach (var FloorRooms in RoomsByFloor.Values)
        {
            for (int I = 1; I < FloorRooms.Count; I++)
            {
                Vector2Int PrevCenter = FloorRooms[I - 1].Center;

                Vector2Int CurrCenter = FloorRooms[I].Center;

                if (Rdn.Next(0, 2) == 0)
                {
                    CreateHorizontalCorridor3D(Map, PrevCenter.x, CurrCenter.x,
                    PrevCenter.y, FloorRooms[I - 1].Floor);
                    
                    CreateVerticalCorridor3D(Map, PrevCenter.y, CurrCenter.y,
                    CurrCenter.x, FloorRooms[I - 1].Floor);
                }

                else
                {
                    CreateVerticalCorridor3D(Map, PrevCenter.y, CurrCenter.y,
                    PrevCenter.x, FloorRooms[I - 1].Floor);
                    
                    CreateHorizontalCorridor3D(Map, PrevCenter.x, CurrCenter.x,
                    CurrCenter.y, FloorRooms[I - 1].Floor);
                }
            }
        }

        // Add stairs between floors (simple: connect centers of rooms on adjacent floors)
        for (int Floor = 0; Floor < Depth - 1; Floor++)
        {
            if (RoomsByFloor.ContainsKey(Floor) && RoomsByFloor.ContainsKey(Floor + 1))
            {
                var Room1 = RoomsByFloor[Floor][0];

                var Room2 = RoomsByFloor[Floor + 1][0];

                Vector2Int Center1 = Room1.Center;
                
                Vector2Int Center2 = Room2.Center;

                // Create a vertical corridor (stair)
                for (int Y = Mathf.Min(Center1.y, Center2.y); Y <= Mathf.Max(Center1.y, Center2.y); Y++)
                {
                    Map[Center1.x, Y, Floor] = 0;

                    Map[Center2.x, Y, Floor + 1] = 0;
                }
            }
        }

        // Instantiate tiles
        for (int X = 0; X < Width; X++)
        {
            for (int Y = 0; Y < Height; Y++)
            {
                float BaseHeight = GetTerrainHeight(X, Y);

                for (int Z = 0; Z < Depth; Z++)
                {
                    float WorldY = BaseHeight + Z * BlockHeight;

                    Vector3 Position = new Vector3(X + (SourceTerrain ?
                    SourceTerrain.transform.position.x : 0.0f),
                    WorldY, Y + (SourceTerrain ? SourceTerrain.transform.position.z : 0.0f));

                    if (Map[X, Y, Z] == 0)
                    {
                        if (FloorPrefab != null)
                        {
                            Instantiate(FloorPrefab, Position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        if (WallPrefab != null)
                        {
                            // Orient wall along Z axis
                            Instantiate(WallPrefab, Position, Quaternion.Euler(0, 90, 0));
                        }
                    }
                }
            }
        }

        // Instantiate objects (simplified, on first floor)
        if (PlayerPrefab != null && Rooms.Count > 0)
        {
            var FirstRoom = Rooms[0];

            float PlayerBaseHeight = GetTerrainHeight(FirstRoom.Center.x, FirstRoom.Center.y);
            
            Vector3 PlayerPos = new Vector3(FirstRoom.Center.x + (SourceTerrain ?
            SourceTerrain.transform.position.x : 0.0f),
            PlayerBaseHeight + FirstRoom.Floor * BlockHeight, FirstRoom.Center.y +
            (SourceTerrain ? SourceTerrain.transform.position.z : 0.0f));
            
            Instantiate(PlayerPrefab, PlayerPos, Quaternion.identity);
        }

        if (DoorPrefab != null && Rooms.Count > 0)
        {
            var LastRoom = Rooms[Rooms.Count - 1];
            
            float DoorBaseHeight = GetTerrainHeight(LastRoom.Center.x, LastRoom.Center.y);
            
            Vector3 DoorPos = new Vector3(LastRoom.Center.x + (SourceTerrain ?
            SourceTerrain.transform.position.x : 0.0f),
            DoorBaseHeight + LastRoom.Floor * BlockHeight,
            LastRoom.Center.y + (SourceTerrain ? SourceTerrain.transform.position.z : 0.0f));
            
            Instantiate(DoorPrefab, DoorPos, Quaternion.identity);
        }

        if (BedPrefab != null && Rooms.Count > 1)
        {
            var SecondRoom = Rooms[1];

            float BedBaseHeight = GetTerrainHeight(SecondRoom.Center.x, SecondRoom.Center.y);
            
            Vector3 BedPos = new Vector3(SecondRoom.Center.x + (SourceTerrain ?
            SourceTerrain.transform.position.x : 0.0f), BedBaseHeight + SecondRoom.Floor * BlockHeight,
            SecondRoom.Center.y + (SourceTerrain ? SourceTerrain.transform.position.z : 0.0f));
            
            Instantiate(BedPrefab, BedPos, Quaternion.identity);
        }

        if (!KeySpawned && KeyPrefab != null && Rooms.Count > 3)
        {
            int KeyIndex = Rdn.Next(2, Rooms.Count - 1);

            var KeyRoom = Rooms[KeyIndex];
            
            float KeyBaseHeight = GetTerrainHeight(KeyRoom.Center.x, KeyRoom.Center.y);
            
            Vector3 KeyPos = new Vector3(KeyRoom.Center.x + (SourceTerrain ? 
            SourceTerrain.transform.position.x : 0.0f),
            KeyBaseHeight + KeyRoom.Floor * BlockHeight,
            KeyRoom.Center.y + (SourceTerrain ? SourceTerrain.transform.position.z : 0.0f));

            Instantiate(KeyPrefab, KeyPos, Quaternion.identity);
            
            KeySpawned = true;
        }
    }

    float GetTerrainHeight(int x, int y)
    {
        if (!UseMinecraftTerrain || SourceTerrain == null)
        {
            return 0.0f;
        }

        var worldPos = new Vector3(x + SourceTerrain.transform.position.x, 0.0f, y +
        SourceTerrain.transform.position.z);
        
        return SourceTerrain.SampleHeight(worldPos) + SourceTerrain.transform.position.y;
    }

    void CreateHorizontalCorridor3D(int[,,] Map, int XStart, int XEnd, int Y, int Floor)
    {
        int Start = Mathf.Min(XStart, XEnd);

        int End = Mathf.Max(XStart, XEnd);
        
        for (int X = Start; X <= End; X++)
        {
            Map[X, Y, Floor] = 0;
        }
    }

    void CreateVerticalCorridor3D(int[,,] Map, int YStart, int YEnd, int X, int Floor)
    {
        int Start = Mathf.Min(YStart, YEnd);

        int End = Mathf.Max(YStart, YEnd);
        
        for (int Y = Start; Y <= End; Y++)
        {
            Map[X, Y, Floor] = 0;
        }
    }
}

public class Room3D
{
    public int X, Y, Floor, Width, Height;

    public Room3D(int X, int Y, int Floor, int Width, int Height)
    {
        this.X = X;

        this.Y = Y;

        this.Floor = Floor;

        this.Width = Width;
        
        this.Height = Height;
    }

    public Vector2Int Center => new Vector2Int(X + Width / 2, Y + Height / 2);

    public bool Overlaps(Room3D other)
    {
        return !(X + Width < other.X || other.X + other.Width < X ||
        Y + Height < other.Y || other.Y + other.Height < Y);
    }
}