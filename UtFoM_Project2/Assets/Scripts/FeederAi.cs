using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederAi : EnemyAi
{
    [Header("Feeder Properties")]
    [SerializeField] private float RushDirectionModifier = 5f;
    [SerializeField] private float RushWaitTime = 0.583f;

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
    /// The Feeder stands still to devour
    /// </summary>
    protected internal override void SpellOne()
    {
        Rigidbody2DScript.velocity = Vector3.zero;
    }
}
