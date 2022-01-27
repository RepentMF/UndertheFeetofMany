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

public abstract class Item : ScriptableObject
{
	public ItemCategory Category;
	public string Name;
	public string Description;
	public Sprite Sprite;

	public abstract Item Clone();
}
