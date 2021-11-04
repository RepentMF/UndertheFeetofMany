using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemCategory
{
    None, // Default
    Charm,
    Diary,
    DoorKey,
    KeyItem,
    Spell,
	Weapon
}

public class Item : MonoBehaviour
{
	public ItemCategory category;
	public string name;
	public string description;

	public Item(string name, string description, ItemCategory category)
	{
		this.name = name;
		this.description = description;
		this.category = category;
	}

	public Item(Item item)
	{
		this.name = item.name;
		this.description = item.description;
		this.category = item.category;
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
