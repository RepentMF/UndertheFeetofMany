using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyInfo : MonoBehaviour
{
    public bool HasBeenDefeated = false;
    protected internal Vector3 DeathPlace;
    private bool ShouldTurnOffLight = false;
    public float LightFadeSpeed = 1f;
    public string ID;

    // Script References
    private BoxCollider2D BoxCollider2DScript;
    private Light2D Light2DScript;
    private SpriteRenderer SpriteRendererScript;
    private SpriteRenderer TargetSpriteRendererScript;
    private StateManager StateManagerScript;

    public void HaveIBeenDefeated()
    {
        if (GetComponent<Stats>().CurrentHealth <= 0f)
        {
            HasBeenDefeated = true;
            DeathPlace = this.gameObject.transform.position;
        }
        else
        {
            HasBeenDefeated = false;
        }
    }

    private void FadeLight()
    {
        if (TargetSpriteRendererScript)
        {
            Light2DScript.intensity = 1 - TargetSpriteRendererScript.color.a;
        }
    }

    void Start()
    {
        if (HasBeenDefeated)
        {
            // set sprite to static corpse sprite TODO
            transform.position = DeathPlace;
        }
    }

    void FixedUpdate()
    {
        if (HasBeenDefeated)
        {
            // set sprite to static corpse sprite TODO
            transform.position = DeathPlace;
        }
        else
        {
            HaveIBeenDefeated();
        }
    }
}