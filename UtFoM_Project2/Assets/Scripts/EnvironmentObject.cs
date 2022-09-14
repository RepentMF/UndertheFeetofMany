using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    public float changeHalf;

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
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
        if (changeHalf == -1)
        {
            changeHalf = 1.5f;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        // If a player has entered the enemy's space
        if (collider.GetComponent<PlayerController>() != null)
        {
            if (this.gameObject.transform.position.y < collider.bounds.center.y + changeHalf)
            {
                GetComponentInParent<SpriteRenderer>().sortingLayerName = "ArtLayer_Foreground";
            } 
            else
            {
                GetComponentInParent<SpriteRenderer>().sortingLayerName = "ArtLayer_Background";
            }
        }
    }
}
