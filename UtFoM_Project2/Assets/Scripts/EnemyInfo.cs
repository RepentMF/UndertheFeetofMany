using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class EnemyInfo : MonoBehaviour
{
    public bool HasBeenDefeated = false;
    public int gameCounter = 0;
    protected internal Vector3 DeathPlace;
    private bool ShouldTurnOffLight = false;
    public float LightFadeSpeed = 1f;
    public string ID;
    public int LifeBlood;

    // Script References
    private BoxCollider2D BoxCollider2DScript;
    private Light2D Light2DScript;
    private SpriteRenderer SpriteRendererScript;
    private SpriteRenderer TargetSpriteRendererScript;
    private StateManager StateManagerScript;
    public GameObject CorpseObjectScript;

    private void FadeLight()
    {
        if (TargetSpriteRendererScript)
        {
            Light2DScript.intensity = 1 - TargetSpriteRendererScript.color.a;
        }
    }
    
    void Start()
	{
		SpriteRendererScript = gameObject.GetComponent<SpriteRenderer>();
        StateManagerScript = gameObject.GetComponent<StateManager>();
	}
    
    void FixedUpdate()
    {
        gameCounter++;
        
        if (HasBeenDefeated)
        {
            if (gameCounter == 2)
            {
                Debug.Break();
                StateManagerScript.InitializeOnDeath(CorpseObjectScript);
                this.gameObject.SetActive(false);
            }
        }
    }
}