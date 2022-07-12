using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	public string target;
	public Image healthBar;
	public Image manaBar;
	public Image staminaBar;
	public Image lbFlare1;
	public Image lbFlare2;
	public Image lbFlare3;
	public Stats stats;

	 // Script References
    private HealthManager HealthScript;

	public void UpdateStats()
	{
		if (healthBar != null)
		{
			healthBar.fillAmount = stats.CurrentHealth / stats.MaxHealth;
		}
		if (manaBar != null)
		{
			manaBar.fillAmount = stats.CurrentMana / stats.MaxMana;
		}
		if (staminaBar != null)
		{
			staminaBar.fillAmount = stats.CurrentStamina / stats.MaxStamina;
		}

		if((stats.CurrentLifeblood >= 0f && stats.CurrentLifeblood <= 10f) && lbFlare1 != null)
		{
			lbFlare3.fillAmount = stats.CurrentLifeblood / 10f;
			lbFlare1.gameObject.SetActive(false);
			lbFlare2.gameObject.SetActive(false);
			lbFlare3.gameObject.SetActive(true);
		}
		else if((stats.CurrentLifeblood > 10f && stats.CurrentLifeblood <= 20f) && lbFlare1 != null)
		{
			lbFlare2.fillAmount = (stats.CurrentLifeblood - 10f) / 10f;
			lbFlare1.gameObject.SetActive(false);
			lbFlare2.gameObject.SetActive(true);
			lbFlare3.gameObject.SetActive(true);
		}
		else if((stats.CurrentLifeblood > 20f && stats.CurrentLifeblood <= 30f) && lbFlare1 != null)
		{
			lbFlare1.fillAmount = (stats.CurrentLifeblood - 20f) / 10f;
			lbFlare1.gameObject.SetActive(true);
			lbFlare2.gameObject.SetActive(true);
			lbFlare3.gameObject.SetActive(true);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		HealthManager[] HMArray = FindObjectsOfType<HealthManager>();

		for(int i = 0; i < HMArray.Length; i++)
		{
			for(int j = 0; j < HMArray.Length; j++)
			{
				if(HMArray[i].target == HMArray[j].target && i != j)
    			{
    				Destroy(HMArray[j].GetComponentInParent<Canvas>().transform.parent.gameObject);
    				return;
    			}
			}
		}
		

    	HealthScript = this;
    	GameObject.DontDestroyOnLoad(GetComponentInParent<Canvas>().transform.parent.gameObject);

		UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
		UpdateStats();
    }
}
