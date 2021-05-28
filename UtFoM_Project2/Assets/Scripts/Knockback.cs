// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Knockback : MonoBehaviour
// {

// 	public bool done;
// 	public bool magic;
// 	public float timer;

// 	private void OnTriggerEnter2D(Collider2D collider)
// 	{
// 		Rigidbody2D hurtbox = collider.GetComponent<Rigidbody2D>();
// 		if(collider.gameObject.CompareTag("Enemy"))
// 		{
// 			if((collider.GetComponent<Enemy>().hitBy != GetComponentInParent<Attack>().hitbox) || 
// 				GetComponentInParent<Attack>().hitbox == "knife")
// 			{
// 				collider.GetComponent<Enemy>().currentState = EnemyState.standby;
// 				collider.GetComponent<Enemy>().hitBy = GetComponentInParent<Attack>().hitbox;

// 				if(collider.isTrigger)
// 				{
// 					if(transform.parent.tag == "Flurry")
// 					{
// 						if(hurtbox != null)
// 						{
// 							collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 							collider.GetComponent<StatusMod>().AddStatus(Status.struggle, 10f, 1.0f * .1f);
// 						}
// 					}
// 					else if(transform.parent.tag == "Burst")
// 					{
// 						if(hurtbox != null)
// 						{	
// 							collider.GetComponent<Enemy>().currentStagger = Mathf.Abs(GetComponentInParent<Attack>().thrust.y) / 40;
							
// 							hurtbox.velocity = Vector2.zero;
// 							hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 							collider.GetComponent<Enemy>().currentState = EnemyState.stagger;
// 							collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, true);
// 							collider.GetComponent<Enemy>().isInvuln= true;
// 							collider.GetComponent<Enemy>().combo++;
// 						}
// 					}
// 					else if(transform.parent.tag == "Spark")
// 					{
// 						if(hurtbox != null)
// 						{
// 							collider.GetComponent<Rigidbody2D>().gravityScale = 2f;

// 							hurtbox.velocity = Vector2.zero;
// 							hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 							collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
// 							collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 							collider.GetComponent<Enemy>().isInvuln= true;
// 							collider.GetComponent<Enemy>().combo++;
// 							Destroy(transform.parent.gameObject);
// 							Destroy(gameObject);
// 						}
// 					}
// 					else if(GetComponentInParent<PlayerMovement>().light == "Knife")
// 					{
// 						if(hurtbox != null)
// 						{
// 							collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 						}
// 					}
// 					else if(GetComponentInParent<PlayerMovement>().light == "Sword")
// 					{
// 						if(GetComponentInParent<Animator>().GetFloat("moveX") != 0f)
// 						{
// 							collider.GetComponent<Enemy>().currentStagger = Mathf.Abs(GetComponentInParent<Attack>().thrust.y) / 10;
// 						}
// 						else if(GetComponentInParent<Animator>().GetFloat("moveY") != 0f)
// 						{
// 							collider.GetComponent<Enemy>().currentStagger = Mathf.Abs(GetComponentInParent<Attack>().thrust.x) / 10;
// 						}

// 						if(!collider.GetComponent<Enemy>().isInvuln && transform.parent.tag == "Light")
// 						{
// 							if(hurtbox != null)
// 							{
// 								hurtbox.velocity = Vector2.zero;
// 								hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 								collider.GetComponent<Enemy>().currentState = EnemyState.stagger;
// 								collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 								collider.GetComponent<Enemy>().isInvuln = true;
// 								collider.GetComponent<Enemy>().combo++;
// 							}
// 						}
// 						else if(!collider.GetComponent<Enemy>().isInvuln && transform.parent.tag == "Medium")
// 						{
// 							if(hurtbox != null)
// 							{
// 								hurtbox.velocity = Vector2.zero;
// 								hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 								collider.GetComponent<Enemy>().currentState = EnemyState.stagger;
// 								collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 								collider.GetComponent<Enemy>().isInvuln = true;
// 								collider.GetComponent<Enemy>().combo++;
// 							}
// 						}
// 						else if(!collider.GetComponent<Enemy>().isInvuln && transform.parent.tag == "Heavy")
// 						{
// 							if(GetComponentInParent<Animator>().GetFloat("moveX") != 0f)
// 							{
// 								collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
// 								collider.GetComponent<Rigidbody2D>().gravityScale = 2f;
// 							}
// 							else if(GetComponentInParent<Animator>().GetFloat("moveY") != 0f)
// 							{
// 								collider.GetComponent<Enemy>().currentStagger = (Mathf.Abs(GetComponentInParent<Attack>().thrust.y) / 10) - 12;
// 								collider.GetComponent<Enemy>().currentState = EnemyState.stagger;
// 							}

// 							if(hurtbox != null)
// 							{
// 								hurtbox.velocity = Vector2.zero;
// 								hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 								collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 								collider.GetComponent<Enemy>().isInvuln = true;
// 								collider.GetComponent<Enemy>().combo++;

// 								if(GetComponentInParent<Animator>().GetFloat("moveY") != 0f)
// 								{
// 									collider.GetComponent<Rigidbody2D>().gravityScale = 0f;
// 								}
// 							}
// 						}
// 					}
// 					else if(GetComponentInParent<PlayerMovement>().light == "Hammer")
// 					{
// 						collider.GetComponent<Rigidbody2D>().gravityScale = 2f;

// 						if(!collider.GetComponent<Enemy>().isInvuln && transform.parent.tag == "Light")
// 						{
// 							if(hurtbox != null)
// 							{
// 								hurtbox.velocity = Vector2.zero;
// 								hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 								collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
// 								collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 								collider.GetComponent<Enemy>().isInvuln = true;
// 								collider.GetComponent<Enemy>().combo++;
// 								//Debug.Log(collider.GetComponent<Slime>().currentState);
// 							}
// 						}
// 						else if(transform.parent.tag == "Medium"
// 							&& collider.GetComponent<Enemy>().currentState != EnemyState.juggle)
// 						{
// 							if(hurtbox != null)
// 							{
// 								hurtbox.velocity = Vector2.zero;
// 								hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 								collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
// 								collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 								collider.GetComponent<Enemy>().isInvuln = true;
// 								collider.GetComponent<Enemy>().combo++;
// 							}
// 						}
// 						else if(transform.parent.tag == "Launch" 
// 							&& collider.GetComponent<Enemy>().currentState != EnemyState.juggle)
// 						{
// 							if(hurtbox != null)
// 							{
// 								hurtbox.velocity = Vector2.zero;
// 								hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 								collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
// 								collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 								collider.GetComponent<Enemy>().isInvuln = true;
// 								collider.GetComponent<Enemy>().combo++;
// 							}
// 						}
// 						else if(transform.parent.tag == "Heavy" 
// 							&& collider.GetComponent<Enemy>().currentState != EnemyState.juggle)
// 						{
// 							if(GetComponentInParent<Animator>().GetFloat("moveX") != 0f)
// 							{
// 								collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
// 							}
// 							else if(GetComponentInParent<Animator>().GetFloat("moveY") != 0f)
// 							{
// 								collider.GetComponent<Enemy>().currentStagger = Mathf.Abs(GetComponentInParent<Attack>().thrust.y) / 10;
// 								collider.GetComponent<Enemy>().currentState = EnemyState.stagger;
// 							}

// 							if(hurtbox != null)
// 							{
// 								hurtbox.velocity = Vector2.zero;
// 								hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

// 								collider.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Attack>().damage, false);
// 								collider.GetComponent<Enemy>().isInvuln = true;
// 								collider.GetComponent<Enemy>().combo++;

// 								if(GetComponentInParent<Animator>().GetFloat("moveY") != 0)
// 								{
// 									collider.GetComponent<Rigidbody2D>().gravityScale = 0f;
// 								}
// 							}
// 						}
// 					}
// 				}
// 			}
// 		}
			
// 		else if(collider.gameObject.CompareTag("Player"))
// 		{
// 			// if(!collider.GetComponent<PlayerMovement>().animator.GetCurrentAnimatorStateInfo(0).IsName("Staggered"))
// 			// {
// 			// 	hurtbox.GetComponent<PlayerMovement>().animator.Play("Staggered");
// 			// 	collider.GetComponent<PlayerMovement>().TakeDamage(damage);
// 			// }
// 		}
// 		else if(collider.gameObject.CompareTag("Breakable"))
// 		{
// 			collider.GetComponent<Pot>().Smash();
// 		}
// 	}

//     // Start is called before the first frame update
//     void Start()
//     {
//     	done = true;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(done && magic)
//         {
//         	timer -= Time.deltaTime;
//         	if(timer <= 0f)
//         	{	
//         		Destroy(transform.parent.gameObject);
// 				Destroy(gameObject);
//         	}
//         }
//     }
// }
