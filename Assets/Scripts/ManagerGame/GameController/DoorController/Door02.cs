using UnityEngine;
using UnityEngine.SceneManagement;

public class Door02 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Door06)
    {
        if (Door06.gameObject.tag == "Door06")
        {
            SceneManager.LoadScene("Room08");
        }
    }
}