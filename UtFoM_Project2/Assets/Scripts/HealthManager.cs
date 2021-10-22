using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	public float currentHealth;
	public float maxHealth;
	public float currentMana;
	public float maxMana;
	public float currentStamina;
	public float maxStamina;
	public float currentLifeblood;
	public float maxLifeblood;
	public float lifebloodDecimal;

	public Image healthBar;
	public Image manaBar;
	public Image staminaBar;
	public Image lifebloodNotches;
	public Image lbFlare1;
	public Image lbFlare2;
	public Image lbFlare3;
	public PlayerMovement player;
	public Enemy enemy;

	public void UpdatePlayerStats()
	{
		healthBar.fillAmount = player.currentHealth / player.maxHealth;
		manaBar.fillAmount = player.currentMana / player.maxMana;
		staminaBar.fillAmount = player.currentStamina / player.maxStamina;

		if(player.currentLifeblood >= 0f && player.currentLifeblood <= 10f)
		{
			lifebloodNotches.fillAmount = 0.032f + (0.0133f * player.currentLifeblood);
			lbFlare1.gameObject.SetActive(false);
			lbFlare2.gameObject.SetActive(false);
			lbFlare3.gameObject.SetActive(false);
		}
		else if(player.currentLifeblood > 10f && player.currentLifeblood <= 20f)
		{
			lifebloodNotches.fillAmount = 0.434f + (0.0133f * (player.currentLifeblood - 10f));
			lbFlare1.gameObject.SetActive(true);
			lbFlare2.gameObject.SetActive(false);
			lbFlare3.gameObject.SetActive(false);
		}
		else if(player.currentLifeblood > 20f && player.currentLifeblood <= 30f)
		{
			lifebloodNotches.fillAmount = 0.835f + (0.0133f * (player.currentLifeblood - 20f));
			lbFlare1.gameObject.SetActive(true);
			lbFlare2.gameObject.SetActive(true);
			lbFlare3.gameObject.SetActive(false);
			if(player.currentLifeblood == 30f)
			{
				lbFlare3.gameObject.SetActive(true);
			}
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
