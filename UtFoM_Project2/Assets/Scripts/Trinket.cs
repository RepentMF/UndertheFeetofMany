using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EquippedTrinket
{
    public Trinket Trinket { get; set; }
    public Weapon TargetWeapon { get; set; }
    public Stats TargetStats { get; set; }

    public EquippedTrinket(Trinket trinket, Weapon targetWeapon)
    {
        Trinket = trinket;
        TargetWeapon = targetWeapon;
        TargetStats = null;
    }

    public EquippedTrinket(Trinket trinket, Stats targetStats)
    {
        Trinket = trinket;
        TargetWeapon = null;
        TargetStats = targetStats;
    }
}

[System.Serializable]
public class TrinketModification
{
    // Stats Modifiers
    public float MaxHealthModifier = 0;
    public float MaxStaminaModifier = 0;
    public float MaxManaModifier = 0;
    public float MaxLifebloodModifier = 0;
    public int MaxTrinketModifier = 0;

    // Light Attack Modifiers
    public int LightAttackCounterMaxModifier = 0;
    public int LightAttackDamageModifierPercentage = 0;
    public int LightAttackBufferThresholdModifierPercentage = 0;
    public StatusEffect LightAttackStatusEffectToInflict;
    public bool LightAttackExecuteDamage = false;

    // Launch Attack Modifiers
    public int LaunchAttackCounterMaxModifier = 0;
    public int LaunchAttackDamageModifierPercentage = 0;
    public int LaunchAttackBufferThresholdModifierPercentage = 0;
    public StatusEffect LaunchAttackStatusEffectToInflict;
    public bool LaunchAttackExecuteDamage = false;

    // Heavy Attack Modifiers
    public int HeavyAttackCounterMaxModifier = 0;
    public int HeavyAttackDamageModifierPercentage = 0;
    public int HeavyAttackBufferThresholdModifierPercentage = 0;
    public StatusEffect HeavyAttackStatusEffectToInflict;
    public bool HeavyAttackExecuteDamage = false;

}


[CreateAssetMenu(menuName = "Scriptable Objects/Trinket")]
public class Trinket : Item
{
    protected internal override ItemCategory Category { get { return ItemCategory.Trinket; } }

    // Trinket Stats
    public int TrinketCost = 0;

    [Header("Knife Attack Modification")]
    public TrinketModification KnifeModification;

    [Header("Sword Attack Modification")]
    public TrinketModification SwordModification;

    [Header("Hammer Attack Modification")]
    public TrinketModification HammerModification;

    [Header("Stats Modification")]
    public TrinketModification StatsModification;
}