using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
	idle,
	walk,
	attack,
	stagger
}

public class Enemy : MonoBehaviour
{
	public EnemyState currentState;
	public FloatValue maxHealth;
	public int baseAtk;
	public float health;
	public float moveSpeed;
	public string enemyName;

	public void Knock(Rigidbody2D rigidbody, float kbTime, float damage)
	{
		StartCoroutine(KnockCo(rigidbody, kbTime));
		TakeDamage(damage);
	}

	private void TakeDamage(float damage)
	{
		health -= damage;
		if(health <= 0)
		{
			this.gameObject.SetActive(false);
		}
	}
	
	private IEnumerator KnockCo(Rigidbody2D rigidbody, float kbTime)
	{
		if(rigidbody != null)
		{
			yield return new WaitForSeconds(kbTime);
			rigidbody.velocity = Vector2.zero;
			currentState = EnemyState.idle;
			rigidbody.velocity = Vector2.zero;
		}
	}

	private void Awake()
	{
		health = maxHealth.initialValue;
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
