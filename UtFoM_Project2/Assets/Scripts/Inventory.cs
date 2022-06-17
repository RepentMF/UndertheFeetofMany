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

	// Script references
	public ParticleSystem ParticleSystemScript;

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

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
