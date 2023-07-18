using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Key Items")]
public class KeyItem : Item
{
    protected internal override ItemCategory Category { get { return ItemCategory.KeyItem; } }

    // KeyItem Details
}