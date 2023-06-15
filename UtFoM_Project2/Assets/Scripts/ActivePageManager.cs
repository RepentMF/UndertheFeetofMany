using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePageManager : MonoBehaviour
{
    private PlayerController MainPlayer;
    private Stats MainStats;
    public Text DynamicText;
    public List<GameObject> Pages;
    public int ActivePage;
    private Inventory MainInventory;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = FindObjectOfType<PlayerController>();
        MainStats = MainPlayer.GetComponent<Stats>();
        MainInventory = MainPlayer.GetComponent<Inventory>();

        ActivePage = 0;
    }

    void DisableAllPages()
    {
        foreach (GameObject obj in Pages)
        {
            obj.SetActive(false);
        }
    }

    void ProfilePageDisplay()
    {
        Pages[0].SetActive(true);
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

    void TrinketPageDisplay()
    {
        Pages[1].SetActive(true);

        // display each trinket in the inventory on each square of
        // the 4x5 grid
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DisableAllPages();
        switch (ActivePage)
        {
            case 0:
                ProfilePageDisplay();
                break;
            case 1:
                TrinketPageDisplay();
                break;
        }
    }
}