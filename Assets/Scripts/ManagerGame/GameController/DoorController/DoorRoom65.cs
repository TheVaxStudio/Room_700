using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRoom65 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            StartCoroutine(NextRoom());
        }
    }

    IEnumerator NextRoom()
    {
        yield return new WaitForSeconds(6.0f);

        SceneManager.LoadScene("Room70");
    }
}