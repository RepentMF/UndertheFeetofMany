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
	public string enemyName;

	public void Knock(float damage)
	{
		if(combo == 0)
		{
			homePos = transform.position.y;
		}

		TakeDamage(damage);
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
    }

    // Update is called once per frame
    void Update()
    {
    	if(GetComponent<Rigidbody2D>().velocity.y < 0f)
    	{
    		currentState = EnemyState.freefall;
    		hit = false;
    	}
    	//Debug.Break();
        if(transform.position.y < homePos && currentState == EnemyState.freefall)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().gravityScale = 0.0f;
			currentState = EnemyState.idle;
			hit = false;
			combo = 0;
		}
    }
}
