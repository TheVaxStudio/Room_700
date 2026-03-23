using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Room 700/TeleportTile")]
public class TeleportTile : Tile
{
    public string DestinationID;

    public string TeleportID;
}