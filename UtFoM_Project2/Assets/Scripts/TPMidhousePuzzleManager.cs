using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TPMidhousePuzzleManager : PuzzleManager
{
    public List<Sprite> TPMapList;
    public SpriteRenderer SpriteRendererScript;

    // Start is called before the first frame update
    void Start()
    {
        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("P1") && !collision.isTrigger)
		{
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        SpriteRendererScript.sprite = TPMapList[1];
        PuzzleCompleted = true;
        FindObjectsOfType<RoomManager>(true)[0].ObservePuzzlesInRoom();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PuzzleCompleted)
        {
            CompletePuzzle();
        }
    }
}