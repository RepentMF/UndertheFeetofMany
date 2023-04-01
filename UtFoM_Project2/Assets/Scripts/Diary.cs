using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Diary")]
public class Diary : Item
{
    protected internal override ItemCategory Category { get { return ItemCategory.Diary; } }
}