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
    public int CorpseCounter = 0;

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
        if (gameCounter < 3)
        {
            gameCounter++;
        }

        if(HasBeenDefeated && StateManagerScript.GetCurrentAnimationTimer() <= 0.0f && 
            StateManagerScript.GetCurrentAnimationTimer() != 0.0f)
        {
            DeathPlace = this.gameObject.transform.position;
            FindObjectsOfType<RoomManager>(true)[0].ObserveEnemiesInRoom();
            Debug.Break();
            StateManagerScript.InitializeOnDeath(CorpseObjectScript);
        }
        else if (HasBeenDefeated && gameCounter == 2)
        {
            this.gameObject.transform.position = DeathPlace;
            StateManagerScript.InitializeOnDeath(CorpseObjectScript);
            Destroy(this.gameObject);
        }
        

        if(this.gameObject.GetComponent<Stats>().CurrentHealth <= 0)
        {
            HasBeenDefeated = true;
        }
    }
}