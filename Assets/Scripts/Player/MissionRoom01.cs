using UnityEngine;
using TMPro;
using System.Collections;

public class MissionRoom01 : MonoBehaviour
{
    public TMP_Text QuestRoom01;

    IEnumerator DisplayMissionRoom01()
	{
		yield return new WaitForSeconds(6.5f);

		QuestRoom01.text = "Find the Door to next Room";

		yield return new WaitForSeconds(4.5f);

		QuestRoom01.enabled = false;
	}
}