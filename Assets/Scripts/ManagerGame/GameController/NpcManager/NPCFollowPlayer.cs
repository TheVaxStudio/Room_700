using UnityEngine;

public class NPCFollowPlayer : MonoBehaviour
{
    [Header("Referências")]
    public Transform Player;
    
    public SpriteRenderer Sr;
    
    Animator Anim;

    float DetectionRange = 7.0f;

    float StartFollowDelay = 1.5f;
    
    float StopFollowDelay = 2.5f;
    
    int CurrentIndex = 0;

    float FollowTimer;

    float StopTimer;
    
    bool PlayerVisible = false;

    bool IsFollowing = false;

    Vector2 MoveDir;

    void DetectPlayer()
    {
        float Dist = Vector2.Distance(transform.position, Player.position);

        if (Dist <= DetectionRange)
        {
            if (!PlayerVisible)
            {
                FollowTimer = StartFollowDelay;

                PlayerVisible = true;
            }
        }

        else
        {
            if (PlayerVisible)
            {
                StopTimer = StopFollowDelay;

                PlayerVisible = false;
            }
        }
    }

    void HandleTimers()
    {
        if (PlayerVisible)
        {
            if (!IsFollowing)
            {
                FollowTimer -= Time.deltaTime;

                if (FollowTimer <= 0.0f)
                {
                    IsFollowing = true;

                    UpdatePath();
                }
            }
        }

        else
        {
            if (IsFollowing)
            {
                StopTimer -= Time.deltaTime;

                if (StopTimer <= 0.0f)
                {
                    IsFollowing = false;
                }
            }
        }
    }

    void UpdatePath()
    {
        CurrentIndex = 0;
    }

    void UpdateAnimation()
    {
        bool Moving = IsFollowing && MoveDir.magnitude > 0.1f;

        Anim.SetBool("IsMoving", Moving);

        if (!Moving)
        {
            return;
        }

        Anim.SetFloat("MoveX", MoveDir.x);

        Anim.SetFloat("MoveY", MoveDir.y);

        if (Mathf.Abs(MoveDir.x) > 0.01f)
        {
            Sr.flipX = MoveDir.x < 0;
        }
    }
}