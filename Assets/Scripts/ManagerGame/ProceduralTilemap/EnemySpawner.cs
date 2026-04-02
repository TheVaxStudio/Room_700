using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace ManagerGame.ProceduralTilemap
{
    public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> EnemyPrefabs;

    [SerializeField] 
    private int EnemiesPerRoom = 1;

    [SerializeField] 
    private int MaxEnemies = 1;

    private Random Rdn;
        
    private int CurrentEnemyCount = 0;

    public void SpawnEnemies(List<RectInt> Rooms, int[,] Map, Tilemap MapTile, int Seed)
    {
        if (EnemyPrefabs == null || EnemyPrefabs.Count == 0)
        {
            return;
        }

        Rdn = new Random(Seed);
            
        CurrentEnemyCount = 0;

        // Spawn enemies in rooms (skip first room where player spawns)
        for (int i = 1; i < Rooms.Count && CurrentEnemyCount < MaxEnemies; i++)
        {
            RectInt Room = Rooms[i];

            int EnemyCount = Rdn.Next(1, EnemiesPerRoom + 1);

            for (int e = 0; e < EnemyCount && CurrentEnemyCount < MaxEnemies; e++)
            {
                // Random position inside the room
                int RoomX = Rdn.Next(Room.x + 1, Room.x + Room.width - 1);

                int RoomY = Rdn.Next(Room.y + 1, Room.y + Room.height - 1);

                Vector3 WorldPosition = MapTile.CellToWorld(new Vector3Int(RoomX, RoomY, 0));

                // Random enemy from list
                GameObject EnemyPrefab = EnemyPrefabs[Rdn.Next(0, EnemyPrefabs.Count)];

                Instantiate(EnemyPrefab, WorldPosition, Quaternion.identity, transform);
                
                CurrentEnemyCount++;
            }
        }
    }

    public void ClearEnemies()
    {
        // Remove all spawned enemies
        foreach (Transform Child in transform)
        {
            Destroy(Child.gameObject);
        }

        CurrentEnemyCount = 0;
    }
}}
