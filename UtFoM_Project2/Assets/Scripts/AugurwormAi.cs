using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugurwormAi : EnemyAi
{
    //[Header("Augurworm Properties")]

    protected internal override void AttackOne()
    {
    	//Create a rock to be thrown
    	Debug.Log("A rock was thrown!");
    }

    protected internal override void AttackTwo()
    {
    }
}
