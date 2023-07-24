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
        this.gameObject.GetComponent<SceneNPC>().AllDialouges = ExtraDialouge1;
        PuzzleCompleted = true;
        FindObjectsOfType<RoomManager>(true)[0].ObservePuzzlesInRoom();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (FindObjectsOfType<RoomManager>().ObservePuzzlesInRoom)
        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
    }
}