using System.Collections.Generic;
using KwaaktjePathfinder2D;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAdvancedMovement : MonoBehaviour
{
    [Header("Referencias")]
    public Transform Player;

    public Tilemap WalkableTilemap;

    public SpriteRenderer Sr;

    [Header("Movimento")]
    public float MoveSpeed = 2.5f;

    public float StopDistance = 0.1f;

    public float RepathInterval = 0.35f;

    public float DetectionRange = 12.0f;

    [Header("Pathfinding")]
    public bool AllowFourDirectionalMovement = true;

    Animator Anim;

    Rigidbody2D Rb;

    Pathfinder2D Pathfinder;

    readonly List<Vector2Int> CurrentPath = new List<Vector2Int>();

    float RepathTimer;

    Vector2 CurrentMoveDir;

    Vector2 LastMoveDir = Vector2.down;

    Vector3 CurrentTargetWorld;

    bool HasPath;

    public Tilemap WallsTilemap;

    public Tilemap NotWalkableTilemap;

    void BuildPathfinder()
    {
        if (WalkableTilemap == null)
        {
            return;
        }

        Dictionary<Vector2Int, float> WeightedMap =
        new Dictionary<Vector2Int, float>();

        BoundsInt Bounds = WalkableTilemap.cellBounds;

        foreach (Vector3Int Cell in Bounds.allPositionsWithin)
        {
            if (!WalkableTilemap.HasTile(Cell))
            {
                continue;
            }

            WeightedMap[(Vector2Int)Cell] = 1.0f;
        }

        if (WeightedMap.Count == 0)
        {
            return;
        }

        NodeConnectionType ConnectionType = AllowFourDirectionalMovement
        ? NodeConnectionType.RectangleWithDiagonals
        : NodeConnectionType.RectangleNoDiagonals;

        Pathfinder = new Pathfinder2D(WeightedMap, ConnectionType);
    }

    void RecalculatePath()
    {
        if (Pathfinder == null)
        {
            return;
        }

        Vector2Int StartCell =
        (Vector2Int)WalkableTilemap.WorldToCell(transform.position);
        
        Vector2Int EndCell =
        (Vector2Int)WalkableTilemap.WorldToCell(Player.position);

        Pathfinder2DResult Result = Pathfinder.FindPath(StartCell, EndCell);

        if (Result.Status != Pathfinder2DResultStatus.Success || Result.Path.Count == 0)
        {
            ClearPath();

            return;
        }

        CurrentPath.Clear();

        for (int I = Result.Path.Count - 1; I >= 0; I--)
        {
            CurrentPath.Add(Result.Path[I]);
        }

        SetNextTarget();
    }

    void MoveAlongPath()
    {
        if (!HasPath)
        {
            StopMoving();

            return;
        }

        Vector2 ToTarget = CurrentTargetWorld - transform.position;

        if (ToTarget.magnitude <= StopDistance)
        {
            SetNextTarget();

            if (!HasPath)
            {
                StopMoving();

                return;
            }

            ToTarget = CurrentTargetWorld - transform.position;
        }

        CurrentMoveDir = ToTarget.normalized;

        LastMoveDir = CurrentMoveDir;
    }

    void ApplyMovement()
    {
        if (Rb == null)
        {
            return;
        }

        Rb.linearVelocity = CurrentMoveDir * MoveSpeed;
    }

    void SetNextTarget()
    {
        if (CurrentPath.Count == 0)
        {
            HasPath = false;

            return;
        }

        Vector2Int NextCell = CurrentPath[0];
        
        CurrentPath.RemoveAt(0);

        Vector3 CellCenter =
        WalkableTilemap.GetCellCenterWorld((Vector3Int)NextCell);

        CurrentTargetWorld = new Vector3(CellCenter.x,
        CellCenter.y, transform.position.z);
        
        HasPath = true;
    }

    void StopMoving()
    {
        CurrentMoveDir = Vector2.zero;

        if (Rb != null)
        {
            Rb.linearVelocity = Vector2.zero;
        }

        UpdateAnimation();
    }

    void ClearPath()
    {
        CurrentPath.Clear();

        HasPath = false;
    }

    void UpdateAnimation()
    {
        if (Anim == null)
        {
            if (Sr != null && Mathf.Abs(CurrentMoveDir.x) > 0.01f)
            {
                Sr.flipX = CurrentMoveDir.x < 0.0f;
            }

            return;
        }

        bool IsMoving = CurrentMoveDir.sqrMagnitude > 0.001f;

        Anim.SetBool("IsMoving", IsMoving);

        Anim.SetFloat("MoveX", CurrentMoveDir.x);

        Anim.SetFloat("MoveY", CurrentMoveDir.y);

        Anim.SetFloat("LastMoveX", LastMoveDir.x);

        Anim.SetFloat("LastMoveY", LastMoveDir.y);
        
        Anim.SetFloat("Speed", MoveSpeed);

        if (Sr != null && Mathf.Abs(CurrentMoveDir.x) > 0.01f)
        {
            Sr.flipX = CurrentMoveDir.x < 0.0f;
        }
    }
}