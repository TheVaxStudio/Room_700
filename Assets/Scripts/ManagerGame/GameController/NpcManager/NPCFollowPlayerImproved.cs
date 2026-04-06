using UnityEngine;

public class NPCFollowPlayerImproved : MonoBehaviour
{
    [Header("Referências")]
    public Transform Player;

    public SpriteRenderer Sr;

    // Adicione as paredes em uma Layer chamada "Paredes" e selecione-a no Inspector
    public LayerMask WallLayer; 
    
    Animator Anim;

    [Header("Configurações")]
    public float Speed = 2.0f;

    public float DetectionRange = 7.0f;

    public float StartFollowDelay = 1.5f;

    public float StopFollowDelay = 2.5f;

    public float DistanceFromWalls = 0.7f; // Ajustado para ser levemente maior que o corpo

    float FollowTimer;

    float StopTimer;

    bool PlayerVisible = false;

    bool IsFollowing = false;

    bool WallAhead = false; // Começa como falso
    
    string AnimName;

    Vector2 MoveDir;

    void DetectPlayer()
    {
        float Dist = Vector2.Distance(transform.position,
        Player.position);

        if (Dist <= DetectionRange)
        {
            if (!PlayerVisible)
            {
                PlayerVisible = true;

                FollowTimer = StartFollowDelay;
            }
        }

        else if (PlayerVisible)
        {
            PlayerVisible = false;

            StopTimer = StopFollowDelay;
        }
    }

    void HandleTimers()
    {
        if (PlayerVisible && !IsFollowing)
        {
            FollowTimer -= Time.deltaTime;

            if (FollowTimer <= 0.0f)
            {
                IsFollowing = true;
            }
        }

        else if (!PlayerVisible && IsFollowing)
        {
            StopTimer -= Time.deltaTime;

            if (StopTimer <= 0.0f)
            {
                IsFollowing = false;
            }
        }
    }

    void HandleMovement()
    {
        if (IsFollowing)
        {
            MoveDir = (Player.position - transform.position).normalized;

            // Chama a detecção usando a distância configurada
            DetectWallAhead(DistanceFromWalls);

            // CORREÇÃO: SE NÃO (!) houver parede, ele anda.
            if (!WallAhead)
            {
                transform.position += (Vector3)MoveDir 
                * Speed * Time.deltaTime;
            }

            else
            {
                // Se bater na parede, paramos o movimento para a animação de Idle entrar
                MoveDir = Vector2.zero;
            }
        }

        else
        {
            MoveDir = Vector2.right;
        }
    }

    void DetectWallAhead(float Distance)
    {
        // O Raycast agora ignora o próprio NPC usando a WallLayer
        RaycastHit2D Hit = Physics2D.Raycast(transform.position,
        MoveDir, Distance, WallLayer);
        
        WallAhead = Hit.collider != null;

        // Linha visual para você ver o sensor funcionando no Editor
        Debug.DrawRay(transform.position, MoveDir 
        * Distance, WallAhead ? Color.red : Color.green);
    }

    void UpdateAnimation()
    {
        bool Moving = MoveDir.sqrMagnitude > 0.01f;

        Anim.SetBool("IsMoving", Moving);

        if (!Moving)
        {
            return;
        }

        // CORREÇÃO: Lógica para decidir se a animação é Horizontal ou Vertical
        if (Mathf.Abs(MoveDir.x) > Mathf.Abs(MoveDir.y))
        {
            AnimName = (MoveDir.x > 0) ? "WalkRight" 
            : "WalkLeft";
        }

        else
        {
            AnimName = (MoveDir.y > 0) ? "WalkUp" 
            : "WalkDown";
        }

        Anim.Play(AnimName);
    }
}