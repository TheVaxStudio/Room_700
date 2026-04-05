using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Referências")]
    public Transform Player;

    Rigidbody2D Rb;
    
    Animator Anim;

    SpriteRenderer SpriteRenderer;

    [Header("Movimento")]
    float MoveSpeed = 2.5f;

    float FollowDistance = 0.5f;

    float Smoothness = 0.15f;

    [Header("Desvio Inteligente")]
    bool SmartAvoidance = true;
    
    public LayerMask ObstacleMask;

    float ObstacleCheckDistance = 0.8f;

    float AvoidanceStrength = 0.5f;

    Vector2 MoveDir;

    Vector2 LastMoveDir;

    const float FrameTime = 1.0f / 60.0f;

    float AnimationTimer;
    
    bool UpdatingFlip;

    void FollowPlayer()
    {
        Vector2 TargetPos = (Vector2)Player.position - Vector2.up * FollowDistance;

        MoveTowards(TargetPos);
    }

    void MoveTowards(Vector2 TargetPos)
    {
        Vector2 ToTarget = TargetPos - (Vector2)transform.position;

        Vector2 DesiredDir = ToTarget.normalized;

        if (SmartAvoidance)
        {
            RaycastHit2D Hit = Physics2D.Raycast(transform.position, DesiredDir,
            ObstacleCheckDistance, ObstacleMask);

            if (Hit.collider != null)
            {
                Vector2 Avoid = Vector2.Perpendicular(Hit.normal);

                DesiredDir = Vector2.Lerp(DesiredDir, Avoid, AvoidanceStrength);
            }
        }

        MoveDir = Vector2.Lerp(MoveDir, DesiredDir, Smoothness);

        Rb.linearVelocity = MoveDir * MoveSpeed;

        LastMoveDir = MoveDir;
    }

    void UpdateAnimator()
    {
        Vector2 Vel = Rb.linearVelocity;

        float Speed = Vel.magnitude;

        Anim.SetFloat("MoveX", Vel.x);

        Anim.SetFloat("MoveY", Vel.y);
        
        Anim.SetFloat("Speed", Speed);

        bool Moving = Speed > 0.05f;

        Anim.SetBool("IsMoving", Moving);

        if (!Moving)
        {
            Anim.SetFloat("LastMoveX", LastMoveDir.x);

            Anim.SetFloat("LastMoveY", LastMoveDir.y);
        }
    }

    void UpdateFlip()
    {
        if (UpdatingFlip || SpriteRenderer == null)
        {
            return;
        }

        if (Mathf.Abs(MoveDir.x) > 0.05f)
        {
            SpriteRenderer.flipX = MoveDir.x < 0;
        }
    }
}