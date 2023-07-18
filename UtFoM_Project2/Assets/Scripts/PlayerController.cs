using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : GenericSingleton<PlayerController>
{
    private delegate void ActionDelegate();
    private ActionDelegate Action;
    [SerializeField] private GameObject PauseMenuReference;
    [SerializeField] private GameObject StatsBookReference;
    [SerializeField] private GameObject BookInfoReference;
    private ActivePageManager ActivePageManagerReference;
    private bool IsSceneItemInRange = false;
    private bool IsInteractableInRange = false;
    public bool IsInteracting = false;
    private SceneItem SceneItemTarget;
    private Interactable InteractableTarget;
    private string CurrentAttackAnimationName = "";
    private bool IsShortcutButtonPressed = false;
    private bool confirm = true;
    private Vector2 MovementVector;
    private Vector2 DodgeVector;
    [SerializeField] public float MovementSpeed = 6;
    [SerializeField] private float DodgeSpeed = 6;
    [SerializeField] private float DodgeCost = 5;
    [SerializeField] private float LifebloodCost;
    [SerializeField] private string CurrentScene;
    public string NextScene;
    [SerializeField] private GameObject VoltTrapObject;
    [SerializeField] private GameObject SparkTriggerObject;
    [SerializeField] private GameObject FlurryFieldObject;

    // Script References
    private Animator AnimatorScript;
    private InputController InputControllerScript;
    private Inventory InventoryScript;
    private PlayerController PlayerScript;
    private Rigidbody2D Rigidbody2DScript;
    private StateManager StateManagerScript;
    private Stats StatsScript;
    private StatusMod StatusModScript;

    private void ToggleDisplayPauseMenu()
    {
        // Unpauses the game
        if (GameStateManager.Instance.IsPaused() && PauseMenuReference != null && StatsBookReference != null
            && BookInfoReference != null)
        {
            PauseMenuReference.SetActive(true);
            StateManager pauseMenuStateManager = PauseMenuReference.GetComponent<StateManager>();
            pauseMenuStateManager.CurrentState = ActionState.Appear;
            StatsBookReference.SetActive(false);
            ActivePageManagerReference = BookInfoReference.GetComponent<ActivePageManager>();
        }
        // Pauses the game
        else if (!GameStateManager.Instance.IsPaused() && PauseMenuReference != null && StatsBookReference != null
            && BookInfoReference != null)
        {
            StateManager pauseMenuStateManager = PauseMenuReference.GetComponent<StateManager>();
            BookInfoReference.SetActive(false);
            pauseMenuStateManager.CurrentState = ActionState.Death;
        }
    }

    /// <summary>
    /// Checks the StateManagerScript's CurrentAnimation.AnimationTimer during an attack, and if the attack timer hits 0 then sets the player state to idle
    /// </summary>
    private void CheckAttackTimer()
    {
        if (StateManagerScript.CurrentState == ActionState.Attack && StateManagerScript.GetCurrentAnimationName() == CurrentAttackAnimationName && StateManagerScript.GetCurrentAnimationTimer() <= 0.0f)
        {
            CurrentAttackAnimationName = "";
            if (InventoryScript.EquippedWeapon != null)
            {
                InventoryScript.EquippedWeapon.ResetAttackCounters();
            }
            StateManagerScript.CurrentState = ActionState.Idle;
        }
    }

    private void CheckDodgeTimer()
    {
        if (StateManagerScript.CurrentState == ActionState.Dodge && StateManagerScript.GetCurrentAnimationTimer() <= 0.0f)
        {
            StatsScript.DamageStamina(DodgeCost);
            StateManagerScript.CurrentState = ActionState.Idle;
        }
    }

    private bool CanAct()
    {
        if (!GameStateManager.Instance.IsGameplay() || StateManagerScript.CurrentState == ActionState.Attack || StateManagerScript.CurrentState == ActionState.Stagger || StateManagerScript.CurrentState == ActionState.Juggle || StateManagerScript.CurrentState == ActionState.Freefall || StateManagerScript.CurrentState == ActionState.Death || StateManagerScript.CurrentState == ActionState.Dodge)
        {
            return false;
        }
        return true;
    }

    private bool CanAttack()
    {
        if (!GameStateManager.Instance.IsGameplay() || StateManagerScript.CurrentState == ActionState.Stagger || StateManagerScript.CurrentState == ActionState.Juggle || StateManagerScript.CurrentState == ActionState.Freefall || StateManagerScript.CurrentState == ActionState.Death || StateManagerScript.CurrentState == ActionState.Dodge)
        {
            return false;
        }
        return true;
    }

    private void OnShortcutButton(InputValue value)
    {
        IsShortcutButtonPressed = value.isPressed;
    }

    private void OnLightButton()
    {
        if (CanAttack() && !StatusModScript.GetStatus(Status.Exhaust))
        {
            if (IsShortcutButtonPressed)
            {
                // FlurryField
                //Debug.Log("Ice, ice, baby");
            }
            else if (InventoryScript.EquippedWeapon != null)
            {
                // Light weapon attack
                if (CurrentAttackAnimationName == "")
                {
                    InventoryScript.EquippedWeapon.LightAttackCounter++;
                    CurrentAttackAnimationName = InventoryScript.EquippedWeapon.LightAttackAnimationName;
                    StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.LightAttackAnimationName, InventoryScript.EquippedWeapon.LightAttackAnimationTimer, InventoryScript.EquippedWeapon.LightAttackBufferThreshold);
                    StateManagerScript.CurrentState = ActionState.Attack;
                }
                else if (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == InventoryScript.EquippedWeapon.LightAttackAnimationName && InventoryScript.EquippedWeapon.LightAttackCounter < InventoryScript.EquippedWeapon.LightAttackCounterMax)
                {
                    InventoryScript.EquippedWeapon.LightAttackCounter++;
                    CurrentAttackAnimationName = InventoryScript.EquippedWeapon.MediumAttackAnimationName;
                    StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.MediumAttackAnimationName, InventoryScript.EquippedWeapon.MediumAttackAnimationTimer, InventoryScript.EquippedWeapon.MediumAttackBufferThreshold);
                    StateManagerScript.CurrentState = ActionState.Attack;
                }
                else if (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == InventoryScript.EquippedWeapon.MediumAttackAnimationName && InventoryScript.EquippedWeapon.LightAttackCounter < InventoryScript.EquippedWeapon.LightAttackCounterMax)
                {
                    InventoryScript.EquippedWeapon.LightAttackCounter++;
                    CurrentAttackAnimationName = InventoryScript.EquippedWeapon.LightAttackAnimationName;
                    StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.LightAttackAnimationName, InventoryScript.EquippedWeapon.LightAttackAnimationTimer, InventoryScript.EquippedWeapon.LightAttackBufferThreshold);
                    StateManagerScript.CurrentState = ActionState.Attack;
                }
                else if (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == "Knife2")
                {
                    InventoryScript.EquippedWeapon.LightAttackCounter++;
                    CurrentAttackAnimationName = InventoryScript.EquippedWeapon.LightAttackAnimationName;
                    StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.LightAttackAnimationName, InventoryScript.EquippedWeapon.LightAttackAnimationTimer, InventoryScript.EquippedWeapon.LightAttackBufferThreshold);
                    StateManagerScript.CurrentState = ActionState.Attack;
                }
            }
        }
    }

    private void OnLaunchButton()
    {
        if (CanAttack() && !StatusModScript.GetStatus(Status.Exhaust))
        {
            if (IsShortcutButtonPressed)
            {
                // SparkTrigger
                //Debug.Log("It's gettin' hot in here!");
            }
            else if (InventoryScript.EquippedWeapon != null)
            {
                // Launch weapon attack
                if (InventoryScript.EquippedWeapon.Name == "Kitchen Knife")
                {
                    OnLightButton();
                }
                else if (InventoryScript.EquippedWeapon.Name == "Machete Sword")
                {
                    OnLightButton();
                }
                else if (InventoryScript.EquippedWeapon.Name == "Bell Hammer")
                {
                    if (CurrentAttackAnimationName == "" || (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && ((CurrentAttackAnimationName == InventoryScript.EquippedWeapon.MediumAttackAnimationName || CurrentAttackAnimationName == InventoryScript.EquippedWeapon.LightAttackAnimationName) && InventoryScript.EquippedWeapon.LightAttackCounter >= InventoryScript.EquippedWeapon.LightAttackCounterMax) || (CurrentAttackAnimationName == InventoryScript.EquippedWeapon.LaunchAttackAnimationName && InventoryScript.EquippedWeapon.LaunchAttackCounter < InventoryScript.EquippedWeapon.LaunchAttackCounterMax)))
                    {
                        InventoryScript.EquippedWeapon.LaunchAttackCounter++;
                        CurrentAttackAnimationName = InventoryScript.EquippedWeapon.LaunchAttackAnimationName;
                        StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.LaunchAttackAnimationName, InventoryScript.EquippedWeapon.LaunchAttackAnimationTimer, InventoryScript.EquippedWeapon.LaunchAttackBufferThreshold);
                        StateManagerScript.CurrentState = ActionState.Attack;
                    }
                }
            }
        }
    }

    private void OnHeavyButton()
    {
        if (CanAttack() && !StatusModScript.GetStatus(Status.Exhaust))
        {
            if (IsShortcutButtonPressed)
            {
                // VoltTrap
                //Debug.Log("Zap!");
            }
            else if (InventoryScript.EquippedWeapon != null)
            {
                // Heavy weapon attack
                if (InventoryScript.EquippedWeapon.Name == "Kitchen Knife")
                {
                    OnLightButton();
                }
                else if (InventoryScript.EquippedWeapon.Name == "Machete Sword")
                {
                    if (CurrentAttackAnimationName == "" || (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && (((CurrentAttackAnimationName == InventoryScript.EquippedWeapon.MediumAttackAnimationName || CurrentAttackAnimationName == InventoryScript.EquippedWeapon.LightAttackAnimationName) && InventoryScript.EquippedWeapon.LightAttackCounter >= InventoryScript.EquippedWeapon.LightAttackCounterMax) || (CurrentAttackAnimationName == InventoryScript.EquippedWeapon.HeavyAttackAnimationName && InventoryScript.EquippedWeapon.HeavyAttackCounter < InventoryScript.EquippedWeapon.HeavyAttackCounterMax))))
                    {
                        InventoryScript.EquippedWeapon.HeavyAttackCounter++;
                        CurrentAttackAnimationName = InventoryScript.EquippedWeapon.HeavyAttackAnimationName;
                        StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.HeavyAttackAnimationName, InventoryScript.EquippedWeapon.HeavyAttackAnimationTimer, InventoryScript.EquippedWeapon.HeavyAttackBufferThreshold);
                        StateManagerScript.CurrentState = ActionState.Attack;
                    }
                }
                else if (InventoryScript.EquippedWeapon.Name == "Bell Hammer")
                {
                    if (CurrentAttackAnimationName == "" || (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && ((CurrentAttackAnimationName == InventoryScript.EquippedWeapon.LaunchAttackAnimationName && InventoryScript.EquippedWeapon.LaunchAttackCounter >= InventoryScript.EquippedWeapon.LaunchAttackCounterMax) || (CurrentAttackAnimationName == InventoryScript.EquippedWeapon.HeavyAttackAnimationName && InventoryScript.EquippedWeapon.HeavyAttackCounter < InventoryScript.EquippedWeapon.HeavyAttackCounterMax))))
                    {
                        InventoryScript.EquippedWeapon.HeavyAttackCounter++;
                        CurrentAttackAnimationName = InventoryScript.EquippedWeapon.HeavyAttackAnimationName;
                        StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.HeavyAttackAnimationName, InventoryScript.EquippedWeapon.HeavyAttackAnimationTimer, InventoryScript.EquippedWeapon.HeavyAttackBufferThreshold);
                        StateManagerScript.CurrentState = ActionState.Attack;
                    }
                }
            }
        }
    }

    private void OnDodge()
    {
        if (CanAct() && !StatusModScript.GetStatus(Status.Exhaust) && StateManagerScript.CurrentState != ActionState.Idle)
        {
            DodgeVector = InputControllerScript.Player.Move.ReadValue<Vector2>();
            DodgeVector.Normalize();
            StateManagerScript.CurrentState = ActionState.Dodge;
        }
    }

    private void OnHealthButton()
    {
        if (CanAct() && StatsScript.CurrentLifeblood >= LifebloodCost)
        {
            StatsScript.DamageLifeblood(LifebloodCost);
            StatsScript.HealHealth(5f);
        }
    }

    private void OnManaButton()
    {
        if (CanAct() && StatsScript.CurrentLifeblood >= LifebloodCost)
        {
            StatsScript.DamageLifeblood(LifebloodCost);
            StatsScript.HealMana(5f);
        }
    }

    private void OnStaminaButton()
    {
        if (CanAct() && StatsScript.CurrentLifeblood >= LifebloodCost)
        {
            StatsScript.DamageLifeblood(LifebloodCost);
            StatsScript.HealStamina(5f);
        }
    }

    private void OnWeaponChangeRight()
    {
        if (CanAct())
        {
            InventoryScript.ChangeWeaponForward();
        }
    }

    private void OnWeaponChangeLeft()
    {
        if (CanAct())
        {
            InventoryScript.ChangeWeaponBack();
        }
    }

    private void OnMenuButton()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            GameStateManager.Instance.StartGameplay();
        }
        else if (GameStateManager.Instance.IsGameplay())
        {
            GameStateManager.Instance.PauseGame();
        }
        ToggleDisplayPauseMenu();
    }

    // move page forward or backwards
    private void OnNextPageButton()
    {
        // if active page is at max, go to beginning- 1 is currently max
        if (ActivePageManagerReference.ActivePage == ActivePageManagerReference.Pages.Count - 1)
        {
            ActivePageManagerReference.ActivePage = 0;  
        }
        else
        {
            ActivePageManagerReference.ActivePage++;
        }

        if (ActivePageManagerReference.ActivePage == 1 || ActivePageManagerReference.ActivePage == 2)
        {
            ActivePageManagerReference.CursorDisplay.enabled = true;
        }
        else
        {
            ActivePageManagerReference.CursorDisplay.enabled = false;
        }
    }

    private void OnPreviousPageButton()
    {   
        // if active page is at min, go to end- 0 will always be min
        if (ActivePageManagerReference.ActivePage == 0)
        {
            ActivePageManagerReference.ActivePage = 2;  
        }
        else
        {
            ActivePageManagerReference.ActivePage--;
        }

        if (ActivePageManagerReference.ActivePage == 1 || ActivePageManagerReference.ActivePage == 2)
        {
            ActivePageManagerReference.CursorDisplay.enabled = true;
        }
        else
        {
            ActivePageManagerReference.CursorDisplay.enabled = false;
        }
    }
    
    // move cursor in 4 directions
    private void OnMoveCursorDownButton()
    {
        // if on trinket or key items page, currently
        if (ActivePageManagerReference.Pages[1].active || 
            ActivePageManagerReference.Pages[2].active)
        {
            if (ActivePageManagerReference.Selector > 15)
            {
                ActivePageManagerReference.Selector -= 16;
            }
            else
            {
                ActivePageManagerReference.Selector += 4;
            }

            if (ActivePageManagerReference.Pages[1].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.TrinketImages[ActivePageManagerReference.Selector].transform.position;
            }
            else if (ActivePageManagerReference.Pages[2].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.KeyItemImages[ActivePageManagerReference.Selector].transform.position;
            }
        }
    }

    private void OnMoveCursorUpButton()
    {
        // if on trinket or key items page, currently
        if (ActivePageManagerReference.Pages[1].active || 
            ActivePageManagerReference.Pages[2].active)
        {
            if (ActivePageManagerReference.Selector < 4)
            {
                ActivePageManagerReference.Selector += 16;
            }
            else
            {
                ActivePageManagerReference.Selector -= 4;
            }

            if (ActivePageManagerReference.Pages[1].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.TrinketImages[ActivePageManagerReference.Selector].transform.position;
            }
            else if (ActivePageManagerReference.Pages[2].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.KeyItemImages[ActivePageManagerReference.Selector].transform.position;
            }
        }
    }

    private void OnMoveCursorLeftButton()
    {
        // if on trinket page, currently
        if (ActivePageManagerReference.Pages[1].active || 
            ActivePageManagerReference.Pages[2].active)
        {
            if ((ActivePageManagerReference.Selector % 4) == 0)
            {
                ActivePageManagerReference.Selector += 3;
            }
            else
            {
                ActivePageManagerReference.Selector--;
            }

            if (ActivePageManagerReference.Pages[1].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.TrinketImages[ActivePageManagerReference.Selector].transform.position;
            }
            else if (ActivePageManagerReference.Pages[2].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.KeyItemImages[ActivePageManagerReference.Selector].transform.position;
            }
        }
    }

    private void OnMoveCursorRightButton()
    {
        // if on trinket page, currently
        if (ActivePageManagerReference.Pages[1].active || 
            ActivePageManagerReference.Pages[2].active)
        {
            if (((ActivePageManagerReference.Selector + 1) % 4) == 0)
            {
                ActivePageManagerReference.Selector -= 3;
            }
            else
            {
                ActivePageManagerReference.Selector++;
            }

            if (ActivePageManagerReference.Pages[1].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.TrinketImages[ActivePageManagerReference.Selector].transform.position;
            }
            else if (ActivePageManagerReference.Pages[2].active)
            {
                ActivePageManagerReference.CursorDisplay.transform.position = 
                    ActivePageManagerReference.KeyItemImages[ActivePageManagerReference.Selector].transform.position;
            }
        }
    }

    // equip trinket with knife
    private void OnCursorSelectLightButton()
    {
        // if on trinket page, currently
        if (ActivePageManagerReference.Pages[1].active)
        {
            int i = ActivePageManagerReference.Selector;
            if (InventoryScript.TrinketsList[i] != null)
            {
                // ADD CONDITIONAL TO SEE IF KNIFE IS VALID OPTION
                if (InventoryScript.WeaponsList.Exists(x => x.Name == "Knife") && 
                    !InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    Weapon w = new Weapon();
                    foreach(Weapon weapon in InventoryScript.WeaponsList)
                    {
                        if (weapon.Name == "Knife")
                        {
                            w = weapon;
                        }
                    }
                    InventoryScript.EquipTrinket(InventoryScript.TrinketsList[i], w);
                }
                else if (InventoryScript.WeaponsList.Exists(x => x.Name == "Knife") && 
                    InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    Weapon w = new Weapon();
                    foreach(Weapon weapon in InventoryScript.WeaponsList)
                    {
                        if (weapon.Name == "Knife")
                        {
                            w = weapon;
                        }
                    }
                    InventoryScript.UnequipTrinket(InventoryScript.TrinketsList[i], w);
                }
            }
        }
    }

    // equip trinket with sword
    private void OnCursorSelectLaunchButton()
    {
        // if on trinket page, currently
        if (ActivePageManagerReference.Pages[1].active)
        {
            int i = ActivePageManagerReference.Selector;
            if (InventoryScript.TrinketsList[i] != null)
            {
                // ADD CONDITIONAL TO SEE IF SWORD IS VALID OPTION
                if (InventoryScript.WeaponsList.Exists(x => x.Name == "Sword") && 
                    !InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    Weapon w = new Weapon();
                    foreach(Weapon weapon in InventoryScript.WeaponsList)
                    {
                        if (weapon.Name == "Sword")
                        {
                            w = weapon;
                        }
                    }
                    InventoryScript.EquipTrinket(InventoryScript.TrinketsList[i], w);
                }
                else if (InventoryScript.WeaponsList.Exists(x => x.Name == "Sword") && 
                    InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    Weapon w = new Weapon();
                    foreach(Weapon weapon in InventoryScript.WeaponsList)
                    {
                        if (weapon.Name == "Sword")
                        {
                            w = weapon;
                        }
                    }
                    InventoryScript.UnequipTrinket(InventoryScript.TrinketsList[i], w);
                }
            }
        }
    }

    // equip trinket with hammer
    private void OnCursorSelectHeavyButton()
    {
        // if on trinket page, currently
        if (ActivePageManagerReference.Pages[1].active)
        {
            int i = ActivePageManagerReference.Selector;
            if (InventoryScript.TrinketsList[i] != null)
            {
                // ADD CONDITIONAL TO SEE IF SWORD IS VALID OPTION
                if (InventoryScript.WeaponsList.Exists(x => x.Name == "Hammer") && 
                    !InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    Weapon w = new Weapon();
                    foreach(Weapon weapon in InventoryScript.WeaponsList)
                    {
                        if (weapon.Name == "Hammer")
                        {
                            w = weapon;
                        }
                    }
                    InventoryScript.EquipTrinket(InventoryScript.TrinketsList[i], w);
                }
                else if (InventoryScript.WeaponsList.Exists(x => x.Name == "Hammer") && 
                    InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    Weapon w = new Weapon();
                    foreach(Weapon weapon in InventoryScript.WeaponsList)
                    {
                        if (weapon.Name == "Hammer")
                        {
                            w = weapon;
                        }
                    }
                    InventoryScript.UnequipTrinket(InventoryScript.TrinketsList[i], w);
                }
            }
        }
    }

    // equip trinket with player stats
    private void OnCursorSelectContextButton()
    {
        // if on trinket page, currently- equip or unequip trinket with player stats
        // TRINKET MENU SELECTION LOGIC
        if (ActivePageManagerReference.Pages[1].active)
        {
            int i = ActivePageManagerReference.Selector;
            if (InventoryScript.TrinketsList[i] != null)
            {
                if (!InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    InventoryScript.EquipTrinket(InventoryScript.TrinketsList[i], StatsScript);
                }
                else if (InventoryScript.EquippedTrinkets.Exists(x => x.Trinket == InventoryScript.TrinketsList[i]))
                {
                    InventoryScript.UnequipTrinket(InventoryScript.TrinketsList[i], StatsScript);
                }
            }
        }
        // KEY ITEM MENU SELECTION LOGIC
        else if (ActivePageManagerReference.Pages[2].active)
        {
            int i = ActivePageManagerReference.Selector;
            if (InventoryScript.KeyItemsList[i] != null)
            {
                // do logic with selected key item
            }
        }
    }

    private void OnContextConfirm()
    {
        if (CanAct() && IsSceneItemInRange)
        {
            SceneItemTarget.PickUp(InventoryScript);
        }
        else if (CanAct() && IsInteractableInRange)
        {
            InteractableTarget.Interact();
        }
    }

    public void OnInteract()
    {
        IsInteracting = true;
        InteractableTarget.Interact();
    }

    private void OnEndInteraction()
    {
        if (IsInteractableInRange)
        {
            if (InteractableTarget.IsQuestion)
            {
                // Send this to whatever function needs a flag changed
                DecideConfirmDeny();
            }
            IsInteracting = false;
            InteractableTarget.EndInteraction();
        }
    }

    public bool DecideConfirmDeny()
    {
        return confirm;
    }

    private void OnDecideLeftButton()
    {
        if (InteractableTarget.IsQuestion)
        {
            GameObject.FindGameObjectWithTag("Decide").gameObject.transform.position = GameObject.FindGameObjectWithTag("Confirm").gameObject.transform.position;
            confirm = true;
        }
    }
    
    private void OnDecideRightButton()
    {
        if (InteractableTarget.IsQuestion)
        {
            GameObject.FindGameObjectWithTag("Decide").gameObject.transform.position = GameObject.FindGameObjectWithTag("Deny").gameObject.transform.position;
            confirm = false;
        }
    }

    private void ReadInput()
    {
        MovementVector = InputControllerScript.Player.Move.ReadValue<Vector2>();
        MovementVector.Normalize();
        if (MovementVector != Vector2.zero)
        {
            Action = Move;
        }
        else
        {
            Action = NoAction;
        }
    }

    // private void VoltTrap()
    // {
    //     GameObject volt = Instantiate(VoltTrapObject);
    // 	volt.transform.position = this.gameObject.transform.position + new Vector3(.75f, .75f, 0f);
    // 	GameObject volt2 = Instantiate(VoltTrapObject);
    // 	volt2.transform.position = this.gameObject.transform.position + new Vector3(-.75f, .75f, 0f);
    // 	volt2.transform.GetComponentInParent<Attack>().thrust.x *= -1;
    // 	GameObject volt3 = Instantiate(VoltTrapObject);
    // 	volt3.transform.position = this.gameObject.transform.position + new Vector3(.75f, -.75f, 0f);
    // 	volt3.transform.GetComponentInParent<Attack>().thrust.y *= -1f;
    // 	GameObject volt4 = Instantiate(VoltTrapObject);
    // 	volt4.transform.position = this.gameObject.transform.position + new Vector3(-.75f, -.75f, 0f);
    // 	volt4.transform.GetComponentInParent<Attack>().thrust *= -1f;
    // }

    // private void SparkTrigger()
    // {
    // 	GameObject fire = Instantiate(SparkTriggerObject);
    // 	fire.transform.position = this.gameObject.transform.position;
    // 	Rigidbody2D rb = fire.GetComponentInChildren<Rigidbody2D>();

    // 	if(animator.GetFloat("moveX") > 0)
    // 	{
    // 		rb.AddForce(magicStart.right * 20f, ForceMode2D.Impulse);
    // 		fire.transform.Rotate(0, 0, 270);
    // 	}
    // 	else if(animator.GetFloat("moveY") > 0)
    // 	{
    // 		rb.AddForce(magicStart.up * 20f, ForceMode2D.Impulse);
    // 	}
    // 	else if(animator.GetFloat("moveX") < 0)
    // 	{
    // 		rb.AddForce(magicStart.right * -20f, ForceMode2D.Impulse);
    // 		fire.transform.Rotate(0, 0, 90);
    // 	}
    // 	else if(animator.GetFloat("moveY") < 0)
    // 	{
    // 		rb.AddForce(magicStart.up * -20f, ForceMode2D.Impulse);
    // 		fire.transform.Rotate(0, 0, 180);
    // 	}
    // }

    private void Move()
    {
        SetAnimatorFloats(MovementVector);
        StateManagerScript.CurrentState = ActionState.Move;
        float currentSpeed = StatusModScript.GetStatus(Status.Exhaust) ? MovementSpeed / 1.5f : MovementSpeed;
        Rigidbody2DScript.MovePosition(this.gameObject.transform.position + new Vector3(MovementVector.x * currentSpeed * Time.deltaTime, MovementVector.y * currentSpeed * Time.deltaTime, 0));
    }

    private void Dodge()
    {
        if (StateManagerScript.CurrentState == ActionState.Dodge && !StateManagerScript.HasReachedDodgeAnimationBufferThreshold())
        {
            Rigidbody2DScript.MovePosition(this.gameObject.transform.position + new Vector3(DodgeVector.x * DodgeSpeed * Time.deltaTime, DodgeVector.y * DodgeSpeed * Time.deltaTime, 0));
        }
    }

    /// <summary>
    /// Sets the moveX and moveY floats in the AnimatorScript according to the given vector
    /// </summary>
    protected internal void SetAnimatorFloats(Vector2 vector)
    {
        AnimatorScript.SetFloat("moveX", vector.x);
        AnimatorScript.SetFloat("moveY", vector.y);
    }

    private void NoAction()
    {
        CurrentAttackAnimationName = "";
        if (InventoryScript.EquippedWeapon != null)
        {
            InventoryScript.EquippedWeapon.ResetAttackCounters();
        }
        StateManagerScript.CurrentState = ActionState.Idle;
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<SceneItem>() != null)
        {
            SceneItemTarget = collider2D.GetComponent<SceneItem>();
            IsSceneItemInRange = true;
        }
        else if (collider2D.GetComponent<Interactable>() != null)
        {
            InteractableTarget = collider2D.GetComponent<Interactable>();
            IsInteractableInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<SceneItem>() != null)
        {
            IsSceneItemInRange = false;
            SceneItemTarget = null;
        }
        else if (collider2D.GetComponent<Interactable>() != null)
        {
            IsInteractableInRange = false;
            InteractableTarget = null;
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    public override void Awake()
    {
        base.Awake();
        // Gather script references
        AnimatorScript = this.gameObject.GetComponent<Animator>();
        InputControllerScript = new InputController();
        InputControllerScript.Enable();
        InventoryScript = this.gameObject.GetComponent<Inventory>();
        Rigidbody2DScript = this.gameObject.GetComponent<Rigidbody2D>();
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        StatsScript = this.gameObject.GetComponent<Stats>();
        StatusModScript = this.gameObject.GetComponent<StatusMod>();
        // Subscribe to events
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        // Initialize variables
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 45;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerScript != null)
        {
            Destroy(this.gameObject);
            return;
        }

        PlayerScript = this;
        if (transform.parent != null)
        {
            GameObject.DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentScene != NextScene)
        {
            CurrentScene = NextScene;
        }

        CheckAttackTimer();
        CheckDodgeTimer();
        if (StateManagerScript.CurrentState == ActionState.Dodge)
        {
            Dodge();
        }
        else if (CanAct())
        {
            ReadInput();
            Action();
        }
    }

    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
}