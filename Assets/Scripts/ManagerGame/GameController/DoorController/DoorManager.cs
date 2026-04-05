using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
	public float InteractDistance = 2.0f;

    public GameObject Player;

	public List<GameObject> Doors;

    public void TryOpenDoor()
    {
        foreach (GameObject Door in Doors)
        {
			if (Door == null)
			{
				continue;
			}
				
            float Distance = Vector2.Distance(Player.transform.position, Door.transform.position);
			
            if (Distance <= InteractDistance)
			{
                Door.SetActive(false);

				print("Door opened!");

				return;
			}
        }
    }
}