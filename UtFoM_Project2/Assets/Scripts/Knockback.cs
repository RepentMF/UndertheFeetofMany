using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	public float thrust;
	public float kbTime;
	public float damage;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag ("Breakable")) 
		{
			collider.GetComponent<Pot>().Smash();
		}

		if(collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player"))
		{
			Rigidbody2D hurtbox = collider.GetComponent<Rigidbody2D>();
			if(hurtbox != null)
			{
				Vector2 difference = hurtbox.transform.position - transform.position;
				difference = difference.normalized * thrust;
				hurtbox.AddForce(difference, ForceMode2D.Impulse);

				if(collider.gameObject.CompareTag("Enemy") && collider.isTrigger)
				{
					hurtbox.GetComponent<Enemy>().currentState = EnemyState.stagger;
					collider.GetComponent<Enemy>().Knock(hurtbox, kbTime, damage);
				}

				if(collider.gameObject.CompareTag("Player"))
				{
					if(collider.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
					{
						hurtbox.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
						collider.GetComponent<PlayerMovement>().Knock(kbTime, damage);
					}
				}
			}
		}
	}

	private IEnumerator KnockCo(Rigidbody2D rigidbody)
	{
		if(rigidbody != null)
		{
			yield return new WaitForSeconds(kbTime);
			rigidbody.velocity = Vector2.zero;
			rigidbody.GetComponent<Enemy>().currentState = EnemyState.idle;
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
