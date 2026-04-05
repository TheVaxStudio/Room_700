using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door45 : MonoBehaviour
{
    IEnumerator ChangeRoom()
    {
        yield return new WaitForSeconds(6.0f);

        SceneManager.LoadScene("Room50");
    }
}