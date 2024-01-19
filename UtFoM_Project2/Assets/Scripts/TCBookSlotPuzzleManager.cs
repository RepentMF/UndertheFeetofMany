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

    public void FixedUpdate()
    {
        if (PlacedItem != null)
        {
            GetComponent<SpriteRenderer>().sprite = PlacedItem.OverworldSprite;
        }
    }
}
