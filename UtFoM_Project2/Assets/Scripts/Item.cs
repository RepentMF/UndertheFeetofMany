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

    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
