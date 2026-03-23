using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance
    {
        get;

        set; 
    }

    [Header("Câmeras")]
    public Camera PlayerCamera;

    public Camera NpcCamera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        ActivatePlayerCamera();
    }

    public void ActivatePlayerCamera()
    {
        PlayerCamera.enabled = true;

        NpcCamera.enabled = false;
    }

    public void ActivateNPCCamera(Transform npcTarget)
    {
        NpcCamera.enabled = true;

        PlayerCamera.enabled = false;

        NpcCamera.transform.position = npcTarget.position + new Vector3(0, 1.8f, -2.5f);
        
        NpcCamera.transform.LookAt(npcTarget);
    }

    public void Release()
    {
        
    }

    public void FocusOn(Transform transform)
    {
        
    }
}