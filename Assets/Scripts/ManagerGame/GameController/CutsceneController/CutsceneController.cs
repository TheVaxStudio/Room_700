using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector[] Directors;

    string NextScene = "Room20";

    bool HasPlayed = false;

    bool SceneChanged = false;

    float Timer = 0.0f;

    float MaxTime = 60.0f;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}