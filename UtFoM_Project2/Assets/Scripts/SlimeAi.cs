using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAi : EnemyAi
{
    [Header("Slime Properties")]
    [SerializeField] private float MovementCooldownTimer = 1;
    [SerializeField] private float RushCooldownTimer = 1;
    [SerializeField] private float RushDirectionModifier = 1.75f;
    [SerializeField] private float RushWaitTime = 0.583f;

    /// <summary>
    /// Overridden to provide a cooldown timer on the Rush attack
    /// </summary>
    protected internal override void StartAttackOne()
    {
        base.StartAttackOne();
        SetCooldownTimer(RushCooldownTimer);
    }

    /// <summary>
    /// After a brief startup, moves the Slime according to the Direction
    /// </summary>
    protected internal override void AttackOne()
    {
        if (ActionTimer < RushWaitTime)
        {
            Rigidbody2DScript.velocity = Direction.normalized * RushDirectionModifier;
        }
    }

    /// <summary>
    /// Overridden to provide a cooldown timer on movement
    /// </summary>
    protected internal override void Move()
    {
        base.Move();
        SetCooldownTimer(MovementCooldownTimer);
    }
}
