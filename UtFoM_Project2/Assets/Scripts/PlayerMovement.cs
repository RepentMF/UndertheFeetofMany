using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	walk,
	knife,
	hammer1,
	interact,
	stagger, 
	idle
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerState currentState;
	public float speed;
	private Rigidbody2D rigidbody;
	public Vector3 change;
	private Animator animator;
	public FloatValue currentHealth;
	public Signal playerHealthSignal;
	public VectorValue startingPosition;

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

    // Start is called before the first frame update
    void Start()
    {
		//currentState = PlayerState.idle;
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		if (Input.GetButtonDown("knife"))
		{
			animator.Play("Knifing");
			animator.SetBool("moving", false);
		}
		else if (Input.GetButtonDown("hammer1"))
		{
			animator.Play("Hammering");
			animator.SetBool("moving", false);
		}
		else if (change != Vector3.zero && !animator.GetCurrentAnimatorStateInfo(0).IsName("Knifing") && 
			!animator.GetCurrentAnimatorStateInfo(0).IsName("Hammering"))
		{
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		}
		else if (change == Vector3.zero)
		{
			animator.SetBool("moving", false);
		}
    }

    void FixedUpdate()
    {
    	if (animator.GetBool("moving"))
    	{
			change.Normalize();
			rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    	}
    	else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knifing"))
    	{
    		change = Vector3.zero;
    	}
    }
}