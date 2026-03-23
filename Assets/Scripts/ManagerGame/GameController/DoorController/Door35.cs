using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door35 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ChangeRoom());
        }
    }

    IEnumerator ChangeRoom()
    {
        yield return new WaitForSeconds(6.0f);

        SceneManager.LoadScene("Room45");
    }
}