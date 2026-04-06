using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorRoom50 : MonoBehaviour
{
    string NextScene = "Room65";

    IEnumerator NewRoom()
    {
        yield return new WaitForSeconds(8.0f);

        SceneManager.LoadScene(NextScene);
    }
}