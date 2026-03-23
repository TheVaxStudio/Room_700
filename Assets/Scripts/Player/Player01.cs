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

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        
        float y = Input.GetAxisRaw("Vertical");

        RawInput = new Vector2(x, y).normalized;

        Vector2 AnimDir = Vector2.zero;

        if (RawInput.sqrMagnitude > 0.01f)
        {
            if (Mathf.Abs(RawInput.x) > Mathf.Abs(RawInput.y))
            {
                AnimDir = new Vector2(Mathf.Sign(RawInput.x), 0.0f);
            }

            else
            {
                AnimDir = new Vector2(0.0f, Mathf.Sign(RawInput.y));
            }

            LastFaceDir = AnimDir;
        }

        SmoothAnimDir = Vector2.Lerp(SmoothAnimDir, AnimDir,
        DirectionSmoothness * Time.deltaTime);

        Anim.SetFloat("Horizontal", SmoothAnimDir.x);

        Anim.SetFloat("Vertical", SmoothAnimDir.y);

        Anim.SetFloat("Speed", CurrentVelocity.magnitude);

        Anim.SetFloat("LastHorizontal", LastFaceDir.x);
        
        Anim.SetFloat("LastVertical", LastFaceDir.y);
    }

    void FixedUpdate()
    {
        float Mult = 1.0f;

        if (Input.GetKey(SprintKey))
        {
            Mult = SprintMultiplier;
        }

        else if (Input.GetKey(RunKey))
        {
            Mult = RunMultiplier;
        }
        
        float TargetSpeed = WalkSpeed * Mult;

        Vector2 TargetVelocity = RawInput * TargetSpeed;

        if (RawInput.sqrMagnitude > 0.01f)
        {
            CurrentVelocity = Vector2.Lerp(CurrentVelocity, TargetVelocity,
            Acceleration * Time.fixedDeltaTime);
        }

        else
        {
            CurrentVelocity = Vector2.Lerp(CurrentVelocity, Vector2.zero,
            Deceleration * Time.fixedDeltaTime);
        }

        Rb.MovePosition(Rb.position + CurrentVelocity * Time.fixedDeltaTime);
    }
}