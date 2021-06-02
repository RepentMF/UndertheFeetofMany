using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
	idle,
	move,
	attack,
	standby, //buffer state for Knockback script
	stagger,
	juggle,
	freefall
}

public class Enemy : MonoBehaviour
{
	public EnemyState currentState;
	public bool isInvuln;
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
	public float movRange;
	public Animator animator;

	protected void SetAnimFloat(Vector2 vec)
	{
		animator.SetFloat("moveX", vec.x);
		animator.SetFloat("moveY", vec.y);
	}

	private void ChangeDir(Vector2 dir)
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

	public void TakeDamage(float damage, bool execute = false)
	{
		if(combo == 0)
		{
			home = transform.position.y;
		}

		if(!isInvuln)
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

	public void IdleReset()
	{
		if(currentState == EnemyState.idle)
    	{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().gravityScale = 0.0f;
			isInvuln = false;
			combo = 0;
			hitBy = "";
    	}
	}

	public void Knockback()
	{

	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(!collider.gameObject.CompareTag("Player") && !collider.gameObject.CompareTag("Enemy") 
			&& !isInvuln)
		{
			Attack attack = collider.GetComponentInParent<Attack>();
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			Animator playerAnim = collider.GetComponentInParent<PlayerMovement>().animator;
			if(hitBy != attack.hitbox || attack.hitbox == "knife")
			{
				hitBy = attack.hitbox;
				TakeDamage(attack.damage);
				Debug.Log(attack.thrust);

				if(attack.hitbox.Contains("sword"))
				{
					currentState = EnemyState.stagger;

					if(playerAnim.GetFloat("moveX") != 0f)
					{
						currentStagger = Mathf.Abs(attack.thrust.y) / 10;
						if(attack.gameObject.tag == "Heavy")
						{
							currentState = EnemyState.juggle;
							body.gravityScale = 2f;
						}
					}
					else if(playerAnim.GetFloat("moveY") != 0f)
					{
						currentStagger = Mathf.Abs(attack.thrust.x) / 10;
						if(attack.gameObject.tag == "Heavy")
						{
							currentStagger = (Mathf.Abs(attack.thrust.y) / 10) - 12;
							body.gravityScale = 0f;
						}
					}

					body.velocity = Vector2.zero;
					body.AddForce(attack.thrust, ForceMode2D.Force);
					isInvuln = true;
					combo++;
				}
				else if(attack.hitbox.Contains("hammer"))
				{
					body.gravityScale = 2f;
					currentState = EnemyState.juggle;

					if(attack.gameObject.tag == "Heavy" && playerAnim.GetFloat("moveY") != 0f)
					{
						currentStagger = Mathf.Abs(attack.thrust.y) / 10;
						currentState = EnemyState.stagger;
						body.gravityScale = 0f;
					}

					body.velocity = Vector2.zero;
					body.AddForce(attack.thrust, ForceMode2D.Force);
					isInvuln = true;
					combo++;
				}
			}
			
		}
	}

    // Start is called before the first frame update
    public void Start()
    {
    	isInvuln = false;
    	combo = 0;
    	currentStagger = 0f;

		currentState = EnemyState.idle;
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
    	if(GetComponent<Rigidbody2D>().velocity.y < 0f && currentState == EnemyState.juggle)
    	{
    		currentState = EnemyState.freefall;
    		isInvuln = false;
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

		if(currentState == EnemyState.stagger || currentState == EnemyState.juggle)
		{
			animator.Play("stagger");
		}
		else if(currentState == EnemyState.idle)
		{
			animator.Play("idle");
		}
		//GetComponent<Rigidbody2D>().velocity = new Vector2 (3f, 0);
		Debug.Log(currentStamina.runtimeValue);
    }
}
