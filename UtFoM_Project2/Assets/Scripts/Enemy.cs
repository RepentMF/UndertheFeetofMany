using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
	idle,
	walk,
	attack,
	stagger,
	juggle,
	freefall
}

public class Enemy : MonoBehaviour
{
	public EnemyState currentState;
	public FloatValue maxHealth;
	public bool hit;
	public int combo;
	public int baseAtk;
	public float health;
	public float moveSpeed;
	public float homePos;
	public float currentStagger;
	public string enemyName;
	public string hitBy;

	public void Knock(float damage)
	{
		if(combo == 0)
		{
			homePos = transform.position.y;
		}

		if(!hit)
		{
			TakeDamage(damage);
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if(health <= 0)
		{
			this.gameObject.SetActive(false);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
    	hit = false;
    	combo = 0;
    	currentStagger = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    	if(GetComponent<Rigidbody2D>().velocity.y < 0f && currentState == EnemyState.juggle)
    	{
    		currentState = EnemyState.freefall;
    		hit = false;
    	}
        
    	if(currentState == EnemyState.stagger)
    	{
			if(currentStagger < 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				GetComponent<Rigidbody2D>().gravityScale = 0.0f;
				currentState = EnemyState.idle;
				hitBy = "";
				hit = false;
				combo = 0;			
			}
			else
			{
				Debug.Break();
				currentStagger--;
			}
    	}
		else if(transform.position.y < homePos && currentState == EnemyState.freefall)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().gravityScale = 0.0f;
			currentState = EnemyState.idle;
			hitBy = "";
			hit = false;
			combo = 0;
		}
    }
}
