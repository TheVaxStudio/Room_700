using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    Dictionary<string, Vector3> TeleportPoints = new Dictionary<string, Vector3>();

    [System.Obsolete]
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Tilemap[] Tilemaps = FindObjectsOfType<Tilemap>();

        foreach (Tilemap Map in Tilemaps)
        {
            RegisterTeleportTiles(Map);
        }
    }

    void RegisterTeleportTiles(Tilemap MapTile)
    {
        foreach (var Pos in MapTile.cellBounds.allPositionsWithin)
        {
            TileBase Tile = MapTile.GetTile(Pos);

            if (Tile is TeleportTile TilePortal)
            {
                Vector3 WorldPos = MapTile.CellToWorld(Pos) + MapTile.cellSize / 2f;

                if (!TeleportPoints.ContainsKey(TilePortal.TeleportID))
                {
                    TeleportPoints.Add(TilePortal.TeleportID, WorldPos);
                }
            }
        }
    }

    public void TeleportPlayer(string DestinationID, Transform Player)
    {
        if (TeleportPoints.TryGetValue(DestinationID, out Vector3 Dest))
        {
            Player.position = Dest;
        }

        else
        {
            Debug.LogWarning("Destino não encontrado: " + DestinationID);
        }
    }
}