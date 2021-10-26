using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public bool magic;
	public string hitbox;
	public float damage;
	public float timer;
	public Vector2 thrust;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collider2D collision)
    {
        Debug.Break();
        GetComponentInParent<PlayerMovement>().particles.GetComponent<ParticleSystem>().enableEmission = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(magic)
        {
        	if(timer > 0.0f)
        	{
        		timer -= Time.deltaTime;
        	}
        	else
        	{
        		Destroy(this.gameObject);
        	}
        }
    }
}
