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
	StatusEffect staminaVar;
	bool pullCheck;

	public string statusLog;

	public bool GetStatus(Status name)
	{
		foreach(StatusEffect status in statuses)
		{
			if(status.name == name)
			{
				return true;
			}
		}
		return false;
	}

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
							PlayerMovement p1 = GameObject.FindWithTag("P1").GetComponent<PlayerMovement>();
							if(enemy.currentHealth - status.intensity < 0f)
							{
								float difference = status.intensity - enemy.currentHealth;
								p1.currentHealth += difference;
							}
							else
							{
								p1.currentHealth += status.intensity;
							}
							enemy.currentHealth -= status.intensity;
		    				status.statTimer -= Time.deltaTime;
		        			break;
		    			case Status.poison:
		    				//add check to make sure halving doesn't happen every frame
		    				enemy.maxHealth /= 2;
		    				if(enemy.maxHealth < enemy.currentHealth)
		    				{
		    					enemy.currentHealth = enemy.maxHealth;
		    				}
		    				break;
						case Status.burn:
							enemy.currentHealth -= status.intensity;
		    				status.statTimer -= Time.deltaTime;
							break;
						case Status.struggle:
							enemy.currentStamina -= status.intensity;
							status.statTimer -= Time.deltaTime;
							break;
						case Status.exhaust:
							status.statTimer -= Time.deltaTime;
							enemy.animator.speed = 0.5f;
							enemy.RegenStamina();
							break;
		        	}
		        	if(status.statTimer <= 0f)
		        	{
		        		if(status.name == Status.exhaust)
		        		{
		        			enemy.animator.speed = 1f;
		        		}
		        		remove = status;
		        		pullCheck = true;
		        		//remove
		        	}

		        	if(status.name == Status.struggle && staminaVar != null)
		        	{
		        		staminaVar = status;
		        	}
	       		}

	        	if(enemy.currentStamina < 0f && statuses.Contains(staminaVar))
				{
					statuses.Remove(staminaVar);
					statuses.Add(new StatusEffect(Status.exhaust, enemy.maxStamina, 0f));
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
