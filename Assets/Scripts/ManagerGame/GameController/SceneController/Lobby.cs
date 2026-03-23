using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    string Room01 = "Room01";
    
    void OnCollisionEnter2D(Collision2D LobbyDoor)
    {
        if (LobbyDoor.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(Room01);
        }
    }
}