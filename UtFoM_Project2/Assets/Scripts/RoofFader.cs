using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofFader : MonoBehaviour
{
    public float FadeSpeed = 1f;
    private bool ShouldFadeObject = false;

    // Script References
    private SpriteRenderer SpriteRendererScript;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // If a player has entered the object's space
        if (collider.GetComponent<PlayerMovement>() != null)
        {
            ShouldFadeObject = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // If a player has exited the object's space
        if (collider.GetComponent<PlayerMovement>() != null)
        {
            ShouldFadeObject = false;
        }
    }

    private void FadeObjectOut()
    {
        if (SpriteRendererScript.color.a > 0f)
        {
            float alphaValue = SpriteRendererScript.color.a;
            alphaValue -= Time.deltaTime * FadeSpeed;
            SpriteRendererScript.color = new Color(SpriteRendererScript.color.r, SpriteRendererScript.color.g, SpriteRendererScript.color.b, alphaValue);
        }
    }

    private void FadeObjectIn()
    {
        if (SpriteRendererScript.color.a < 1f)
        {
            float alphaValue = SpriteRendererScript.color.a;
            alphaValue += Time.deltaTime * FadeSpeed;
            SpriteRendererScript.color = new Color(SpriteRendererScript.color.r, SpriteRendererScript.color.g, SpriteRendererScript.color.b, alphaValue);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRendererScript = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ShouldFadeObject)
        {
            FadeObjectOut();
        }
        else
        {
            FadeObjectIn();
        }
    }
}
