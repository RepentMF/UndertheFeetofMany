﻿using System.Collections;
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
	public int baseWeight;
	public int currentWeight;
	public FloatValue maxHealth;
	public FloatValue currentHealth;
	public FloatValue currentStamina;
	public float moveSpeed;
	public float home;
	public float currentStagger;
	public string enemyName;
	public string hitBy;


	public Rigidbody2D rigidbody;
	public Transform target;
	public Transform homePos;
	public float chaseRadius;
	public float attackRadius;
	public Animator animator;

	void CheckDistance()
	{
		if(Vector3.Distance(target.position, transform.position) <= chaseRadius && 
			Vector3.Distance(target.position, transform.position) > attackRadius)
		{
			if(currentState == EnemyState.idle || currentState == EnemyState.walk &&
				currentState != EnemyState.stagger)
			{
				Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
				rigidbody.MovePosition(temp);
				ChangeAnim(temp - transform.position);
				ChangeState(EnemyState.walk);
				animator.SetBool("awake", true);
			}
		}
		else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
		{
			animator.SetBool("awake", false);
		}
	}

	private void ChangeState(EnemyState newState)
	{
		if(currentState != newState)
		{
			currentState = newState;
		}
	}

	private void SetAnimFloat(Vector2 vec)
	{
		animator.SetFloat("moveX",  vec.x);
		animator.SetFloat("moveY", vec.y);
	}

	private void ChangeAnim(Vector2 dir)
	{
		if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
		{
			if(dir.x > 0)
			{
				SetAnimFloat(Vector2.right);
			}
			else if(dir.x < 0)
			{
				SetAnimFloat(Vector2.left);
			}
		}
		else if(Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
		{
			if(dir.y > 0)
			{
				SetAnimFloat(Vector2.up);
			}
			else if(dir.y < 0)
			{
				SetAnimFloat(Vector2.down);
			}
		}
	}
	public void Knock(float damage, bool execute)
	{
		if(combo == 0)
		{
			home = transform.position.y;
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
    	currentWeight = baseWeight;


		currentState = EnemyState.idle;
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		target = GameObject.FindWithTag("Player").transform;
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
		
		if(transform.position.y < home && currentState == EnemyState.freefall)
		{
			currentState = EnemyState.idle;
		}

		
		if(currentHealth.runtimeValue <= 0)
		{
			this.gameObject.SetActive(false);
		}

		CheckDistance();

		//GetComponent<Rigidbody2D>().velocity = new Vector2 (3f, 0);
		Debug.Log(currentStamina.runtimeValue);
    }
}
