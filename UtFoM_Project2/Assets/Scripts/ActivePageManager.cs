using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ActivePageManager : MonoBehaviour
{
    public Image CursorDisplay;
    private PlayerController MainPlayer;
    private Stats MainStats;
    public Text DynamicText;
    public List<GameObject> Pages;
    public int ActivePage;
    public int Selector = 0;
    private Inventory MainInventory;
    public List<Image> TrinketImages;
    public List<Image> KeyItemImages;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = FindObjectOfType<PlayerController>();
        MainStats = MainPlayer.GetComponent<Stats>();
        MainInventory = MainPlayer.GetComponent<Inventory>();

        ActivePage = 1;
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
        for (int i = 0; i < MainInventory.TrinketsList.Count; i++)
        {
            if (MainInventory.TrinketsList[i] != null)
            {
                TrinketImages[i].gameObject.SetActive(true);
                TrinketImages[i].sprite = MainInventory.TrinketsList[i].Sprite;
            }
        }
    }

    void KeyItemPageDisplay()
    {
        Pages[2].SetActive(true);

        // display each key item in the inventory on each square of
        // the 4x5 grid
        for (int i = 0; i < MainInventory.KeyItemsList.Count; i++)
        {
            if (MainInventory.KeyItemsList[i] != null)
            {
                KeyItemImages[i].gameObject.SetActive(true);
                KeyItemImages[i].sprite = MainInventory.KeyItemsList[i].Sprite;
            }
        }
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
            case 2:
                KeyItemPageDisplay();
                break;
        }
    }
}