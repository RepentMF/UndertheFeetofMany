using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	walk,
	attack,
	interact,
	stagger, 
	idle
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerState currentState;
	public float speed;
	private Rigidbody2D rigidbody;
	private Vector3 change;
	private Animator animator;
	public FloatValue currentHealth;
	public Signal playerHealthSignal;
	public VectorValue startingPosition;

	void MoveCharacter()
	{
		change.Normalize();
		rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
	}

	private IEnumerator KnockCo(float kbTime)
	{
		if(rigidbody != null)
		{
			yield return new WaitForSeconds(kbTime);
			rigidbody.velocity = Vector2.zero;
			currentState = PlayerState.idle;
			rigidbody.velocity = Vector2.zero;
		}
	}

	public void Knock(float kbTime, float damage)
	{
		currentHealth.runtimeValue -= damage;
		playerHealthSignal.Raise();

		if(currentHealth.runtimeValue > 0)
		{
			StartCoroutine(KnockCo(kbTime));
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}

	private IEnumerator AttackCo()
	{
		animator.SetBool("attacking", true);
		currentState = PlayerState.attack;
		yield return null;
		animator.SetBool("attacking", false);
		//yield return new WaitForSeconds(.3f);
		currentState = PlayerState.walk;
	}

	void UpdateAnimationAndMove()
	{
		if (change != Vector3.zero)
		{
			MoveCharacter();
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		}
		else
		{
			animator.SetBool("moving", false);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		currentState = PlayerState.walk;
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
		change = Vector2.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		if (Input.GetButtonDown("attack") && currentState != PlayerState.attack &&
			currentState != PlayerState.stagger) 
		{
			StartCoroutine(AttackCo());
		}
		else if (currentState == PlayerState.walk || currentState == PlayerState.idle) 
		{
			UpdateAnimationAndMove();
		}
    }
}