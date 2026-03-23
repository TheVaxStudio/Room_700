using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRoom20 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Room35");
        }
    }
}