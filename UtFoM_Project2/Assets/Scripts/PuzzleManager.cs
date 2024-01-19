using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public bool PuzzleCompleted;
    public bool AlreadyComplete;
    public int ID;
    public Item PlacedItem;
    public Item RequiredItem;

    public virtual void SetupSpecialPuzzle(bool Confirm)
    {}

    public virtual void ExecuteSpecialPuzzle()
    {}
}
