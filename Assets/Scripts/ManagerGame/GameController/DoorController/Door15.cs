using UnityEngine;
using UnityEngine.SceneManagement;

public class Door15 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("CutsceneRoom20");
        }
    }
}