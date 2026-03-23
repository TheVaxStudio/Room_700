using UnityEngine;
using UnityEngine.Tilemaps;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField]
    Tilemap TeleportMap;

    void OnTriggerEnter2D(Collider2D Player)
    {
        HandleTeleport(Player.gameObject);
    }

    void HandleTeleport(GameObject Player)
    {
        if (Player.CompareTag("Player"))
        {
            return;
        }
    }
}