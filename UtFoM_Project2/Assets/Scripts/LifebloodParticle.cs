using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebloodParticle : MonoBehaviour
{
    public float LifebloodRegen;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.isTrigger)
        {
            if (collider.GetComponent<PlayerController>())
            {
                collider.GetComponent<Stats>().RegenLifeblood(LifebloodRegen);
                Destroy(this.gameObject);
            }
        }
        
    }
}
