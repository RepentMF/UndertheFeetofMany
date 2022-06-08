using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	public Image healthBar;
	public Image manaBar;
	public Image staminaBar;
	public Image lifebloodNotches;
	public Image lbFlare1;
	public Image lbFlare2;
	public Image lbFlare3;
	public Stats playerStats;
	public Enemy enemy;

	public void UpdatePlayerStats()
	{
		healthBar.fillAmount = playerStats.CurrentHealth / playerStats.MaxHealth;
		manaBar.fillAmount = playerStats.CurrentMana / playerStats.MaxMana;
		staminaBar.fillAmount = playerStats.CurrentStamina / playerStats.MaxStamina;

		if(playerStats.CurrentLifeblood >= 0f && playerStats.CurrentLifeblood <= 10f)
		{
			lbFlare3.fillAmount = playerStats.CurrentLifeblood / 10f;
			lbFlare1.gameObject.SetActive(false);
			lbFlare2.gameObject.SetActive(false);
			lbFlare3.gameObject.SetActive(true);
		}
		else if(playerStats.CurrentLifeblood > 10f && playerStats.CurrentLifeblood <= 20f)
		{
			lbFlare2.fillAmount = (playerStats.CurrentLifeblood - 10f) / 10f;
			lbFlare1.gameObject.SetActive(false);
			lbFlare2.gameObject.SetActive(true);
			lbFlare3.gameObject.SetActive(true);
		}
		else if(playerStats.CurrentLifeblood > 20f && playerStats.CurrentLifeblood <= 30f)
		{
			lbFlare1.fillAmount = (playerStats.CurrentLifeblood - 20f) / 10f;
			lbFlare1.gameObject.SetActive(true);
			lbFlare2.gameObject.SetActive(true);
			lbFlare3.gameObject.SetActive(true);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		UpdatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
		UpdatePlayerStats();
    }
}
