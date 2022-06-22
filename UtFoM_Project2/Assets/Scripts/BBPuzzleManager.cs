using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPuzzleManager : MonoBehaviour
{
    // Set variables for use
    public bool puzzleAComplete;
    public bool closed;
    public int currentSlimeCount;
    public int initSlimeCount;
    public Animator AnimatorScript;
    public Collider2D TilemapCollider;
    public SpriteRenderer BackGate;

    // Start is called before the first frame update
    void Start()
    {
        // Grab all the Slimes in the room
        puzzleAComplete = false;
        closed = true;
        currentSlimeCount = FindObjectsOfType<SlimeAi>().Length;
        initSlimeCount = currentSlimeCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentSlimeCount == 0 && closed)
        {
            puzzleAComplete = true;
            // Make animation of door open
            AnimatorScript.Play("BB_gate_fall");
            closed = false;
        }
        else
        {
            if (currentSlimeCount != FindObjectsOfType<SlimeAi>().Length)
            {
                currentSlimeCount = FindObjectsOfType<SlimeAi>().Length;
            }
        }

        if (currentSlimeCount > 0 && currentSlimeCount < initSlimeCount)
        {
            AnimatorScript.Play("BB_gate_drip");
        }

        if (!closed)
        {
            // WHY WON'T THE FUCKING BB_GATE_4 STAY SETACTIVE(TRUE)?!?!?!
            // Play the room, kill all the slimes, and watch the door open 
            // and you'll know what I mean- please help with troubleshooting
            TilemapCollider.gameObject.SetActive(false);
            BackGate.gameObject.SetActive(true);
        }
    }
}
