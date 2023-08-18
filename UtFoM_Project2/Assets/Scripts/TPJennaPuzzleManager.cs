using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class TPJennaPuzzleManager : PuzzleManager
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
        string[] extraDialogue1 = ExtraDialouge1[0].dialogue;
        this.gameObject.GetComponent<Interactable>().TextArray = extraDialogue1;
        this.gameObject.GetComponent<SceneNPC>().CurrentDialogue = this.gameObject.GetComponent<SceneNPC>().AllDialouges.Count - 1;
        PuzzleCompleted = true;
        FindObjectsOfType<RoomManager>(true)[0].ObservePuzzlesInRoom();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isabelleFlag = bool.Parse((string) (FindObjectOfType<RoomManager>().PuzzleTable["1003"]));
        
        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
        else if (isabelleFlag)
        {
            this.gameObject.GetComponent<Interactable>().TextArrayIndex = 0;
            PuzzleCompleted = true;
        }
    }
}