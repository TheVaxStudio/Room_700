using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("Teleport Localization")]

    Transform PortalMap;

    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.CompareTag("Player"))
        {
            Player.gameObject.transform.position = PortalMap.position;
        }
    }
}