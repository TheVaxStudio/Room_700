using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Door : MonoBehaviour
{
    string LobbyScene = "Lobby";

    public bool IsOpen = false;

    readonly KeyCode E = KeyCode.E;

    void Update()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (!IsOpen && Input.GetKeyDown(E))
        {
            IsOpen = true;

            StartCoroutine(ToLobbyScene());
        }
    }

    IEnumerator ToLobbyScene()
    {
        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene(LobbyScene);
    }

    public void Open()
    {
        throw new NotImplementedException();
    }
}