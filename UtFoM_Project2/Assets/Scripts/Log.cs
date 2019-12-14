using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
	private Rigidbody2D rigidbody;
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

    // Start is called before the first frame update
    void Start()
    {
		currentState = EnemyState.idle;
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		target = GameObject.FindWithTag("Player").transform;
    }

	// Update is called once per frame
	void Update()
	{
		
	}

    void FixedUpdate()
    {
		CheckDistance();
    }
}
