using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public Weapon EquippedWeapon;
	public List<Item> SpellsList = new List<Item>();
	public List<Item> CharmsList = new List<Item>();
	public List<Item> DoorKeysList = new List<Item>();
	public List<Weapon> WeaponsList = new List<Weapon>();
	public List<Item> DiariesList = new List<Item>();
	public List<Item> KeyItemsList = new List<Item>();
	public bool HasMap;
	public bool HasGem;

	public void AddItem(Item item)
	{
		switch (item.Category)
		{
			case ItemCategory.Charm:
				CharmsList.Add(item.Clone());
				break;
			case ItemCategory.Diary:
				DiariesList.Add(item.Clone());
				break;
			case ItemCategory.DoorKey:
				DoorKeysList.Add(item.Clone());
				break;
			case ItemCategory.KeyItem:
				KeyItemsList.Add(item.Clone());
				break;
			case ItemCategory.Spell:
				SpellsList.Add(item.Clone());
				break;
			case ItemCategory.Weapon:
				WeaponsList.Add((Weapon)item.Clone());
				if (EquippedWeapon == null)
				{
					EquippedWeapon = WeaponsList[0];
				}
				break;
			default:
				break;
		}
	}

	public void ChangeWeapon(Weapon weapon)
	{
		EquippedWeapon = weapon;
	}

	public void ChangeWeaponForward()
	{
		if (WeaponsList.Count > 1)
		{
			int weaponIndex = WeaponsList.IndexOf(EquippedWeapon);
			if (weaponIndex < WeaponsList.Count - 1) // less than total number of weapons
			{
				EquippedWeapon = WeaponsList[weaponIndex +  1];
			}
			else // the total number of weapons
			{
				EquippedWeapon = WeaponsList[0];
			}
		}
	}

	public void ChangeWeaponBack()
	{
		if (WeaponsList.Count > 1)
		{
			int weaponIndex = WeaponsList.IndexOf(EquippedWeapon);
			if (weaponIndex > 0) // greater than 0
			{
				EquippedWeapon = WeaponsList[weaponIndex -  1];
			}
			else // index 0
			{
				EquippedWeapon = WeaponsList[WeaponsList.Count - 1];
			}
		}
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
