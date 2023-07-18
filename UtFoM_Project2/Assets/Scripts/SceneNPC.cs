using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNPC : MonoBehaviour
{
    //public Item NPCData;
    public float LightFadeSpeed = 1f;
    public string ID;

    // Script References
    public BoxCollider2D BoxCollider2DScript;
    private UnityEngine.Rendering.Universal.Light2D Light2DScript;
    private SpriteRenderer SpriteRendererScript;
    private SpriteRenderer TargetSpriteRendererScript;
    private StateManager StateManagerScript;

    /*private void Initialize(Item newData)
    {
        ItemData = newData;
        if (SpriteRendererScript.sprite == null)
        {
            SpriteRendererScript.sprite = ItemData.Sprite;
        }
        BoxCollider2DScript.size = SpriteRendererScript.size;
    }

#nullable enable
    public void PickUp(Inventory? inventory)
    {
        if (inventory != null)
        {
            inventory.AddItem(ItemData);
        }
        if (StateManagerScript != null)
        {
            StateManagerScript.CurrentState = ActionState.Death;
        }
    }*/
#nullable disable

    private void FadeLight()
    {
        if (TargetSpriteRendererScript)
        {
            Light2DScript.intensity = 1 - TargetSpriteRendererScript.color.a;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // If a roof has entered the object's space
        if (collider.GetComponent<RoofFader>() != null)
        {
            TargetSpriteRendererScript = collider.GetComponent<SpriteRenderer>();
        }
    }

    void Awake()
    {
        Light2DScript = this.gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        SpriteRendererScript = this.gameObject.GetComponent<SpriteRenderer>();
        // StateManagerScript = this.gameObject.GetComponentInChildren<StateManager>();
        // GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
 
    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize(NPCData);
        //StateManagerScript.CurrentState = ActionState.Move;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FadeLight();
    }
}
