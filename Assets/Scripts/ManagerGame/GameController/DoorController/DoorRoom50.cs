using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRoom50 : MonoBehaviour
{
    string NextScene = "Room65";

    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            StartCoroutine(NewRoom());
        }
    }

    IEnumerator NewRoom()
    {
        yield return new WaitForSeconds(8.0f);

        SceneManager.LoadScene(NextScene);
    }
}