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

    void Update()
    {
        if (!HasPlayed)
        {
            for (int i = 0; i < Directors.Length; i++)
            {
                if (Directors[i] != null)
                {
                    Directors[i].Play();
                }
            }

            HasPlayed = true;
        }

        if (!SceneChanged)
        {
            Timer += Time.deltaTime;

            bool AllFinished = true;

            for (int i = 0; i < Directors.Length; i++)
            {
                if (Directors[i] != null && Directors[i].state == PlayState.Playing)
                {
                    AllFinished = false;

                    break;
                }
            }

            if (AllFinished)
            {
                LoadNextScene();
            }

            if (Timer >= MaxTime)
            {
                LoadNextScene();
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}