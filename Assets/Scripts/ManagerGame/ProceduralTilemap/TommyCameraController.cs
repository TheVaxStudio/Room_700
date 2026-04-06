using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyCameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform Target; // O transform do Tommy

    [Header("Camera Settings")]
    public Vector3 Offset = new Vector3(0f, 2f, -5f); // Offset da câmera em relação ao alvo
    public float SmoothSpeed = 0.125f; // Velocidade de suavização
    public float RotationSpeed = 3f; // Velocidade de rotação com mouse
    public bool LockCursor = true; // Bloquear cursor do mouse

    private float xRotation = 0f;
    private float yRotation = 0f;

    // Método para ajustar offset dinamicamente
    public void SetOffset(Vector3 newOffset)
    {
        Offset = newOffset;
    }
}