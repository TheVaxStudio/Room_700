using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Door01)
    {
        if (Door01.gameObject.tag == "Door01")
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}