using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [Header("Configuração do Teleporte")]
    public string TeleportID;

    public string DestinationID;

    void OnTriggerEnter2D(Collider2D Other)
    {
        print("Floor Corridor is inside of Corridor" + DestinationID);

        if (Other.CompareTag("Player"))
        {
            TeleportManager.Instance.TeleportPlayer(DestinationID, Other.transform);
        }

        else
        {
            Debug.LogError("You are being teleported to same tilemap" + DestinationID);
        }
    }
}