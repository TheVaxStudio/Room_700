using UnityEngine;
using UnityEngine.SceneManagement;

public class Door03 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Room15");
        }
    }
}