using UnityEngine;

public class PlayerTeleportTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Player)
    {
        TeleportPoint Tp = Player.GetComponent<TeleportPoint>();

        if (Tp != null)
        {
            TeleportManager.Instance.TeleportPlayer(Tp.DestinationID, Player.transform);
        }
    }
}