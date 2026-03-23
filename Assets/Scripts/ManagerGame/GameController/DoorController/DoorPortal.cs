using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPortal : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Door04)
    {
        if (Door04.gameObject.tag == "Door04")
        {
            SceneManager.LoadScene("Room02");
        }
    }
}