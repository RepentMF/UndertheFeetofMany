using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon")]
public class Weapon : Item
{
    protected internal override ItemCategory Category { get { return ItemCategory.Weapon; } }

    [Header("Light Attack Info")]
    public string LightAttackAnimationName;
    public float LightAttackAnimationTimer = -1;
    [SerializeField] protected internal float OriginalLightAttackBufferThreshold;
    public float LightAttackBufferThreshold;
    [SerializeField] protected internal float OriginalLightAttackDamage;
    public float LightAttackDamage;
    public int LightAttackCounter = 0;
    public int LightAttackCounterMax = 0;
    public List<StatusEffect> LightAttackStatusesToInflict = new List<StatusEffect>();
    public bool LightAttackExecuteDamage = false;

    [Header("Medium Attack Info")]
    public string MediumAttackAnimationName;
    public float MediumAttackAnimationTimer = -1;
    [SerializeField] protected internal float OriginalMediumAttackBufferThreshold;
    public float MediumAttackBufferThreshold;
    [SerializeField] protected internal float OriginalMediumAttackDamage;
    public float MediumAttackDamage;

    [Header("Launch Attack Info")]
    public string LaunchAttackAnimationName;
    public float LaunchAttackAnimationTimer = -1;
    [SerializeField] protected internal float OriginalLaunchAttackBufferThreshold;
    public float LaunchAttackBufferThreshold;
    [SerializeField] protected internal float OriginalLaunchAttackDamage;
    public float LaunchAttackDamage;
    public int LaunchAttackCounter = 0;
    public int LaunchAttackCounterMax = 0;
    public List<StatusEffect> LaunchAttackStatusesToInflict = new List<StatusEffect>();
    public bool LaunchAttackExecuteDamage = false;

    [Header("Heavy Attack Info")]
    public string HeavyAttackAnimationName;
    public float HeavyAttackAnimationTimer = -1;
    [SerializeField] protected internal float OriginalHeavyAttackBufferThreshold;
    public float HeavyAttackBufferThreshold;
    [SerializeField] protected internal float OriginalHeavyAttackDamage;
    public float HeavyAttackDamage;
    public int HeavyAttackCounter = 0;
    public int HeavyAttackCounterMax = 0;
    public List<StatusEffect> HeavyAttackStatusesToInflict = new List<StatusEffect>();
    public bool HeavyAttackExecuteDamage = false;

    public void ResetAttackCounters()
    {
        this.LightAttackCounter = 0;
        this.LaunchAttackCounter = 0;
        this.HeavyAttackCounter = 0;
    }
}