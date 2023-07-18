using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSPuzzlesManager : MonoBehaviour
{
    public List<Sprite> SSMapList;
    public SpriteRenderer SpriteRendererScript;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Create a multifaceted minimap flag-tree
        List<int> CompletedIDs = new List<int>();

        foreach (PuzzleManager puzzle in FindObjectsOfType<PuzzleManager>())
        {
            if (puzzle.PuzzleCompleted)
            {
                CompletedIDs.Add(puzzle.ID);
            }
        }

        switch (CompletedIDs.Count)
        {
            case 1:
                if (CompletedIDs.Contains(1006))
                {
                    SpriteRendererScript.sprite = SSMapList[1];
                }
                else if (CompletedIDs.Contains(1007))
                {
                    SpriteRendererScript.sprite = SSMapList[2];
                }
                else if (CompletedIDs.Contains(1021))
                {
                    SpriteRendererScript.sprite = SSMapList[3];  
                }
                else if (CompletedIDs.Contains(1008))
                {
                    SpriteRendererScript.sprite = SSMapList[4];  
                }
                break;
            case 2:
                if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1007))
                {
                    SpriteRendererScript.sprite = SSMapList[5];
                }
                else if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1021))
                {
                    SpriteRendererScript.sprite = SSMapList[6];
                }
                else if (CompletedIDs.Contains(1007) && CompletedIDs.Contains(1021))
                {
                    SpriteRendererScript.sprite = SSMapList[7];
                }
                else if (CompletedIDs.Contains(1007) && CompletedIDs.Contains(1008))
                {
                    SpriteRendererScript.sprite = SSMapList[8];
                }
                else if (CompletedIDs.Contains(1021) && CompletedIDs.Contains(1008))
                {
                    SpriteRendererScript.sprite = SSMapList[9];
                }
                else if (CompletedIDs.Contains(1006) && CompletedIDs.Contains(1008))
                {
                    SpriteRendererScript.sprite = SSMapList[10];
                }
                break;
            case 3:
                if (!CompletedIDs.Contains(1008))
                {
                    SpriteRendererScript.sprite = SSMapList[11];
                }
                else if (!CompletedIDs.Contains(1021))
                {
                    SpriteRendererScript.sprite = SSMapList[12];
                }
                else if (!CompletedIDs.Contains(1007))
                {
                    SpriteRendererScript.sprite = SSMapList[13];
                }
                else if (!CompletedIDs.Contains(1006))
                {
                    SpriteRendererScript.sprite = SSMapList[14];
                }
                break;
            case 4:
                SpriteRendererScript.sprite = SSMapList[15];
                break;
        }
    }
}