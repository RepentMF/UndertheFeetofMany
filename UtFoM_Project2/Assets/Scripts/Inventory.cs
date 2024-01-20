using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Interactable InteractableTarget;
    public Weapon EquippedWeapon;
    public List<EquippedTrinket> EquippedTrinkets = new List<EquippedTrinket>();
    public List<Item> SpellsList = new List<Item>();
    public List<Trinket> TrinketsList = new List<Trinket>();
    public List<Item> DoorKeysList = new List<Item>();
    public List<Weapon> WeaponsList = new List<Weapon>();
    public List<Item> DiariesList = new List<Item>();
    public List<Item> KeyItemsList = new List<Item>();
    public bool HasMap;
    public bool HasGem;

    // Script References
    private Stats StatsScript;

    public void AddItem(Item item)
    {
        switch (item.Category)
        {
            case ItemCategory.Trinket:
				Trinket t = (Trinket) Instantiate(item);
				SyncTrinketStatusInflicter(t.KnifeModification);
				SyncTrinketStatusInflicter(t.SwordModification);
				SyncTrinketStatusInflicter(t.HammerModification);
                TrinketsList.Add(t);
				// if (StatsScript != null) // TESTING: THIS IF BLOCK IS FOR TESTING UNTIL THE INVENTORY GUI CAN EQUIP AND UNEQUIP TRINKETS
				// {
				// 	EquipTrinket(t, StatsScript);
				// }
                break;
            case ItemCategory.Diary:
                DiariesList.Add((Diary) Instantiate(item));
                break;
            case ItemCategory.DoorKey:
                DoorKeysList.Add(Instantiate(item));
                break;
            case ItemCategory.KeyItem:
                KeyItemsList.Add(Instantiate(item));
                break;
            case ItemCategory.Spell:
                SpellsList.Add(Instantiate(item));
                break;
            case ItemCategory.Weapon:
				Weapon w = (Weapon) Instantiate(item);
                WeaponsList.Add(w);
                if (EquippedWeapon == null)
                {
                    EquippedWeapon = WeaponsList[0];
                }
				SyncHitboxes(w);
                break;
            default:
                break;
        }
    }
    
    public void RemoveItem(Item item)
    {
        switch (item.Category)
        {
            case ItemCategory.Diary:
                DiariesList.Remove(item);
                break;
            case ItemCategory.DoorKey:
                DoorKeysList.Remove(item);
                break;
            case ItemCategory.KeyItem:
                KeyItemsList.Remove(item);
                break;
            default:
                break;
        }
    }

	private void SyncTrinketStatusInflicter(TrinketModification modification)
	{
		modification.LightAttackStatusEffectToInflict.Inflicter = StatsScript;
		modification.LaunchAttackStatusEffectToInflict.Inflicter = StatsScript;
		modification.HeavyAttackStatusEffectToInflict.Inflicter = StatsScript;
	}

	private void SyncHitboxes(Weapon weapon)
	{
		Hitbox[] hitboxes = this.gameObject.GetComponentsInChildren<Hitbox>(true);
		foreach (Hitbox hitbox in hitboxes)
		{
			if (hitbox.HitboxName.IndexOf(weapon.Name) > -1)
			{
				hitbox.WeaponInfo = weapon;
			}
		}
	}

    public void ChangeWeapon(Weapon weapon)
    {
        EquippedWeapon = weapon;
    }

    public void ChangeWeaponForward()
    {
		if (EquippedWeapon == null)
			return;
		
        EquippedWeapon.ResetAttackCounters();
        if (WeaponsList.Count > 1)
        {
            int weaponIndex = WeaponsList.IndexOf(EquippedWeapon);
            if (weaponIndex < WeaponsList.Count - 1) // less than total number of weapons
            {
                EquippedWeapon = WeaponsList[weaponIndex + 1];
            }
            else // the total number of weapons
            {
                EquippedWeapon = WeaponsList[0];
            }
        }
    }

    public void ChangeWeaponBack()
    {
		if (EquippedWeapon == null)
			return;
		
        EquippedWeapon.ResetAttackCounters();
        if (WeaponsList.Count > 1)
        {
            int weaponIndex = WeaponsList.IndexOf(EquippedWeapon);
            if (weaponIndex > 0) // greater than 0
            {
                EquippedWeapon = WeaponsList[weaponIndex - 1];
            }
            else // index 0
            {
                EquippedWeapon = WeaponsList[WeaponsList.Count - 1];
            }
        }
    }

    public void EquipTrinket(Trinket trinket, Weapon target)
    {
        if (StatsScript.CurrentTrinketPoints - trinket.TrinketCost >= 0)
        {
            EquippedTrinkets.Add(new EquippedTrinket(trinket, target));
            StatsScript.CurrentTrinketPoints -= trinket.TrinketCost;
            switch (target.Name)
            {
                case "Knife":
                    EquipWeaponWithTrinket(target, trinket.KnifeModification);
                    break;
                case "Sword":
                    EquipWeaponWithTrinket(target, trinket.SwordModification);
                    break;
                case "Hammer":
                    EquipWeaponWithTrinket(target, trinket.HammerModification);
                    break;
                default:
					break;
            }
        }
    }

    private void EquipWeaponWithTrinket(Weapon weapon, TrinketModification modification)
    {
        // Light Modifications
        weapon.LightAttackCounterMax += modification.LightAttackCounterMaxModifier;
        weapon.LightAttackBufferThreshold += (modification.LightAttackBufferThresholdModifierPercentage / 100) * weapon.OriginalLightAttackBufferThreshold;
        weapon.LightAttackDamage += (modification.LightAttackDamageModifierPercentage / 100) * weapon.OriginalLightAttackDamage;
		weapon.LightAttackStatusesToInflict.Add(modification.LightAttackStatusEffectToInflict);

        // Medium Modifications
        weapon.MediumAttackBufferThreshold += (modification.LightAttackBufferThresholdModifierPercentage / 100) * weapon.OriginalMediumAttackBufferThreshold;
        weapon.MediumAttackDamage += (modification.LightAttackDamageModifierPercentage / 100) * weapon.OriginalMediumAttackDamage;

        // Launch Modifications
        weapon.LaunchAttackCounterMax += modification.LaunchAttackCounterMaxModifier;
        weapon.LaunchAttackBufferThreshold += (modification.LaunchAttackBufferThresholdModifierPercentage / 100) * weapon.OriginalLaunchAttackBufferThreshold;
        weapon.LaunchAttackDamage += (modification.LaunchAttackDamageModifierPercentage / 100) * weapon.OriginalLaunchAttackDamage;
		weapon.LaunchAttackStatusesToInflict.Add(modification.LaunchAttackStatusEffectToInflict);

        // Heavy Modifications
        weapon.HeavyAttackCounterMax += modification.HeavyAttackCounterMaxModifier;
        weapon.HeavyAttackBufferThreshold = (modification.HeavyAttackBufferThresholdModifierPercentage) * weapon.OriginalHeavyAttackBufferThreshold;
        weapon.HeavyAttackDamage += (modification.HeavyAttackDamageModifierPercentage / 100) * weapon.OriginalHeavyAttackDamage;
		weapon.HeavyAttackStatusesToInflict.Add(modification.HeavyAttackStatusEffectToInflict);
    }

    public void UnequipTrinket(Trinket trinket, Weapon target)
    {
        EquippedTrinkets.Remove(EquippedTrinkets.Find(element => element.Trinket.Name == trinket.Name && element.TargetWeapon == target));
        StatsScript.CurrentTrinketPoints += trinket.TrinketCost;
        switch (target.Name)
        {
            case "Kitchen Knife":
                UnequipWeaponWithTrinket(target, trinket.KnifeModification);
                break;
            case "Machete Sword":
                UnequipWeaponWithTrinket(target, trinket.SwordModification);
                break;
            case "Bell Hammer":
                UnequipWeaponWithTrinket(target, trinket.HammerModification);
                break;
            default:
				break;
        }
    }

    private void UnequipWeaponWithTrinket(Weapon weapon, TrinketModification modification) // GG TODO: Execute Damage needs to be done in a way that it doesn't get overriden by new trinkets
    {
        // Light Modifications
        weapon.LightAttackCounterMax -= modification.LightAttackCounterMaxModifier;
        weapon.LightAttackBufferThreshold -= (modification.LightAttackBufferThresholdModifierPercentage / 100) * weapon.OriginalLightAttackBufferThreshold;
        weapon.LightAttackDamage -= (modification.LightAttackDamageModifierPercentage / 100) * weapon.OriginalLightAttackDamage;
		weapon.LightAttackStatusesToInflict.Remove(modification.LightAttackStatusEffectToInflict);

        // Medium Modifications
        weapon.MediumAttackBufferThreshold -= (modification.LightAttackBufferThresholdModifierPercentage / 100) * weapon.OriginalMediumAttackBufferThreshold;
        weapon.MediumAttackDamage -= (modification.LightAttackDamageModifierPercentage / 100) * weapon.OriginalMediumAttackDamage;

        // Launch Modifications
        weapon.LaunchAttackCounterMax -= modification.LaunchAttackCounterMaxModifier;
        weapon.LaunchAttackBufferThreshold -= (modification.LaunchAttackBufferThresholdModifierPercentage / 100) * weapon.OriginalLaunchAttackBufferThreshold;
        weapon.LaunchAttackDamage -= (modification.LaunchAttackDamageModifierPercentage / 100) * weapon.OriginalLaunchAttackDamage;
		weapon.LaunchAttackStatusesToInflict.Remove(modification.LaunchAttackStatusEffectToInflict);

        // Heavy Modifications
        weapon.HeavyAttackCounterMax -= modification.HeavyAttackCounterMaxModifier;
        weapon.HeavyAttackBufferThreshold = (modification.HeavyAttackBufferThresholdModifierPercentage) * weapon.OriginalHeavyAttackBufferThreshold;
        weapon.HeavyAttackDamage -= (modification.HeavyAttackDamageModifierPercentage / 100) * weapon.OriginalHeavyAttackDamage;
		weapon.HeavyAttackStatusesToInflict.Remove(modification.HeavyAttackStatusEffectToInflict);
    }

    public void EquipTrinket(Trinket trinket, Stats target)
    {
        if (StatsScript.CurrentTrinketPoints - trinket.TrinketCost >= 0)
        {
            EquippedTrinkets.Add(new EquippedTrinket(trinket, target));
            StatsScript.CurrentTrinketPoints -= trinket.TrinketCost;
			EquipStatsWithTrinket(StatsScript, trinket.StatsModification);
        }
    }

    private void EquipStatsWithTrinket(Stats stats, TrinketModification modification)
    {
		// Health
        stats.MaxHealth += modification.MaxHealthModifier;

		// Stamina
        stats.MaxStamina += modification.MaxStaminaModifier;

		// Mana
        stats.MaxMana += modification.MaxManaModifier;

		// Lifeblood
        stats.MaxLifeblood += modification.MaxLifebloodModifier;

		// Trinket Points
        stats.MaxTrinketPoints += modification.MaxTrinketModifier;
    }

    public void UnequipTrinket(Trinket trinket, Stats target)
    {
		EquippedTrinkets.Remove(EquippedTrinkets.Find(element => element.Trinket.Name == trinket.Name && element.TargetStats == target));
		StatsScript.CurrentTrinketPoints += trinket.TrinketCost;
		UnequipStatsWithTrinket(StatsScript, trinket.StatsModification);
    }

    private void UnequipStatsWithTrinket(Stats stats, TrinketModification modification)
    {
		// Health
        stats.MaxHealth -= modification.MaxHealthModifier;
		stats.CurrentHealth = stats.CurrentHealth > stats.MaxHealth ? stats.MaxHealth : stats.CurrentHealth;

		// Stamina
        stats.MaxStamina -= modification.MaxStaminaModifier;
		stats.CurrentStamina = stats.CurrentStamina > stats.MaxStamina ? stats.MaxStamina : stats.CurrentStamina;
		
		// Mana
        stats.MaxMana -= modification.MaxManaModifier;
		stats.CurrentMana = stats.CurrentMana > stats.MaxMana ? stats.MaxMana : stats.CurrentMana;

		// Lifeblood
        stats.MaxLifeblood -= modification.MaxLifebloodModifier;
		stats.CurrentLifeblood = stats.CurrentLifeblood > stats.MaxLifeblood ? stats.MaxLifeblood : stats.CurrentLifeblood;

		// Trinket Points
        stats.MaxTrinketPoints -= modification.MaxTrinketModifier;
		stats.CurrentTrinketPoints = stats.CurrentTrinketPoints > stats.MaxTrinketPoints ? stats.MaxTrinketPoints : stats.CurrentTrinketPoints; // GG TODO: This needs to be fixed so that adding a 0 cost trinket that increases the max, filling up trinkets to that new max, and then remving the 0 cost trinket doesn't cause the player to have a higher trinket count than their now lowered max
    }

    void Awake()
    {
        StatsScript = this.gameObject.GetComponent<Stats>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
