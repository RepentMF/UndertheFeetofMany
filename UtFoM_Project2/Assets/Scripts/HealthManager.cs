using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	public float currentHealth;
	public float maxHealth;
	public Image healthBar;
	public PlayerMovement player;

	public void UpdateHealth()
	{
		// currentHealth = player.currentHealth;
		// maxHealth = player.maxHealth;

		healthBar.fillAmount = player.currentHealth / player.maxHealth;
	}

    // Start is called before the first frame update
    void Start()
    {
		UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }
}
