using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [Header("Configurações do Portal")]
    [SerializeField]
    GameObject player;
    
    [SerializeField]
    TeleportData teleportDataScript;

    [SerializeField]
    float Cooldown = 0.5f;

    float lastTeleportTime = -999f;

    bool Teleported;

    void Teleport(GameObject portalEntered)
    {
        if (Teleported)
        {
            return;
        }

        if (Time.time - lastTeleportTime < Cooldown)
        {
            return;
        }

        GameObject destination = teleportDataScript.GetDestination(portalEntered);

        if (destination != null && player != null)
        {
            player.transform.position = destination.transform.position;

            lastTeleportTime = Time.time;

            Teleported = true;
        }
    }
}