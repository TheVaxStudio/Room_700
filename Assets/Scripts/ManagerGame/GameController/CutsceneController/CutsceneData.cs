using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICutsceneAction
{
    IEnumerator Execute();
}

[System.Serializable]
public class CutsceneData
{
    public string ID;

    public List<ICutsceneAction> Actions;
}

public class NPCTalkAction : ICutsceneAction
{
    string NpcName;
    
    string Dialogue;

    public NPCTalkAction(string NpcName, string Dialogue)
    {
        this.NpcName = NpcName;

        this.Dialogue = Dialogue;
    }

    public IEnumerator Execute()
    {
        Debug.Log($"{NpcName}: \"{Dialogue}\"");

        yield return new WaitForSeconds(2f);
    }
}

public class NPCMoveAction : ICutsceneAction
{
    string NpcName;

    Vector3 TargetPosition;
    
    float Duration;

    public NPCMoveAction(string NpcName, Vector3 TargetPosition, float Duration)
    {
        this.NpcName = NpcName;

        this.TargetPosition = TargetPosition;
        
        this.Duration = Duration;
    }

    public IEnumerator Execute()
    {
        GameObject Npc = GameObject.Find(NpcName);

        if (Npc == null)
        {
            Debug.LogWarning($"Npc '{NpcName}' não encontrado!");

            yield break;
        }

        Vector3 Start = Npc.transform.position;

        float T = 0;

        while (T < 1)
        {
            T += Time.deltaTime / Duration;

            Npc.transform.position = Vector3.Lerp(Start, TargetPosition, T);

            yield return null;
        }
    }
}

public class FocusCameraOnNPCAction : ICutsceneAction
{
    string NpcName;

    public FocusCameraOnNPCAction(string NpcName)
    {
        this.NpcName = NpcName;
    }

    public IEnumerator Execute()
    {
        GameObject Npc = GameObject.Find(NpcName);

        if (Npc != null)
        {
            CameraController.Instance.FocusOn(Npc.transform);
        }

        yield return null;
    }
}

public class ReleaseCameraAction : ICutsceneAction
{
    public IEnumerator Execute()
    {
        CameraController.Instance.Release();

        yield return null;
    }
}

public class WaitAction : ICutsceneAction
{
    float WaitTime;

    public WaitAction(float WaitTime) => this.WaitTime = WaitTime;
    
    public IEnumerator Execute()
    {
        yield return WaitTime;
    }
}
