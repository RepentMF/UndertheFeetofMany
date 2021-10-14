using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	InputController controls;

	public List<GameObject> playerList;
	public bool shortcut = false;
	public bool comboBool = false;
	public bool newPlayer = true;
	public int swipe = 0;
	//public int comboCounter = 1;
	public float attackFrame;
	public float attackBuffer;
	public float speed;
	public float currentKBTime;
	public string curScene;
	public string newScene;
	public string light;
	public string med;
	public string launch;
	public string heavy;
	public Transform magicStart;
	public GameObject spark;
	public GameObject burst;
	public GameObject flurry;
	public VectorValue playerPos;
	public VectorValue playerDir;
	public float currentHealth;
	public float maxHealth;
	public FloatValue currentMana;
	public FloatValue currentStamina;
	public FloatValue currentEstus;
	public Signal playerHealthSignal;
	public Signal playerManaSignal;
	public Signal playerStaminaSignal;
	public Signal playerEstusSignal;
	public Animator animator;
	private Rigidbody2D rigidbody;
	public Vector2 change;
	

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

	public void SparkTrigger()
	{
		GameObject fire = Instantiate(spark);
		fire.transform.position = magicStart.position;
		Rigidbody2D rb = fire.GetComponentInChildren<Rigidbody2D>();

		if(animator.GetFloat("moveX") > 0)
		{
			rb.AddForce(magicStart.right * 20f, ForceMode2D.Impulse);
			fire.transform.Rotate(0, 0, 270);
		}
		else if(animator.GetFloat("moveY") > 0)
		{
			rb.AddForce(magicStart.up * 20f, ForceMode2D.Impulse);
		}
		else if(animator.GetFloat("moveX") < 0)
		{
			rb.AddForce(magicStart.right * -20f, ForceMode2D.Impulse);
			fire.transform.Rotate(0, 0, 90);
		}
		else if(animator.GetFloat("moveY") < 0)
		{
			rb.AddForce(magicStart.up * -20f, ForceMode2D.Impulse);
			fire.transform.Rotate(0, 0, 180);
		}
	}

	public void LightningBurst()
	{
		GameObject lightning = Instantiate(burst);
		lightning.transform.position = magicStart.position + new Vector3(.75f, .75f, 0f);
		GameObject lightning2 = Instantiate(burst);
		lightning2.transform.position = magicStart.position + new Vector3(-.75f, .75f, 0f);
		lightning2.transform.GetComponentInParent<Attack>().thrust.x *= -1;
		GameObject lightning3 = Instantiate(burst);
		lightning3.transform.position = magicStart.position + new Vector3(.75f, -.75f, 0f);
		lightning3.transform.GetComponentInParent<Attack>().thrust.y *= -1f;
		GameObject lightning4 = Instantiate(burst);
		lightning4.transform.position = magicStart.position + new Vector3(-.75f, -.75f, 0f);
		lightning4.transform.GetComponentInParent<Attack>().thrust *= -1f;
		Rigidbody2D rb = lightning.GetComponentInChildren<Rigidbody2D>();
	}

	public void FlurryField()
	{
		GameObject ice = Instantiate(flurry);
		ice.transform.position = magicStart.position;
		Rigidbody2D rb = ice.GetComponentInChildren<Rigidbody2D>();

		if(animator.GetFloat("moveX") > 0)
		{
			ice.transform.Rotate(0, 0, 270);
		}
		else if(animator.GetFloat("moveY") > 0)
		{
		}
		else if(animator.GetFloat("moveX") < 0)
		{
			ice.transform.Rotate(0, 0, 90);
		}
		else if(animator.GetFloat("moveY") < 0)
		{
			ice.transform.Rotate(0, 0, 180);
		}
	}

	void OnShortcutButton()
	{
		controls.Player.ShortcutButton.started += ctx => shortcut = true;
		controls.Player.ShortcutButton.canceled += ctx => shortcut = false;
	}

	void Awake()
	{
		controls = new InputController();
    	controls.Enable();
	}

    // Start is called before the first frame update
    void Start()
    {
    	playerList = GameObject.FindGameObjectsWithTag("Player").OfType<GameObject>().ToList();
    	QualitySettings.vSyncCount = 0;
    	Application.targetFrameRate = 45;
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		if(playerPos.initialValue != Vector2.zero)
		{
			transform.position = playerPos.initialValue;
		}

		if(playerDir.initialValue != Vector2.zero)
		{
			animator.SetFloat("moveX", playerDir.initialValue.x);
			animator.SetFloat("moveY", playerDir.initialValue.y);
		}

		foreach(GameObject player in playerList)
		{
			if(player.GetComponent<PlayerMovement>().newPlayer && playerList.Count > 1)
			{
				Destroy(player);
				playerList.RemoveAt(1);
			}
		}
		currentKBTime = 0f;
		light = "Hammer";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if(curScene != newScene)
    	{
    		curScene = newScene;
			transform.position = playerPos.initialValue;
    		animator.SetFloat("moveX", playerDir.initialValue.x);
			animator.SetFloat("moveY", playerDir.initialValue.y);
    	}

    	DontDestroyOnLoad(this.gameObject);
    	
		change = controls.Player.Move.ReadValue<Vector2>();

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

		if(comboBool && attackBuffer == 0f)
		{
			attackFrame = animator.GetCurrentAnimatorStateInfo(0).length * 60f;
		}

		if(attackBuffer < attackFrame)
		{
			attackBuffer = (float) Math.Ceiling(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * attackFrame);
		}
		else if(attackBuffer >= attackFrame)
		{
			attackBuffer = 0f;
			comboBool = false;
			attackFrame = 0f;
		}

		if(comboBool)
		{
			switch (light)
			{
				case "Knife":
					if(attackBuffer >= 5f && (controls.Player.LightButton.triggered || controls.Player.LaunchButton.triggered || controls.Player.HeavyButton.triggered))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						if(Anicheck("Knife"))
						{
							animator.Play(med);
						}
						else if(Anicheck("Knife2"))
						{
							animator.Play(light);
						}
					}
					break;
				case "Sword":
					if(attackBuffer >= 10f && (controls.Player.LightButton.triggered))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(med);
					}
					else if(attackBuffer >= 10f && (controls.Player.LaunchButton.triggered || controls.Player.HeavyButton.triggered) && 
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
					if(attackBuffer >= 30f && (controls.Player.LightButton.triggered) && 
						!Anicheck(launch))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(med);
					}
					else if(attackBuffer >= 30f && controls.Player.LaunchButton.triggered && Anicheck(med))
					{
						animator.SetBool("moving", false);
						attackBuffer = 0f;
						animator.Play(launch);
					}
					else if(attackBuffer >= 34f && controls.Player.HeavyButton.triggered && Anicheck(launch))
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

			if(Anicheck("Idle") || Anicheck("Walking"))
			{
				comboBool = false;
			}
		}

		//"State Machine" block
		if(currentKBTime > 0f)
		{
			currentKBTime -= Time.deltaTime;
		}
		else if((!comboBool && (controls.Player.LightButton.triggered || controls.Player.LaunchButton.triggered || controls.Player.HeavyButton.triggered) && 
			(Anicheck("Idle") || Anicheck("Walking"))))
		{
			//Swing the weapon
			if(shortcut)
			{
				if(controls.Player.LaunchButton.triggered)
				{
					// Stop the player
					animator.SetBool("moving", false);
					SparkTrigger();
				}
				else if(controls.Player.HeavyButton.triggered)
				{
					// Stop the player
					animator.SetBool("moving", false);
					LightningBurst();
				}
				else if(controls.Player.LightButton.triggered)
				{
					// Stop the player
					animator.SetBool("moving", false);
					FlurryField();
				}
			}
			else if(controls.Player.LightButton.triggered)
			{
				// Stop the player
				animator.SetBool("moving", false);
				animator.Play(light);
				swipe++;
				comboBool = true;
			}
			else if(controls.Player.LaunchButton.triggered)
			{
				// Stop the player
				animator.SetBool("moving", false);
				animator.Play(launch);
				if(light == "Hammer")
				{
					comboBool = true;
				}
			}
			else
			{
				// Stop the player
				animator.SetBool("moving", false);
				animator.Play(heavy);
				if(light != "Knife")
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
		else if(change != Vector2.zero && (Anicheck("Idle") || Anicheck("Walking"))
			&& !(Anicheck(light) || Anicheck(med) || Anicheck(launch) 
			|| Anicheck(heavy)))
		{
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		}
		else if(change == Vector2.zero)
		{
			animator.SetBool("moving", false);
		}

		if(currentKBTime < 0f)
		{
			currentKBTime = 0f;
		}

		if(animator.GetBool("moving") && Anicheck("Walking"))
    	{
			change.Normalize();
			rigidbody.MovePosition(transform.position + new Vector3 (change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime, 0));
    	}

    	if(Anicheck("Idle") || Anicheck("Walking"))
    	{
    		attackBuffer = 0f;
    		attackFrame = 0f;
    	}
    }
}