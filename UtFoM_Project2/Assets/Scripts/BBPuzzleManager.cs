using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPuzzleManager : PuzzleManager
{
    private int CurrentSlimeCount;
    private int InitialSlimeCount;

    // Script References
    private Animator AnimatorScript;
    private Collider2D Collider2DScript;
    private SpriteRenderer[] SpriteRendererScript; // Index 0 should be foreground gate with animations, Index 1 should be the background gate after completion
    private StateManager StateManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorScript = this.gameObject.GetComponent<Animator>();
        Collider2DScript = this.gameObject.GetComponent<Collider2D>();
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
            // Grab all the Slimes in the room
            InitialSlimeCount = FindObjectsOfType<SlimeAi>().Length;
            SpriteRendererScript[1].enabled = false; // Ensure background gate is not visible
        }
    }

    void GetCurrentSlimeCount()
    {
        CurrentSlimeCount = FindObjectsOfType<SlimeAi>().Length;
    }

    void ManageState()
    {
        if ((StateManagerScript.CurrentState == ActionState.Idle || StateManagerScript.CurrentState == ActionState.Dodge) && CurrentSlimeCount == InitialSlimeCount)
        {
            StateManagerScript.CurrentState = ActionState.Dodge;
        }
        else if (StateManagerScript.CurrentState == ActionState.Dodge && CurrentSlimeCount < InitialSlimeCount)
        {
            StateManagerScript.CurrentState = ActionState.Move;
        }
        else if (StateManagerScript.CurrentState == ActionState.Move && CurrentSlimeCount <= 0)
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
        FindObjectsOfType<RoomManager>(true)[0].ObservePuzzlesInRoom();
        PuzzleCompleted = true;
        StateManagerScript.IdleAnimationName = "BB_gate_opened";
        StateManagerScript.CurrentState = ActionState.Idle;
        Collider2DScript.enabled = false;
        SpriteRendererScript[1].enabled = true;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PuzzleCompleted)
        {
            GetCurrentSlimeCount();
            ManageState();
        }
    }
}