using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneNPC : MonoBehaviour
{
    //public Item NPCData;
    public float LightFadeSpeed = 1f;
    public string ID;
    [SerializeField] public List<Dialogue> AllDialouges;
    public int CurrentDialogue = 0;
    public bool SpecialInteraction;

    // Script References
    public BoxCollider2D BoxCollider2DScript;
    private UnityEngine.Rendering.Universal.Light2D Light2DScript;
    private SpriteRenderer SpriteRendererScript;
    private SpriteRenderer TargetSpriteRendererScript;
    private StateManager StateManagerScript;

    
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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FadeLight();
    }
}
