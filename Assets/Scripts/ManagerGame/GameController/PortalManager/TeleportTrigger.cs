using UnityEngine;
using UnityEngine.Tilemaps;

public class TeleportTrigger : MonoBehaviour
{
    void HandleTeleport(GameObject Player)
    {
        if (Player.CompareTag("Player"))
        {
            return;
        }
    }
}