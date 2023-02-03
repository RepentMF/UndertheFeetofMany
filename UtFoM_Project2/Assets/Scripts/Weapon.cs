using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : Item
{
    [Header("Light Attack Info")]
    public string LightAttackAnimationName;
    public float LightAttackAnimationTimer = -1;
    public float LightAttackBufferThreshold;

    [Header("Medium Attack Info")]
    public string MediumAttackAnimationName;
    public float MediumAttackAnimationTimer = -1;
    public float MediumAttackBufferThreshold;

    [Header("Launch Attack Info")]
    public string LaunchAttackAnimationName;
    public float LaunchAttackAnimationTimer = -1;
    public float LaunchAttackBufferThreshold;

    [Header("Heavy Attack Info")]
    public string HeavyAttackAnimationName;
    public float HeavyAttackAnimationTimer = -1;
    public float HeavyAttackBufferThreshold;

    public override Item Clone()
    {
        Weapon weapon = ScriptableObject.CreateInstance<Weapon>();
        weapon.Name = this.Name;
        weapon.Description = this.Description;
        weapon.Sprite = this.Sprite;
        weapon.Category = this.Category;
        weapon.LightAttackAnimationName = this.LightAttackAnimationName;
        weapon.LightAttackAnimationTimer = this.LightAttackAnimationTimer;
        weapon.LightAttackBufferThreshold = this.LightAttackBufferThreshold;
        weapon.MediumAttackAnimationName = this.MediumAttackAnimationName;
        weapon.MediumAttackAnimationTimer = this.MediumAttackAnimationTimer;
        weapon.MediumAttackBufferThreshold = this.MediumAttackBufferThreshold;
        weapon.LaunchAttackAnimationName = this.LaunchAttackAnimationName;
        weapon.LaunchAttackAnimationTimer = this.LaunchAttackAnimationTimer;
        weapon.LaunchAttackBufferThreshold = this.LaunchAttackBufferThreshold;
        weapon.HeavyAttackAnimationName = this.HeavyAttackAnimationName;
        weapon.HeavyAttackAnimationTimer = this.HeavyAttackAnimationTimer;
        weapon.HeavyAttackBufferThreshold = this.HeavyAttackBufferThreshold;
        return weapon;
    }
}