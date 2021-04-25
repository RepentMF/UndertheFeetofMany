using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
	idle,
	walk,
	attack,
	standby, //buffer state for Knockback script
	stagger,
	juggle,
	freefall
}

public class Enemy : MonoBehaviour
{
	public EnemyState currentState;
	
	public bool hit;
	public int combo;
	public int baseAtk;
	public FloatValue maxHealth;
	public FloatValue currentHealth;
	public FloatValue currentStamina;
	public float moveSpeed;
	public float homePos;
	public float currentStagger;
	public string enemyName;
	public string hitBy;

	public void Knock(float damage, bool execute)
	{
		if(combo == 0)
		{
			homePos = transform.position.y;
		}

		if(!hit)
		{
			if(execute)
			{
				float percent = (currentHealth.initialValue - currentHealth.runtimeValue) / currentHealth.initialValue;
				damage += (percent * 4);
			}
			currentHealth.runtimeValue -= damage;
			Debug.Log(damage);
		}
	}

	// public void TakeDamage(float damage)
	// {
	// 	currentHealth.runtimeValue -= damage;
	// 	Debug.Log(damage);
	// }

    // Start is called before the first frame update
    void Start()
    {
    	hit = false;
    	combo = 0;
    	currentStagger = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if(currentState == EnemyState.idle)
    	{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().gravityScale = 0.0f;
			hit = false;
			combo = 0;
			hitBy = "";
    	}

    	if(GetComponent<Rigidbody2D>().velocity.y < 0f && currentState == EnemyState.juggle)
    	{
    		currentState = EnemyState.freefall;
    		hit = false;
    	}
        
    	if(currentState == EnemyState.stagger)
    	{
			if(currentStagger < 0f)
			{
				currentState = EnemyState.idle;
				currentStagger = 0f;
			}
			else
			{
				currentStagger--;
			}
    	}
		
		if(transform.position.y < homePos && currentState == EnemyState.freefall)
		{
			currentState = EnemyState.idle;
		}

		
		if(currentHealth.runtimeValue <= 0)
		{
			this.gameObject.SetActive(false);
		}


		GetComponent<Rigidbody2D>().velocity = new Vector2 (3f, 0);
    }
}
