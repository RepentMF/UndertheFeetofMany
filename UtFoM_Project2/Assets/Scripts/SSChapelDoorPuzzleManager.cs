using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SSChapelDoorPuzzleManager : PuzzleManager
{
    BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        PuzzleCompleted = true;
        FindObjectsOfType<RoomManager>(true)[0].ObservePuzzlesInRoom();
        box = this.gameObject.GetComponent<BoxCollider2D>();
        box.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //bool elisiaFlag = bool.Parse((string) (FindObjectOfType<RoomManager>().PuzzleTable["1009"]));
        bool elisiaFlag = false;

        foreach (PuzzleManager pm in FindObjectsOfType<PuzzleManager>())
        {
            if (pm.ID == 1009)
            {
                elisiaFlag = pm.PuzzleCompleted;
            }
        }
        //Debug.Log(elisiaFlag);

        if (PuzzleCompleted && !AlreadyComplete)
        {
            CompletePuzzle();
        }
        else if (elisiaFlag)
        {
            PuzzleCompleted = true;
            AlreadyComplete = true;
        }
    }
}
