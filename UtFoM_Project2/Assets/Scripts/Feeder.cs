using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeder : Enemy
{
    public Vector3 dir;
    public float rushRange = 5f;
    public float devourRange = 3f;
    public float movTimerBase;
    public float rushTimerBase;
    public float devourTimerBase;
    public float rushStaminaCost;
    public float devourManaCost;
    public float movTimer;
    public float atkTimer;
    public bool inRange;
    public bool inRushRange;
    public bool inDevourRange;

    void CheckRange()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < movRange)
        {
            inRange = true;
            inRushRange = false;
            inDevourRange = false;

            if(Vector3.Distance(transform.position, target.transform.position) < rushRange)
            {
                inRushRange = true;
            }
            if(Vector3.Distance(transform.position, target.transform.position) < devourRange && currentMana > 0f)
            {
                inDevourRange = true;
            }
        }
        else
        {
            currentState = EnemyState.idle;
            inRange = false;
            inRushRange = false;
            inDevourRange = false;
        }
    }

    void CheckDirection()
    {
        if(inRange)
        {
            dir = (target.transform.position - transform.position);
            SetAnimFloat(dir);
        }
    }

    void ChooseAction()
    {
        if(!statMod.GetStatus(Status.Exhaust))
        {
            if(inDevourRange)
            {
                StartDevour();
            }
            else if(inRushRange && currentStamina > rushStaminaCost)
            {
                StartRush();
            }
            else if(currentStamina > 0)
            {
                Move();
            }
        }
    }

    void Move()
    {
        currentState = EnemyState.move;
        rigidbody.velocity = dir.normalized * spd;
    }

    void StartRush()
    {
        currentStamina -= rushStaminaCost;
        currentState = EnemyState.attack;
        //rigidbody.velocity = Vector2.zero;
        atkTimer = rushTimerBase;
        animator.Play("rush");
    }

    void Rush()
    {
        if(atkTimer < 0)
        {
            currentState = EnemyState.idle;
            rigidbody.velocity = Vector3.zero;
        }
        else if(atkTimer < 0.583f)
        {
            rigidbody.velocity = dir.normalized * 5f;
        }
    }

    void StartDevour()
    {
        currentMana -= devourManaCost;
        currentState = EnemyState.attack;
        //rigidbody.velocity = Vector2.zero;
        atkTimer = devourTimerBase;
        animator.Play("devour");
    }

    void Devour()
    {
        rigidbody.velocity = Vector3.zero;
        if(atkTimer < 0)
        {
            currentState = EnemyState.idle;
        }
    }

    void FixedUpdate() 
    {
        //fix idle animation playing during rush attack
        FindTarget();
        IdleReset();
        CheckRange();

        if(currentState != EnemyState.stagger && currentState != EnemyState.juggle && currentState != EnemyState.freefall)
        {   
            animator.SetBool("moving", false);
            if(currentState == EnemyState.idle && !statMod.GetStatus(Status.Exhaust))
            {   
                RegenStamina();
                if(inRange)
                {
                    CheckDirection();
                    ChooseAction();
                }
            }
            else if(currentState == EnemyState.move)
            {
                if(inRange)
                {
                    animator.SetBool("moving", true);
                    CheckDirection();
                    ChooseAction();
                    currentStamina -= Time.deltaTime;
                }

                KillEnemy();
            }
            else if(currentState == EnemyState.attack)
            {
                atkTimer -= Time.deltaTime;
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("devour"))
                {
                    Devour();
                }
                else
                {
                    //this is booboo terrible
                    Rush();
                }
                //Devour();
            }

            if(currentMana <= 0)
            {
                currentMana = 0f;
            }
            if(currentStamina <= 0)
            {
                currentStamina = 0f;
                currentState = EnemyState.idle;
                statMod.AddStatus(Status.Exhaust, maxStamina, 0f);
            }
        }
        else
        {
            base.FixedUpdate();
        }
    }
}
