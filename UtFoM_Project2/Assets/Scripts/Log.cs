using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
	// public Transform target;
	// public Transform homePos;
	// public float chaseRadius;
	// public float attackRadius;
	// public Animator animator;

	

    // Start is called before the first frame update
    void Start()
    {
		currentState = EnemyState.idle;
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		target = GameObject.FindWithTag("Player").transform;
    }

	// Update is called once per frame
	void Update()
	{
		
	}

    void FixedUpdate()
    {
    	
    }
}
