using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SceneItem : MonoBehaviour // GG TODO: Add voip animation to scene items
{
    public Item ItemData;
    protected internal bool HasBeenPickedUp;
    private bool ShouldTurnOffLight = false;
    public float LightFadeSpeed = 1f;
    public string ID;

    // Script References
    private BoxCollider2D BoxCollider2DScript;
    private Light2D Light2DScript;
    private SpriteRenderer SpriteRendererScript;
    private SpriteRenderer TargetSpriteRendererScript;
    private StateManager StateManagerScript;

    private void Initialize(Item newData)
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
        HasBeenPickedUp = true;
        if (StateManagerScript != null)
        {
            StateManagerScript.CurrentState = ActionState.Death;
        }
        else
        {            
            this.gameObject.SetActive(false);
        }
    }
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
        BoxCollider2DScript = this.gameObject.GetComponent<BoxCollider2D>();
        Light2DScript = this.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        SpriteRendererScript = this.gameObject.GetComponent<SpriteRenderer>();
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
 
    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {

        if (HasBeenPickedUp)
        {
            this.gameObject.SetActive(false);
            //GameObject.Destroy(this.gameObject);
        }
        else
        {
            Initialize(ItemData);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FadeLight();
    }
}
