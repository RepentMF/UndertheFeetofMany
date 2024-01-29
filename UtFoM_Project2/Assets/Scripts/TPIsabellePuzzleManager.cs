using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class TPIsabellePuzzleManager : PuzzleManager
{
    [SerializeField] public List<Dialogue> ExtraDialouge1;

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
        FindObjectsOfType<RoomManager>(true)[0].NewObservePuzzlesInRoom();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Interactable interactable = this.gameObject.GetComponent<Interactable>();
        //if (FindObjectsOfType<RoomManager>().ObservePuzzlesInRoom)
        if (!PuzzleCompleted && interactable.TextArrayIndex == interactable.TextArray.Length - 1)
        {
            CompletePuzzle();
        }
        else if (PuzzleCompleted && interactable.TextArrayIndex < interactable.TextArray.Length - 1)
        {
            interactable.TextArrayIndex++;
        }
    }
}