using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour
{
    KeyCode InteractKey = KeyCode.F;

    string NextSceneName = "Lobby";

    string RoomScene = "Room01";

    bool IsInteracting = false;

    float Speed;

    public List<Door> NearbyDoors = new List<Door>();

    void Update()
    {
        if (Input.GetKeyDown(InteractKey) && !IsInteracting)
        {
            if (NearbyDoors.Count > 0)
            {
                StartCoroutine(InteractWithDoor(NearbyDoors[0]));
            }
        }
    }

    IEnumerator InteractWithDoor(Door door)
    {
        IsInteracting = true;

        if (door != null && !door.IsOpen)
        {
            yield return new WaitForSeconds(0.2f);

            door.OpenDoor();

            switch (door.gameObject.tag)
            {
                case "Door01":
                    yield return new WaitForSeconds(1f);

                    SceneManager.LoadScene(NextSceneName);

                    break;

                case "Door02":
                    Debug.Log("Door02 opened.");

                    break;

                case "Door03":
                    Debug.Log("Door03 opened.");

                    yield return new WaitForSeconds(1.0f);

                    SceneManager.LoadScene(RoomScene);

                    break;
                
                case "Door04":
                    Debug.Log("Door04 opened");

                    break;

                default:
                    Debug.LogWarning("Unknown door tag: " + door.gameObject.tag);

                    break;
            }
        }

        IsInteracting = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Door door = collision.gameObject.GetComponent<Door>();

        if (door != null && !NearbyDoors.Contains(door))
        {
            switch (collision.gameObject.tag)
            {
                case "Door01":
                case "Door02":
                case "Door03":
                case "Door04":
                    NearbyDoors.Add(door);
                    
                    break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Door door = collision.gameObject.GetComponent<Door>();

        if (door != null && NearbyDoors.Contains(door))
        {
            switch (collision.gameObject.tag)
            {
                case "Door01":
                case "Door02":
                case "Door03":
                case "Door04":
                    NearbyDoors.Remove(door);

                    break;
            }
        }
    }
}