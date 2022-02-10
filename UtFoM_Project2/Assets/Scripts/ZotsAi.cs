using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZotsAi : EnemyAi
{

    [Header("Zots Properties")]
    [SerializeField] private float DevourCooldownTimer = 1;
    [SerializeField] private float DevourDirectionModifier = 1.75f;
    [SerializeField] private float ShriekCooldownTimer = 1;

    protected internal override void StartSpellOne()
    {
    	base.StartSpellOne();
    	SetCooldownTimer(DevourCooldownTimer);
    }

    protected internal override void SpellOne()
    {
        Rigidbody2DScript.velocity = Direction.normalized * DevourDirectionModifier;
    }

    protected internal override void StartSpellTwo()
    {
    	base.StartSpellTwo();
    	SetCooldownTimer(ShriekCooldownTimer);
    }

    protected internal override void SpellTwo()
    {
        Rigidbody2DScript.velocity = Vector3.zero;
    }
}
