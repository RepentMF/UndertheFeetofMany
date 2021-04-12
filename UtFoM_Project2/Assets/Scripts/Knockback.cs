using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collider)
	{
		Rigidbody2D hurtbox = collider.GetComponent<Rigidbody2D>();

		if(collider.gameObject.CompareTag("Enemy") && collider.isTrigger)
		{
			if(GetComponentInParent<PlayerMovement>().light == "Knife")
			{
				if(hurtbox != null)
				{
					collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
				}
			}
			else if(GetComponentInParent<PlayerMovement>().light == "Sword")
			{
				if(transform.parent.tag == "Light")
				{
					if(hurtbox != null)
					{
						hurtbox.velocity = Vector2.zero;

						collider.GetComponent<Enemy>().currentState = EnemyState.stagger;
						collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
						collider.GetComponent<Enemy>().hit = true;
						collider.GetComponent<Enemy>().combo++;
					}
				}
				else if(transform.parent.tag == "Medium")
				{
					if(hurtbox != null)
					{
						hurtbox.velocity = Vector2.zero;

						collider.GetComponent<Enemy>().currentState = EnemyState.stagger;
						collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
						collider.GetComponent<Enemy>().hit = true;
						collider.GetComponent<Enemy>().combo++;
					}
				}
				else if(transform.parent.tag == "Heavy")
				{	
					collider.GetComponent<Rigidbody2D>().gravityScale = 2f;
					
					if(hurtbox != null)
					{
						hurtbox.velocity = Vector2.zero;
						hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

						collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
						collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
						collider.GetComponent<Enemy>().hit = true;
						collider.GetComponent<Enemy>().combo++;

						if(GetComponentInParent<Animator>().GetFloat("moveY") != 0)
						{
							collider.GetComponent<Rigidbody2D>().gravityScale = 0f;
						}
					}
				}
			}
			else if(GetComponentInParent<PlayerMovement>().light == "Hammer")
			{
				collider.GetComponent<Rigidbody2D>().gravityScale = 2f;

				if(!collider.GetComponent<Enemy>().hit && transform.parent.tag == "Light")
				{
					if(hurtbox != null)
					{
						hurtbox.velocity = Vector2.zero;
						hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

						collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
						collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
						collider.GetComponent<Enemy>().hit = true;
						collider.GetComponent<Enemy>().combo++;
					}
				}
				else if(transform.parent.tag == "Medium"
					&& collider.GetComponent<Enemy>().currentState != EnemyState.juggle)
				{
					if(hurtbox != null)
					{
						hurtbox.velocity = Vector2.zero;
						hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

						collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
						collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
						collider.GetComponent<Enemy>().hit = true;
						collider.GetComponent<Enemy>().combo++;
					}
				}
				else if(transform.parent.tag == "Launch" 
					&& collider.GetComponent<Enemy>().currentState != EnemyState.juggle)
				{
					if(hurtbox != null)
					{
						hurtbox.velocity = Vector2.zero;
						hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

						collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
						collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
						collider.GetComponent<Enemy>().hit = true;
						collider.GetComponent<Enemy>().combo++;
					}
				}
				else if(transform.parent.tag == "Heavy" 
					&& collider.GetComponent<Enemy>().currentState != EnemyState.juggle)
				{	
					if(hurtbox != null)
					{
						hurtbox.velocity = Vector2.zero;
						hurtbox.AddForce(GetComponentInParent<Attack>().thrust, ForceMode2D.Force);

						collider.GetComponent<Enemy>().currentState = EnemyState.juggle;
						collider.GetComponent<Enemy>().Knock(GetComponentInParent<Attack>().damage);
						collider.GetComponent<Enemy>().hit = true;
						collider.GetComponent<Enemy>().combo++;

						if(GetComponentInParent<Animator>().GetFloat("moveY") != 0)
						{
							collider.GetComponent<Rigidbody2D>().gravityScale = 0f;
						}
					}
				}
			}
		}
		else if(collider.gameObject.CompareTag("Player"))
		{
			// if(!collider.GetComponent<PlayerMovement>().animator.GetCurrentAnimatorStateInfo(0).IsName("Staggered"))
			// {
			// 	hurtbox.GetComponent<PlayerMovement>().animator.Play("Staggered");
			// 	collider.GetComponent<PlayerMovement>().Knock(damage);
			// }
		}
		else if(collider.gameObject.CompareTag ("Breakable"))
		{
			collider.GetComponent<Pot>().Smash();
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
