using System.Collections.Generic;
using UnityEngine;

public class AdvancedDungeonGenerator : MonoBehaviour
{
    public DungeonConfig Config;

    List<Room> Rooms;

    List<Corridor> Corridors;

    Vector2Int P;

    Vector2Int S;

    void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        Rooms = new List<Room>();

        Corridors = new List<Corridor>();

        CreateRooms();
        
        CreateCorridors();
    }

    void CreateRooms()
    {
        for (int I = 0; I < Config.RoomCount; I++)
        {
            Room R = new Room(P, S);

            R.GenerateRoom();
            
            Rooms.Add(R);
        }
    }

    void CreateCorridors()
    {
        for (int I = 0; I < Rooms.Count - 1; I++)
        {
            Corridor C = new Corridor(Rooms[I].Position, Rooms[I].Size);

            C.GenerateCorridor();
            
            Corridors.Add(C);
        }
    }
}