using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class TPGarfieldPuzzleManager : PuzzleManager
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
        FindObjectOfType<PlayerController>().GetComponent<Stats>().HealHealth(100f);
        PuzzleCompleted = false;
    }

    void IncompletePuzzle()
    {
        Interactable interactable = this.gameObject.GetComponent<Interactable>();

        string[] allDialogue1 = GetComponent<SceneNPC>().AllDialouges[0].dialogue;
        interactable.TextArray = allDialogue1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Interactable interactable = this.gameObject.GetComponent<Interactable>();

        // if (FindObjectOfType<PlayerController>().GetComponent<Stats>().CurrentHealth == 
        //     FindObjectOfType<PlayerController>().GetComponent<Stats>().MaxHealth)
        // {
        //     PuzzleCompleted = false;
        // }
        // else if (FindObjectOfType<PlayerController>().GetComponent<Stats>().CurrentHealth < 
        //     FindObjectOfType<PlayerController>().GetComponent<Stats>().MaxHealth)
        // {
        //     PuzzleCompleted = true;
        // }
        
        // if (PuzzleCompleted && interactable.TextArrayIndex == interactable.TextArray.Length - 1)
        // {
        //     CompletePuzzle();
        // }
        // else
        // {
        //     IncompletePuzzle();
        // }
    }
}