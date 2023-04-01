using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemCategory
{
    None, // Default
    Trinket,
    Diary,
    DoorKey,
    KeyItem,
    Spell,
	Weapon
}

public abstract class Item : ScriptableObject
{
	abstract protected internal ItemCategory Category { get; }
	public string Name;
	public string Description;
	public Sprite Sprite;
}