using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    string SceneName = "Room20";

    float TimeToChangeScene = 10.0f;

    bool StartedCoroutine = false;

    void Update()
    {
        if (!StartedCoroutine)
        {
            StartedCoroutine = true;

            StartCoroutine(ChangeSceneAfterTime());
        }
    }

    IEnumerator ChangeSceneAfterTime()
    {
        yield return new WaitForSeconds(TimeToChangeScene);

        SceneManager.LoadScene(SceneName);
    }
}