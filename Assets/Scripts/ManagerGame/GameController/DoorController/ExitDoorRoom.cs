using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorRoom : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Door01)
    {
        if (Door01.gameObject.tag == "ExitRoom01")
        {
            SceneManager.LoadScene("Room02");
        }
    }
}