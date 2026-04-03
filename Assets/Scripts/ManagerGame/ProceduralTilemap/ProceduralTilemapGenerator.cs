using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralTilemapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    int Width = 64;

    int Height = 64;

    int MaxRooms = 12;

    int MinRoomSize = 6;
    
    int MaxRoomSize = 12;

    [Header("Tilemaps & Tiles")]
    public Tilemap MapTile;

    public Tilemap WallTilemap;

    public TileBase FloorTile;
    
    public TileBase WallTile;

    [Header("Prefabs")]
    public GameObject PlayerPrefab;

    public GameObject DoorPrefab;

    public GameObject BedPrefab;
    
    public GameObject KeyPrefab;

    [Header("Enemy Spawner")]
    public EnemySpawner EnemySpawn;

    [Header("Generation State")]
    public Transform Player;

    bool PlayerSpawned = false;

    bool DoorSpawned = false;

    bool BedSpawned = false;

    bool KeySpawned = false;

    float GenerationTimer = 0f;

    int Seed = 0;

    System.Random Rdn;

    int[,] Map;
    
    List<RectInt> Rooms;

    void Update()
    {
        GenerationTimer += Time.deltaTime;

        if (GenerationTimer >= 20.0f)
        {
            GenerationTimer = 0.0f;

            PlayerSpawned = true;
            
            DoorSpawned = true;
            
            BedSpawned = true;
            
            KeySpawned = true;
            
            StartCoroutine(GenerateDungeon());
        }
    }

    IEnumerator GenerateDungeon()
    {
        // Place tiles on the tilemaps
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                if (Map[x, y] == 0)
                {
                    if (MapTile != null && FloorTile != null)
                    {
                        MapTile.SetTile(pos, FloorTile);
                    }
                }

                else
                {
                    if (WallTilemap != null && WallTile != null)
                    {
                        WallTilemap.SetTile(pos, WallTile);
                    }

                    else if (MapTile != null && WallTile != null)
                    {
                        MapTile.SetTile(pos, WallTile);
                    }
                }
            }
        }

        // Instantiate player in the center of the first room
        if (!PlayerSpawned && PlayerPrefab != null && Rooms.Count > 0)
        {
            Vector2Int PlayerSpawn = new Vector2Int(Rooms[0].x + Rooms[0].width / 2,
            Rooms[0].y + Rooms[0].height / 2);

            Vector3 WorldPos = MapTile.CellToWorld(new Vector3Int(PlayerSpawn.x, PlayerSpawn.y, 0));

            Instantiate(PlayerPrefab, WorldPos, Quaternion.identity);
            
            PlayerSpawned = true;
        }            

        // Instantiate door in the center of the last room
        if (!DoorSpawned && DoorPrefab != null && Rooms.Count > 0)
        {
            RectInt LastRoom = Rooms[Rooms.Count - 1];

            Vector2Int DoorSpawn = new Vector2Int(LastRoom.x + LastRoom.width / 2,
            LastRoom.y + LastRoom.height / 2);

            Vector3 WorldPos = MapTile.CellToWorld(new Vector3Int(DoorSpawn.x, DoorSpawn.y, 0));

            Instantiate(DoorPrefab, WorldPos, Quaternion.identity);
            
            DoorSpawned = true;
        }

        // Instantiate bed in the center of the second room
        if (!BedSpawned && BedPrefab != null && Rooms.Count > 1)
        {
            RectInt SecondRoom = Rooms[1];

            Vector2Int BedSpawn = new Vector2Int(SecondRoom.x + SecondRoom.width / 2,
            SecondRoom.y + SecondRoom.height / 2);

            Vector3 WorldPos = MapTile.CellToWorld(new Vector3Int(BedSpawn.x, BedSpawn.y, 0));

            Instantiate(BedPrefab, WorldPos, Quaternion.identity);

            BedSpawned = true;
        }

        if (EnemySpawn != null)
        {
            EnemySpawn.ClearEnemies();

            EnemySpawn.SpawnEnemies(Rooms, Map, MapTile, Seed);
        }
        
        if (KeyPrefab != null)
        {
            int KeyRoomIndex = Rdn.Next(2, Rooms.Count - 1);

            RectInt KeyRoom = Rooms[KeyRoomIndex];

            Vector2Int KeySpawn = new Vector2Int(KeyRoom.x + KeyRoom.width / 2,
            KeyRoom.y + KeyRoom.height / 2);

            Vector3 KeyWorldPos = MapTile.CellToWorld(new Vector3Int(KeySpawn.x, KeySpawn.y, 0));

            Instantiate(KeyPrefab, KeyWorldPos, Quaternion.identity);
            
            KeySpawned = true;
        }

        yield return new WaitForSeconds(5.0f);
    }

    // Corridors (opcional, pode ser expandido)
    void CreateHorizontalCorridor(int[,] Map, int xStart, int xEnd, int y)
    {
        int Start = Mathf.Min(xStart, xEnd);

        int End = Mathf.Max(xStart, xEnd);
        
        for (int X = Start; X <= End; X++)
        {
            for (int Dy = 0; Dy < 2; Dy++)
            {
                int YPos = y + Dy;

                if (YPos < Height)
                {
                    Map[X, YPos] = 0;
                }
            }
        }
    }

    void CreateVerticalCorridor(int[,] Map, int yStart, int yEnd, int x)
    {
        int Start = Mathf.Min(yStart, yEnd);
        
        int End = Mathf.Max(yStart, yEnd);

        for (int Y = Start; Y <= End; Y++)
        {
            for (int Dx = 0; Dx < 2; Dx++)
            {
                int XPos = x + Dx;

                if (XPos < Width)
                {
                    Map[XPos, Y] = 0;
                }
            }
        }
    }
}