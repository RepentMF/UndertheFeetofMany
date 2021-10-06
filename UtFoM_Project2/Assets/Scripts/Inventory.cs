using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public string equippedWeapon;
	public List<Item> spellsList = new List<Item>();
	public List<Item> charmsList = new List<Item>();
	public List<Item> doorKeysList = new List<Item>();
	public List<Item> weaponsList = new List<Item>();
	public List<Item> diariesList = new List<Item>();
	public List<Item> keyItemsList = new List<Item>();
	public bool hasMap;
	public bool hasGem;

	public void AddItem(Item item)
	{
		switch (item.category)
		{
			case ItemCategory.Charm:
				charmsList.Add(item);
				break;
			case ItemCategory.Diary:
				diariesList.Add(item);
				break;
			case ItemCategory.DoorKey:
				doorKeysList.Add(item);
				break;
			case ItemCategory.KeyItem:
				keyItemsList.Add(item);
				break;
			case ItemCategory.Spell:
				spellsList.Add(item);
				break;
			case ItemCategory.Weapon:
				weaponsList.Add(item);
				break;
			default:
				break;
		}
	}

	public void ChangeWeapon(Item weapon)
	{
		equippedWeapon = weapon.name;
		GetComponent<PlayerMovement>().light = equippedWeapon;
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
