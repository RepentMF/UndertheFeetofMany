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
        else if (TextArray.Length <= TextArrayIndex) // If all of the dialogue text has been iterated through (or if none existed)
        {
            if (DialogueBoxReference != null)
            {
                DialogueBoxReference.SetActive(false);
            }
            TextArrayIndex = 0;
            UnpauseGame();
        }
    }
    #nullable disable

    private void PauseGame()
    {
        Time.timeScale = 0;
        // GG TODO: Change button mapping here
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
        // GG TODO: Revert button mapping here
    }

    void Awake()
    {
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
