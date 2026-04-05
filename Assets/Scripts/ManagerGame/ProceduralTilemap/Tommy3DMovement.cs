using System.Collections;
using System.Collections.Generic;
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
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (GroundCheck == null)
        {
            GroundCheck = transform.Find("GroundCheck");
            if (GroundCheck == null)
            {
                GameObject groundCheckObj = new GameObject("GroundCheck");
                groundCheckObj.transform.parent = transform;
                groundCheckObj.transform.localPosition = Vector3.down * 0.5f;
                GroundCheck = groundCheckObj.transform;
            }
        }
    }

    void Update()
    {
        // Verificar se está no chão
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pequena força para manter no chão
        }

        // Movimento horizontal
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * MoveSpeed * Time.deltaTime);

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpForce * -2f * Gravity);
        }

        // Aplicar gravidade
        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Rotação (opcional, para câmera ou personagem)
        float mouseX = Input.GetAxis("Mouse X") * RotationSpeed;
        transform.Rotate(Vector3.up * mouseX);
    }

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