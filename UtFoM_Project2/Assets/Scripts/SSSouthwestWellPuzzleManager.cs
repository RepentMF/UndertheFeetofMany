using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SSSouthwestWellPuzzleManager : PuzzleManager
{
    // Start is called before the first frame update
    void Start()
    {
        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("P1") && FindObjectOfType<PlayerController>().IsInteracting)
		{  
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        PuzzleCompleted = true;
        FindObjectsOfType<RoomManager>(true)[0].ObservePuzzlesInRoom();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
    }
}
