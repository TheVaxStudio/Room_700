using UnityEngine;
using TMPro;
using System.Collections;

public class ShowQuest01 : MonoBehaviour
{
	public TMP_Text Quest01;

	void OnTriggerEnter2D(Collider2D Player)
	{
		if (Player.CompareTag("Player"))
		{
			StartCoroutine(DisplayMission01());
		}
	}

	IEnumerator DisplayMission01()
	{
		yield return new WaitForSeconds(6.5f);

		Quest01.text = "You are inside the hotel lobby";

		yield return new WaitForSeconds(4.5f);

		Quest01.enabled = false;
	}
}