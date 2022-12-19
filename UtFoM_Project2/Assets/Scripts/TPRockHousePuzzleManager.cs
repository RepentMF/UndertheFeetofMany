using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPRockHousePuzzleManager : GenericSingleton<TPRockHousePuzzleManager>
{
    private bool PuzzleCompleted = false;

    // Script References
    private Collider2D Collider2DScript;
    private StateManager StateManagerScript;
    private SpriteRenderer[] SpriteRendererArrayScript;

    // Start is called before the first frame update
    void Start()
    {
        Collider2DScript = this.gameObject.GetComponent<Collider2D>();
        SpriteRendererArrayScript = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        StateManagerScript.RemoveOnDeath = false;

        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
        else
        {
            
        }
    }

    void ManageState()
    {
        if (StateManagerScript.CurrentState == ActionState.Idle || StateManagerScript.CurrentState == ActionState.Dodge)
        {
            StateManagerScript.CurrentState = ActionState.Dodge;
        }
        else if (StateManagerScript.CurrentState == ActionState.Dodge)
        {
            StateManagerScript.CurrentState = ActionState.Move;
        }
        else if (StateManagerScript.CurrentState == ActionState.Move)
        {
            StateManagerScript.CurrentState = ActionState.Death;
        }
        else if (StateManagerScript.CurrentState == ActionState.Death && StateManagerScript.GetCurrentAnimationTimer() <= 0.0f)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        PuzzleCompleted = true;
        StateManagerScript.IdleAnimationName = "";
        StateManagerScript.CurrentState = ActionState.Idle;
        Collider2DScript.enabled = false;
        //SpriteRendererArrayScript.enabled = true;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PuzzleCompleted)
        {
            ManageState();
        }
    }
}
