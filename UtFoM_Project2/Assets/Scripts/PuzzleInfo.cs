using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleInfo
{
    public bool PuzzleCompleted;
    public bool AlreadyComplete;
    public int ID;
    public Item PlacedItem;
    public Item RequiredItem;
}