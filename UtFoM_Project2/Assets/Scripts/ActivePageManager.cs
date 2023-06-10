using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePageManager : MonoBehaviour
{
    private PlayerController MainPlayer;
    private Stats MainStats;
    public Text DynamicText;
    public List<string> Pages;
    public string ActivePage;
    private Inventory MainInventory;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = FindObjectOfType<PlayerController>();
        MainStats = MainPlayer.GetComponent<Stats>();
        MainInventory = MainPlayer.GetComponent<Inventory>();

        Pages = new List<string>();
        Pages.Add("UI_profilepage");
        Pages.Add("UI_trinketspage");
        ActivePage = "UI_profilepage";
    }

    void ProfilePageDisplay()
    {
        string weapon;
        string trinkets = "";

        if (MainInventory.EquippedWeapon != null)
        {
            weapon = MainInventory.EquippedWeapon.Name;
        }
        else
        {
            weapon = "";
        }

        // CLEAN UP FORMATTING LATER- GRABS WHAT IT NEEDS, DOESN'T LOOK PRETTY RIGHT NOW
        if (MainInventory.EquippedTrinkets != null)
        {
            foreach (EquippedTrinket trinket in MainInventory.EquippedTrinkets)
            {
                trinkets += trinket.Trinket.Name;
            }
        }

        DynamicText.text = "HP: " + MainStats.CurrentHealth.ToString() + "/" + MainStats.MaxHealth.ToString()
            + "    " + "SP: " + MainStats.MaxStamina.ToString() + "\n" +
            "MP: " + MainStats.CurrentMana.ToString() + "/" + MainStats.MaxMana.ToString() +  
            "    " + "TP: " + MainStats.MaxTrinketPoints.ToString() + "\n" + "Current Weapon: "
            + weapon + "\n" + "Spells: " + "\n" + "Equipped Trinkets: " + trinkets;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (ActivePage)
        {
            case "UI_profilepage":
                ProfilePageDisplay();
                // disable all other inactive pages
                break;
            case "UI_trinketspage":
                // call trinket logic
                // disable all other inactive pages
                break;
        }
    }
}