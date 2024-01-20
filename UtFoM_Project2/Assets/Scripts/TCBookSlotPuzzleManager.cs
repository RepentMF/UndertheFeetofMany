using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCBookSlotPuzzleManager : PuzzleManager
{
    public override void SetupSpecialPuzzle(bool Confirm)
    {
        if (Confirm)
        {
            FindObjectOfType<PlayerController>().SelectFromInventory();
        }
    }

    void ManageState()
    {
        if (PlacedItem != null)
        {
            if (PlacedItem.Name == RequiredItem.Name)
            {
                CompletePuzzle();
            }
        }
    }

    void CompletePuzzle()
    {
        PuzzleCompleted = true;
    }

    public void FixedUpdate()
    {
        if (PlacedItem != null)
        {
            GetComponent<SpriteRenderer>().sprite = PlacedItem.OverworldSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }

        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
        else
        {
            ManageState();
        }
    }
}
