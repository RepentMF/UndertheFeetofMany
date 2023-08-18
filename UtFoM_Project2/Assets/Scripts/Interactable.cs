using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBoxReference;
    [SerializeField] private Text DialogueTextReference;
    [SerializeField] public string[] TextArray;
    public int TextArrayIndex = 0;
    public bool IsQuestion;

    // Script References
    StateManager StateManagerScript;

#nullable enable
    public void Interact(Inventory inventory = null)
    {
        // If there is dialogue text to iterate through, display that text
        if (DialogueBoxReference != null && DialogueTextReference != null && TextArray.Length > 0 && TextArray.Length > TextArrayIndex)
        {
            PauseGame();
            DialogueBoxReference.SetActive(false); // Remove any previously active text boxes
            DialogueBoxReference.SetActive(true); // Display the current text box
            DialogueTextReference.text = TextArray[TextArrayIndex];
            
            foreach (Transform child in DialogueBoxReference.GetComponentInChildren<Transform>())
            {
                if (child.tag == "Decision")
                {
                    child.gameObject.SetActive(IsQuestion);
                }
            }
            //Debug.Log(TextArray.Length + " " + TextArrayIndex);
            TextArrayIndex++;
        }
        else // If all of the dialogue text has been iterated through (or if none existed)
        {
            if (IsQuestion)
            {
                // Scene Transition is present on Interactable
                if (GetComponentInChildren<SceneTransition>() != null)
                {
                    GetComponentInChildren<SceneTransition>().EnterFromDialogue(FindObjectOfType<PlayerController>().DecideConfirmDeny());
                }
            }
            EndInteraction();
        }
    }

    public void EndInteraction()
    {
        if (DialogueBoxReference != null)
        {
            DialogueBoxReference.SetActive(false);
        }
        if (this.gameObject.GetComponent<SceneNPC>() != null)
        {
            SceneNPC tempNPC = this.gameObject.GetComponent<SceneNPC>();

            if (tempNPC.SpecialInteraction)
            {
                TextArrayIndex = 0;
            }
            else if (tempNPC.AllDialouges.Count == 0)
            {
                TextArrayIndex = TextArray.Length - 1;
            }
            else if (tempNPC.CurrentDialogue != tempNPC.AllDialouges.Count - 1)
            {
                TextArray = tempNPC.AllDialouges[tempNPC.CurrentDialogue + 1].dialogue;
                tempNPC.CurrentDialogue++;
                TextArrayIndex = 0;
            }
            else
            {
                TextArrayIndex = TextArray.Length - 1;
            }
        }
        else
        {
            TextArrayIndex = 0;
        }
        UnpauseGame();
    }

    private void PauseGame()
    {
        GameStateManager.Instance.StartInteracting();
    }

    private void UnpauseGame()
    {
        GameStateManager.Instance.StartGameplay();
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Interacting;
    }

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
 
    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void Start()
    {
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        if (DialogueBoxReference == null && DialogueTextReference == null)
        {
            DialogueBoxReference = GameObject.FindGameObjectsWithTag("DialogueBox")[1];
            DialogueTextReference = GameObject.FindGameObjectsWithTag("Dialogue")[1].gameObject.GetComponent<Text>();
        }    
    }
}
