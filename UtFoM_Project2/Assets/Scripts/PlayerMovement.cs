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
	public VectorValue playerPos;
	public VectorValue playerDir;
	public FloatValue currentHealth;
	public Signal playerHealthSignal;
	public Animator animator;
	private Rigidbody2D rigidbody;
	public Vector3 change;
	

	/*private IEnumerator KnockCo(float kbTime)
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
	}*/

	public bool Anicheck(string name)
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
	}

    // Start is called before the first frame update
    void Start()
    {
    	QualitySettings.vSyncCount = 0;
    	Application.targetFrameRate = 45;
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		if (playerPos.initialValue != Vector2.zero)
		{
			transform.position = playerPos.initialValue;
		}

		if (playerDir.initialValue != Vector2.zero)
		{
			animator.SetFloat("moveX", playerDir.initialValue.x);
			animator.SetFloat("moveY", playerDir.initialValue.y);
		}
		currentKBTime = 0f;
		light = "Hammer";
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
		else if (attackBuffer >= attackFrame)
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
					if (attackBuffer >= 5f && (Input.GetButtonDown(light) || Input.GetButtonDown(launch) || Input.GetButtonDown(heavy)))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						if (Anicheck("Knife"))
						{
							animator.Play(med);
						}
						else if (Anicheck("Knife2"))
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
					else if (attackBuffer >= 10f && (Input.GetButtonDown(launch) || Input.GetButtonDown(heavy)) && 
						Anicheck(med))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						attackFrame = 0f;
						animator.Play(heavy);
						comboBool = false;
					}
					break;
				case "Hammer":
					if (attackBuffer >= 30f && (Input.GetButtonDown(light) || Input.GetButtonDown(med)) && 
						!Anicheck(launch))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(med);
					}
					else if (attackBuffer >= 30f && Input.GetButtonDown(launch) && Anicheck(med))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(launch);
					}
					else if (attackBuffer >= 34f && Input.GetButtonDown(heavy) && Anicheck(launch))
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

			if (Anicheck("Idle") || Anicheck("Walking"))
			{
				comboBool = false;
			}
		}

		//"State Machine" block
		if (currentKBTime > 0f)
		{
			currentKBTime -= Time.deltaTime;
		}
		else if ((!comboBool && (Input.GetButtonDown(light) || Input.GetButtonDown(med) || Input.GetButtonDown(launch) || Input.GetButtonDown(heavy)) && 
			(Anicheck("Idle") || Anicheck("Walking"))))
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
		else if (change != Vector3.zero && (Anicheck("Idle") || Anicheck("Walking"))
			&& !(Anicheck(light) || Anicheck(med) || Anicheck(launch) 
			|| Anicheck(heavy)))
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

		if (animator.GetBool("moving") && Anicheck("Walking"))
    	{
			change.Normalize();
			rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    	}

    	if (Anicheck("Idle") || Anicheck("Walking"))
    	{
    		attackBuffer = 0f;
    		attackFrame = 0f;
    	}
    }
}