using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCSecretWallPuzzleManager : PuzzleManager
{
    public bool Book1Placed;
    public bool Book2Placed;

    // Script References
    private Animator AnimatorScript;
    private Collider2D Collider2DScript;
    private SpriteRenderer[] SpriteRendererScript;
    private StateManager StateManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorScript = this.gameObject.GetComponent<Animator>();
        Collider2DScript = this.gameObject.GetComponent<BoxCollider2D>();
        SpriteRendererScript = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        StateManagerScript.RemoveOnDeath = false;

        if (PuzzleCompleted)
        {
            StateManagerScript.CurrentState = ActionState.Death;
            CompletePuzzle();
        }
        else
        {
            ManageState();
        }
    }

    void ManageState()
    {
        if (StateManagerScript.CurrentState == ActionState.Idle && Book1Placed && Book2Placed)
        {
            StateManagerScript.CurrentState = ActionState.Move;
        }
        else if (StateManagerScript.CurrentState == ActionState.Move && StateManagerScript.GetCurrentAnimationTimer() <= 0.0f)
        {
            StateManagerScript.CurrentState = ActionState.Death;
            SpriteRendererScript[0].enabled = false;
            Collider2DScript.enabled = false;
        }
        else if (StateManagerScript.CurrentState == ActionState.Death && StateManagerScript.GetCurrentAnimationTimer() <= 0.0f)
        {
            CompletePuzzle();
        }

        for(int i = 0; i < FindObjectsOfType<PuzzleManager>().Length; i++)
        {
            if (FindObjectsOfType<PuzzleManager>()[i].PlacedItem != null)
            {
                if (FindObjectsOfType<PuzzleManager>()[i].ID == 1017)
                {
                    if (FindObjectsOfType<PuzzleManager>()[i].PlacedItem.Name == FindObjectsOfType<PuzzleManager>()[i].RequiredItem.Name)
                    {
                        Book1Placed = true;
                    }
                    else
                    {
                        Book1Placed = false;
                    }
                }
                else if (FindObjectsOfType<PuzzleManager>()[i].ID == 1018)
                {
                    if (FindObjectsOfType<PuzzleManager>()[i].PlacedItem.Name == FindObjectsOfType<PuzzleManager>()[i].RequiredItem.Name)
                    {
                        Book2Placed = true;
                    }
                    else
                    {
                        Book2Placed = false;
                    }
                }
                
            }
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
            StateManagerScript.CurrentState = ActionState.Death;
            CompletePuzzle();
        }
        else
        {
            ManageState();
        }
    }
}
