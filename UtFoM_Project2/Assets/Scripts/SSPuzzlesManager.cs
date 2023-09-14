using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSPuzzlesManager : MonoBehaviour
{
    public List<Sprite> SSMapList;
    public List<int> CompletedIDs;
    public SpriteRenderer SpriteRendererScript;

    void Start()
    {
        List<int> CompletedIDs = new List<int>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Create a multifaceted minimap flag-tree
        if (CompletedIDs.Contains(1006))
        {
            SpriteRendererScript.sprite = SSMapList[1];
        }
        else if (CompletedIDs.Contains(1009))
        {
            SpriteRendererScript.sprite = SSMapList[2];
        }
        else if (CompletedIDs.Contains(1021))
        {
            SpriteRendererScript.sprite = SSMapList[3];  
        }
        else if (CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[4];  
        }

        if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1009))
        {
            SpriteRendererScript.sprite = SSMapList[5];
        }
        else if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1021))
        {
            SpriteRendererScript.sprite = SSMapList[6];
        }
        else if (CompletedIDs.Contains(1009) && CompletedIDs.Contains(1021))
        {
            SpriteRendererScript.sprite = SSMapList[7];
        }
        else if (CompletedIDs.Contains(1009) && CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[8];
        }
        else if (CompletedIDs.Contains(1021) && CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[9];
        }
        else if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[10];
        }

        if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1009) && CompletedIDs.Contains(1021))
        {
            SpriteRendererScript.sprite = SSMapList[11];
        }
        else if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1009) && CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[12];
        }
        else if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1021) && CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[13];
        }
        else if (CompletedIDs.Contains(1009) && CompletedIDs.Contains(1021) && CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[14];
        }

        if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1009) && CompletedIDs.Contains(1021) && 
        CompletedIDs.Contains(1999))
        {
            SpriteRendererScript.sprite = SSMapList[15];
        }
        
        foreach (PuzzleManager puzzle in FindObjectsOfType<PuzzleManager>())
        {
            if (puzzle.PuzzleCompleted && !CompletedIDs.Contains(puzzle.ID))
            {
                CompletedIDs.Add(puzzle.ID);
            }
        }
    }
}