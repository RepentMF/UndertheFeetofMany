using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Status
{
	leech,
	poison,
	burn,
	struggle,
	exhaust
}

public class StatusEffect
{
	public float statTimer;
	public float originalTimer;
	public float intensity;
	public Status name;
	public StatusEffect(Status name, float statTimer, float intensity)
	{
		this.name = name;
		this.statTimer = statTimer;
		this.originalTimer = statTimer;
		this.intensity = intensity;
	}
}

public class StatusMod : MonoBehaviour
{
	private PlayerMovement player;
	private Enemy enemy;
	List<StatusEffect> statuses;
	StatusEffect remove;
	bool pullCheck;

	public void AddStatus(Status name, float statTimer, float intensity)
	{
		bool check = false;

		foreach(StatusEffect status in statuses)
		{
			if(status.name == name)
			{
				check = true;
				status.statTimer = status.intensity > intensity ? status.statTimer : statTimer;
				status.intensity = status.intensity > intensity ? status.intensity : intensity;
			}
		}

		if(!check)
		{
			statuses.Add(new StatusEffect(name, statTimer, intensity));
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<PlayerMovement>() != null)
    	{
    		player = gameObject.GetComponent<PlayerMovement>();
    	}
    	else
    	{	
    		enemy = gameObject.GetComponent<Enemy>();
    	}
    	statuses = new List<StatusEffect>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if(statuses.Count != 0)
    	{
    		if(player != null)
	    	{
	    		
	    	}
	       	else
	       	{
	       		foreach(StatusEffect status in statuses)
		        {
		        	switch (status.name)
		        	{
		        		case Status.leech:
		        			//need a way to pull user's info to give illusion of health being sapped
							enemy.currentHealth.runtimeValue -= status.intensity;
		    				status.stattimer -= Time.deltaTime;
		        			break;
		    			case Status.poison:
		    				enemy.maxHealth.runtimeValue /= 2;
		    				if(enemy.maxHealth.runtimeValue < enemy.currentHealth.runtimeValue)
		    				{
		    					enemy.currentHealth.runtimeValue = enemy.maxHealth.runtimeValue;
		    				}
		    				break;
						case Status.burn:
							enemy.currentHealth.runtimeValue -= status.intensity;
		    				status.stattimer -= Time.deltaTime;
							break;
						case Status.struggle:
							enemy.currentStamina.runtimeValue -= status.intensity;
							status.statTimer -= Time.deltaTime;
							break;
						case Status.exhaust:
							break;
		        	}
		        	if(status.statTimer <= 0f)
		        	{
		        		remove = status;
		        		pullCheck = true;
		        		//remove
		        	}
	       		}
	       		if(statuses.Contains(remove) && pullCheck)
	       		{
	       			statuses.Remove(remove);
	       			pullCheck = false;
	       		}
       		}
    	}
    }
}
