using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRoom65 : MonoBehaviour
{
    IEnumerator NextRoom()
    {
        yield return new WaitForSeconds(6.0f);

        SceneManager.LoadScene("Room70");
    }
}