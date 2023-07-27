using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class TPGarfieldPuzzleManager : PuzzleManager
{
    [SerializeField] public List<Dialogue> ExtraDialouge1;
    public bool canHeal;

    // Start is called before the first frame update
    void Start()
    {

    }

    void CompletePuzzle()
    {
        FindObjectOfType<PlayerController>().GetComponent<Stats>().HealHealth(100f);
        PuzzleCompleted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Interactable interactable = this.gameObject.GetComponent<Interactable>();

        if (FindObjectOfType<PlayerController>().GetComponent<Stats>().CurrentHealth == 
            FindObjectOfType<PlayerController>().GetComponent<Stats>().MaxHealth)
        {
            string[] extraDialogue2 = ExtraDialouge1[1].dialogue;
            interactable.TextArray = extraDialogue2;
            PuzzleCompleted = false;
        }
        else if (FindObjectOfType<PlayerController>().GetComponent<Stats>().CurrentHealth < 
            FindObjectOfType<PlayerController>().GetComponent<Stats>().MaxHealth)
        {
            string[] extraDialogue1 = ExtraDialouge1[0].dialogue;
            interactable.TextArray = extraDialogue1;
            PuzzleCompleted = true;
        }
        
        if (PuzzleCompleted && interactable.TextArrayIndex == interactable.TextArray.Length)
        {
            CompletePuzzle();
            interactable.TextArrayIndex++;
        }
    }
}