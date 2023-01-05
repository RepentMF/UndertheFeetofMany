using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBoxReference;
    [SerializeField] private Text DialogueTextReference;
    [SerializeField] private string[] TextArray;
    private int TextArrayIndex = 0;

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
            TextArrayIndex++;
        }
        else // If all of the dialogue text has been iterated through (or if none existed)
        {
            EndInteraction();
        }
    }

    public void EndInteraction()
    {
        if (DialogueBoxReference != null)
        {
            DialogueBoxReference.SetActive(false);
        }
        TextArrayIndex = 0;
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
