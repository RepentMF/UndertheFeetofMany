using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ActivePageManager : MonoBehaviour
{
    public Image CursorDisplay;
    public Sprite EmptySlot;
    public PlayerController MainPlayer;
    private Stats MainStats;
    public Text DynamicText;
    public List<GameObject> Pages;
    public int ActivePage;
    public int Selector = 0;
    public Inventory MainInventory;
    public List<Image> TrinketImages;
    public List<Image> KeyItemImages;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = FindObjectOfType<PlayerController>();
        MainStats = MainPlayer.GetComponent<Stats>();
        MainInventory = MainPlayer.GetComponent<Inventory>();
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
                TrinketImages[i].sprite = MainInventory.TrinketsList[i].InventorySprite;
            }
        }
    }

    public void KeyItemPageDisplay()
    {
        Pages[2].SetActive(true);

        // display each key item in the inventory on each square of
        // the 4x5 grid
        for (int i = 0; i < KeyItemImages.Count; i++)
        {
            KeyItemImages[i].sprite = EmptySlot;

            if (i <= MainInventory.KeyItemsList.Count - 1)
            {
                KeyItemImages[i].gameObject.SetActive(true);
                KeyItemImages[i].sprite = MainInventory.KeyItemsList[i].InventorySprite;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MainPlayer == null)
        {
            MainPlayer = FindObjectOfType<PlayerController>();
            MainStats = MainPlayer.GetComponent<Stats>();
            MainInventory = MainPlayer.GetComponent<Inventory>();

            MainPlayer.PauseMenuReference = GetComponentInParent<PauseMenu>().gameObject;
            MainPlayer.BookInfoReference = this.gameObject;
            MainPlayer.ActivePageManagerReference = GetComponent<ActivePageManager>();
            // above 3 lines did not work, since PauseMenu and ActivePageManager are disabled at Start()
            // likely need a Component/ Script on "UI" that will assign its children at Start()
        }

        if (!GameStateManager.Instance.IsGameplay())
        {
            if (MainInventory.InteractableTarget != null)
            {
                if (MainInventory.InteractableTarget.IsSpecial)
                {
                    ActivePage = 2;
                }
            }
        }

        DisableAllPages();
        switch (ActivePage)
        {
            case 0:
                ProfilePageDisplay();
                CursorDisplay.enabled = false;
                break;
            case 1:
                TrinketPageDisplay();
                CursorDisplay.enabled = true;
                break;
            case 2:
                KeyItemPageDisplay();
                CursorDisplay.enabled = true;
                break;
        }
    }
}