using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public int randDir;

    void CheckDistance()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < 3.0f)
        {
            //move toward player
            rigidbody.velocity = target.transform.position - transform.position;
        }
        else
        {
            //move randomly
        }
    }

    void Rush(float spd)
    {
        // randDir = Random.Range(0, 4);
        // Debug.Log(randDir);
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
        CheckDistance();
    }
}
