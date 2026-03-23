using UnityEngine;
using UnityEngine.Tilemaps;

public class TeleportArea : MonoBehaviour
{
    Tilemap Map;

    void Awake()
    {
        Map = GetComponent<Tilemap>();
    }

    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.CompareTag("Player"))
        {
            Vector3Int CellPos = Map.WorldToCell(Player.transform.position);

            TileBase Tile = Map.GetTile(CellPos);

            if (Tile is TeleportTile TilePortal)
            {
                TeleportManager.Instance.TeleportPlayer(TilePortal.DestinationID, Player.transform);
            }
        }
    }
}