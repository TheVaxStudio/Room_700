using UnityEngine;

public class Player01 : MonoBehaviour
{
    [Header("Teclas")]
    KeyCode RunKey = KeyCode.LeftShift;

    KeyCode SprintKey = KeyCode.LeftControl;

    [Header("Velocidades")]
    float WalkSpeed = 2.5f;

    float RunMultiplier = 1.8f;
    
    float SprintMultiplier = 2.8f;

    [Header("Suavização")]
    float Acceleration = 10.0f;

    float Deceleration = 14.0f;
    
    float DirectionSmoothness = 12.0f;

    Rigidbody2D Rb;
    
    Vector2 RawInput;
    
    Vector2 CurrentVelocity;
    
    Vector2 LastFaceDir = Vector2.down;

    Vector2 SmoothAnimDir;

    Animator Anim;
}