using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Tommy3DMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5f; // Velocidade de movimento
    public float JumpForce = 8f; // Força do pulo
    public float Gravity = -20f; // Gravidade
    public float RotationSpeed = 10f; // Velocidade de rotação

    [Header("Ground Check")]
    public Transform GroundCheck; // Ponto para verificar chão
    public float GroundDistance = 0.4f; // Distância para detectar chão
    public LayerMask GroundMask; // Layer do chão

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    // Método para movimento programático (ex: para AI)
    public void Move(Vector3 direction, bool jump = false)
    {
        controller.Move(direction * MoveSpeed * Time.deltaTime);
        if (jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpForce * -2f * Gravity);
        }
    }
}