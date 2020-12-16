using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool comboBool = false;
	public int swipe = 0;
	//public int comboCounter = 1;
	public float attackFrame;
	public float attackBuffer;
	public float speed;
	public float currentKBTime;
	public string light;
	public string med;
	public string launch;
	public string heavy;
	public FloatValue currentHealth;
	public Signal playerHealthSignal;
	public Animator animator;
	public VectorValue startingPosition;
	private Rigidbody2D rigidbody;
	public Vector3 change;
	

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
		light = "Knife";
		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		//transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		// Choose which weapon is being used
		switch (light)
		{
			case "Knife":
				med = light + "2";
				launch = light;
				heavy = light;
				break;
			case "Sword":
				med = light + "2";
				launch = light + "3";
				heavy = light + "3";
				break;
			case "Hammer":
				med = light + "2";
				launch = light + "3";
				heavy = light + "4";
				break;
			default:
				break;
		}

		if (comboBool && attackBuffer == 0f)
		{
			attackFrame = animator.GetCurrentAnimatorStateInfo(0).length * 60f;
		}

		if (attackBuffer < attackFrame)
		{
			attackBuffer = (float) Math.Ceiling(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * attackFrame);
		}
		else if (attackBuffer == attackFrame)
		{
			attackBuffer = 0f;
			comboBool = false;
			attackFrame = 0f;
		}

		if (comboBool)
		{
			switch (light)
			{
				case "Knife":
					if (attackBuffer >= 1f && (Input.GetButtonDown(light) || Input.GetButtonDown(launch) || Input.GetButtonDown(heavy)))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knife"))
						{
							animator.Play(med);
						}
						if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knife2"))
						{
							animator.Play(light);
						}
					}
					break;
				case "Sword":
					if (attackBuffer >= 10f && (Input.GetButtonDown(light) || Input.GetButtonDown(med)))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(med);
					}
					else if (attackBuffer >= 10f && (Input.GetButtonDown(launch) || Input.GetButtonDown(heavy)) && animator.GetCurrentAnimatorStateInfo(0).IsName(med))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						attackFrame = 0f;
						animator.Play(heavy);
						comboBool = false;
					}
					break;
				case "Hammer":
					if (attackBuffer >= 30f && (Input.GetButtonDown(light) || Input.GetButtonDown(med)))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(med);
					}
					else if (attackBuffer >= 30f && Input.GetButtonDown(launch) && animator.GetCurrentAnimatorStateInfo(0).IsName(med))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(launch);
					}
					else if (attackBuffer >= 40f && Input.GetButtonDown(heavy) && animator.GetCurrentAnimatorStateInfo(0).IsName(launch))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						attackFrame = 0f;
						animator.Play(heavy);
						comboBool = false;
					}
					break;
				default:
					break;
			}
		}

		//"State Machine" block
		if (currentKBTime > 0f)
		{
			currentKBTime -= Time.deltaTime;
		}
		else if ((!comboBool && (Input.GetButtonDown(light) || Input.GetButtonDown(med) || Input.GetButtonDown(launch) || Input.GetButtonDown(heavy)) && 
			(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))))
		{
			// Stop the player
			animator.SetBool("moving", false);
			// Swing the weapon
			if (Input.GetButtonDown(light) || Input.GetButtonDown(med))
			{
				animator.Play(light);
				swipe++;
				comboBool = true;
			}
			else if (Input.GetButtonDown(launch))
			{
				animator.Play(launch);
				if (light == "Hammer")
				{
					comboBool = true;
				}
			}
			else
			{
				animator.Play(heavy);
				if (light != "Knife")
				{
					comboBool = false;	
				}
				else
				{
					comboBool = true;
				}
			}

			attackBuffer = 0f;
		}
		else if (change != Vector3.zero && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
			&& !(animator.GetCurrentAnimatorStateInfo(0).IsName(light) || animator.GetCurrentAnimatorStateInfo(0).IsName(med) || animator.GetCurrentAnimatorStateInfo(0).IsName(launch) 
			|| animator.GetCurrentAnimatorStateInfo(0).IsName(heavy)))
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

		if (animator.GetBool("moving") && animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
    	{
			change.Normalize();
			rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    	}

    	if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
    	{
    		attackBuffer = 0f;
    		attackFrame = 0f;
    	}
    }
}