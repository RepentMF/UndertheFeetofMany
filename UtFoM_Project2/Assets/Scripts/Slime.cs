using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public Vector3 dir;
    public float spd;
    public float movRange = 7.5f;
    public float rushRange = 4.5f;
    public float actTimerBase;
    public float movTimerBase;
    public float rushTimerBase;
    public float actTimer;
    public float movTimer;
    public float atkTimer;
    public bool inRange;
    public bool inRushRange;

    void CheckRange()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < movRange)
        {
            inRange = true;
            inRushRange = false;

            if(Vector3.Distance(transform.position, target.transform.position) < rushRange)
            {
                inRushRange = true;
            }
        }
        else
        {
            inRange = false;
            inRushRange = false;
        }
    }

    void CheckDirection()
    {
        if(inRange)
        {
            dir = (target.transform.position - transform.position);

            if(dir.x > 0 && Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir = Vector3.right;
            }
            else if(dir.y > 0 && Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            {
                dir = Vector3.up;
            }
            else if(dir.x < 0 && Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir = Vector3.left;
            }
            else if(dir.y < 0 && Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            {
                dir = Vector3.down;
            }
        }
    }

    void Move()
    {
        currentState = EnemyState.move;
        rigidbody.velocity = dir * spd;
        movTimer = movTimerBase;
    }

    void StartRush()
    {
        currentState = EnemyState.attack;
        //rigidbody.velocity = dir * 1.75f;
        atkTimer = rushTimerBase;
        animator.Play("rush");
    }

    void Rush()
    {
        if(atkTimer < 0)
        {
            currentState = EnemyState.idle;
            actTimer = actTimerBase;
            rigidbody.velocity = Vector3.zero;
            animator.Play("idle");
        }
        else if(atkTimer < 0.583f)
        {
            rigidbody.velocity = dir * 1.75f;
        }
    }

    // Start is called before the first frame update
    // void Start()
    // {
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void FixedUpdate() 
    {
        CheckRange();

        if(currentState == EnemyState.idle && inRange)
        {
            if(actTimer < 0)
            {
                if(inRushRange)
                {
                    StartRush();
                }
                else
                {
                    Move();
                }
            }
            else
            {
                CheckDirection();
                SetAnimFloat(dir);
                actTimer -= Time.deltaTime;
            }  
        }
        else if(currentState == EnemyState.move)
        {
            movTimer -= Time.deltaTime;
            if(movTimer < 0)
            {
                currentState = EnemyState.idle;
                actTimer = actTimerBase;
                rigidbody.velocity = Vector3.zero;
            }
        }
        else if(currentState == EnemyState.attack)
        {
            atkTimer -= Time.deltaTime;
            Rush();
        }
    }
}
