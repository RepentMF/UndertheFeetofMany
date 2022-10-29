using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePageManager : MonoBehaviour
{
    private PlayerController MainPlayer;
    private Stats MainStats;
    private Inventory MainInventory;
    public Text DynamicText;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = FindObjectOfType<PlayerController>();
        MainStats = MainPlayer.GetComponent<Stats>();
        MainInventory = MainPlayer.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MainInventory.EquippedWeapon != null)
        {
            DynamicText.text = "HP: " + MainStats.CurrentHealth.ToString() + "/(" + MainStats.MaxHealth.ToString()
            + "+" + "0" + ")    " + "SP: (" + MainStats.MaxStamina.ToString() + "+" + "0" + ")" + "\n" +
            "MP: " + MainStats.CurrentMana.ToString() + "/(" + MainStats.MaxMana.ToString()+ "+" + "0" +  
            ")    " + "TP: " + "(" + MainStats.MaxTrinket.ToString() + "+" + "0" + ")" + "\n" + "Current Weapon: "
            + MainInventory.EquippedWeapon.Name + "\n" + "Spells: " + "\n" + "Trinkets: " + "\n" + "Current Quest: ";
        }
        else
        {
            DynamicText.text = "HP: " + MainStats.CurrentHealth.ToString() + "/(" + MainStats.MaxHealth.ToString()
            + "+" + "0" + ")    " + "SP: (" + MainStats.MaxStamina.ToString() + "+" + "0" + ")" + "\n" +
            "MP: " + MainStats.CurrentMana.ToString() + "/(" + MainStats.MaxMana.ToString()+ "+" + "0" +  
            ")    " + "TP: " + "(" + MainStats.MaxTrinket.ToString() + "+" + "0" + ")" + "\n" + "Current Weapon: "
            + "\n" + "Spells: " + "\n" + "Trinkets: " + "\n" + "Current Quest: ";
        }
    }
}
