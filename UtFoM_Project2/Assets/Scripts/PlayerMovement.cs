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
	public float speed;
	public float currentKBTime;
	public FloatValue currentHealth;
	public Signal playerHealthSignal;
	public Animator animator;
	public VectorValue startingPosition;
	private Rigidbody2D rigidbody;
	private Vector3 change;
	

	private IEnumerator KnockCo(float kbTime)
	{
		if(rigidbody != null)
		{
			yield return new WaitForSeconds(kbTime);
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
			currentKBTime = kbTime;
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		currentKBTime = 0f;
		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		if (currentKBTime > 0f)
		{
			currentKBTime -= Time.deltaTime;
		}
		else if (Input.GetButtonDown("knife"))
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

		if (currentKBTime < 0f)
		{
			currentKBTime = 0f;
		}
    }

    void FixedUpdate()
    {
    	if (animator.GetBool("moving"))
    	{
			change.Normalize();
			rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    	}
    }
}