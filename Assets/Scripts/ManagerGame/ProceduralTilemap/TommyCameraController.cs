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

    void Start()
    {
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Target == null)
        {
            Target = transform.parent; // Assumir que a câmera é filha do Tommy
        }
    }

    void LateUpdate()
    {
        if (Target == null) return;

        // Rotação com mouse
        float mouseX = Input.GetAxis("Mouse X") * RotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar rotação vertical

        // Aplicar rotação à câmera
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);

        // Posição desejada
        Vector3 desiredPosition = Target.position + transform.TransformVector(Offset);

        // Suavizar movimento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;
    }

    // Método para ajustar offset dinamicamente
    public void SetOffset(Vector3 newOffset)
    {
        Offset = newOffset;
    }
}