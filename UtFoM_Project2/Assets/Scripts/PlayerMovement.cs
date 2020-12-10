using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool comboBool = false;
	public int attackBuffer;
	public int comboCounter = 1;
	public float speed;
	public float currentKBTime;
	public string attack;
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
    	QualitySettings.vSyncCount = 0;
    	Application.targetFrameRate = 45;
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		currentKBTime = 0f;
		attack = "";
		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		if (animator.GetBool("moving"))
    	{
			change.Normalize();
			rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    	}

		if (attackBuffer > 0)
		{
			attackBuffer--;
		}

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			comboCounter = 1;
			comboBool = false;
		}

		if (comboBool && attackBuffer <= 20 && Input.GetButtonDown(attack))
		{
			comboCounter++;
			string var = attack + comboCounter.ToString();
			attackBuffer = 50;
			animator.Play(var);
		}

		if (currentKBTime > 0f)
		{
			currentKBTime -= Time.deltaTime;
		}
		else if ((!comboBool && Input.GetButtonDown(attack) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || 
			animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))))
		{
			// Stop the player
			animator.SetBool("moving", false);
			// Swing the weapon
			animator.Play(attack);
			comboBool = true;

			switch (attack)
			{
				case "Hammer":
					attackBuffer = 50;
					break;
				default:
					break;
			}
		}
		else if (change != Vector3.zero && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")))
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

   //  void FixedUpdate()
   //  {
   // //  	if (animator.GetBool("moving"))
   // //  	{
			// // change.Normalize();
			// // rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
   // //  	}
   //  }
}