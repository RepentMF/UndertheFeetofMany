using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : GenericSingleton<PlayerController>
{
    private delegate void ActionDelegate();
    private ActionDelegate Action;
    private bool IsPaused = false;
    private bool IsSceneItemInRange = false;
    private SceneItem SceneItemTarget;
    private string CurrentAttackAnimationName = "";
    private bool IsShortcutButtonPressed = false;
    private Vector2 MovementVector;
    private Vector2 DodgeVector;
    [SerializeField] public float MovementSpeed = 6;
    [SerializeField] private float DodgeSpeed = 6;
    [SerializeField] private float DodgeCost = 5.0f;
    [SerializeField] private GameObject VoltTrapObject;
    [SerializeField] private GameObject SparkTriggerObject;
    [SerializeField] private GameObject FlurryFieldObject;

    // Script References
    private Animator AnimatorScript;
    private InputController InputControllerScript;
    private Inventory InventoryScript;
    private Rigidbody2D Rigidbody2DScript;
    private StateManager StateManagerScript;
    private Stats StatsScript;
    private StatusMod StatusModScript;

    private void PauseGame()
    {
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
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
        if (IsPaused || StateManagerScript.CurrentState == ActionState.Attack || StateManagerScript.CurrentState == ActionState.Stagger || StateManagerScript.CurrentState == ActionState.Juggle || StateManagerScript.CurrentState == ActionState.Freefall || StateManagerScript.CurrentState == ActionState.Death || StateManagerScript.CurrentState == ActionState.Dodge)
        {
            return false;
        }
        return true;
    }

    private bool CanAttack()
    {
        if (IsPaused || StateManagerScript.CurrentState == ActionState.Stagger || StateManagerScript.CurrentState == ActionState.Juggle || StateManagerScript.CurrentState == ActionState.Freefall || StateManagerScript.CurrentState == ActionState.Death || StateManagerScript.CurrentState == ActionState.Dodge)
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
                Debug.Log("Ice, ice, baby");
            }
            else if (InventoryScript.EquippedWeapon != null)
            {
                // Light weapon attack
                if (CurrentAttackAnimationName == "")
                {
                    CurrentAttackAnimationName = InventoryScript.EquippedWeapon.LightAttackAnimationName;
                    StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.LightAttackAnimationName, InventoryScript.EquippedWeapon.LightAttackAnimationTimer, InventoryScript.EquippedWeapon.LightAttackBufferThreshold);
                    StateManagerScript.CurrentState = ActionState.Attack;
                }
                else if (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == InventoryScript.EquippedWeapon.LightAttackAnimationName)
                {
                    CurrentAttackAnimationName = InventoryScript.EquippedWeapon.MediumAttackAnimationName;
                    StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.MediumAttackAnimationName, InventoryScript.EquippedWeapon.MediumAttackAnimationTimer, InventoryScript.EquippedWeapon.MediumAttackBufferThreshold);
                    StateManagerScript.CurrentState = ActionState.Attack;
                }
                else if (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == "Knife2")
                {
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
                Debug.Log("It's gettin' hot in here!");
            }
            else if (InventoryScript.EquippedWeapon != null)
            {
                // Launch weapon attack
                if (InventoryScript.EquippedWeapon.Name == "Knife")
                {
                    OnLightButton();
                }
                else if (InventoryScript.EquippedWeapon.Name == "Sword")
                {
                    OnLightButton();
                }
                else if (InventoryScript.EquippedWeapon.Name == "Hammer")
                {
                    if (CurrentAttackAnimationName == "" || (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == InventoryScript.EquippedWeapon.MediumAttackAnimationName))
                    {
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
                Debug.Log("Zap!");
            }
            else if (InventoryScript.EquippedWeapon != null)
            {
                // Heavy weapon attack
                if (InventoryScript.EquippedWeapon.Name == "Knife")
                {
                    OnLightButton();
                }
                else if (InventoryScript.EquippedWeapon.Name == "Sword")
                {
                    if (CurrentAttackAnimationName == "" || (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == InventoryScript.EquippedWeapon.MediumAttackAnimationName))
                    {
                        CurrentAttackAnimationName = InventoryScript.EquippedWeapon.HeavyAttackAnimationName;
                        StateManagerScript.SetAttackAnimation(InventoryScript.EquippedWeapon.HeavyAttackAnimationName, InventoryScript.EquippedWeapon.HeavyAttackAnimationTimer, InventoryScript.EquippedWeapon.HeavyAttackBufferThreshold);
                        StateManagerScript.CurrentState = ActionState.Attack;
                    }
                }
                else if (InventoryScript.EquippedWeapon.Name == "Hammer")
                {
                    if (CurrentAttackAnimationName == "" || (StateManagerScript.HasReachedAttackAnimationBufferThreshold() && CurrentAttackAnimationName == InventoryScript.EquippedWeapon.LaunchAttackAnimationName))
                    {
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
        PauseGame();
    }

    private void OnContextConfirm()
    {
        if (IsSceneItemInRange)
        {
            SceneItemTarget.PickUp(InventoryScript);
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
        float currentSpeed = StatusModScript.GetStatus(Status.Exhaust) ? MovementSpeed/1.5f : MovementSpeed;
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
    private void SetAnimatorFloats(Vector2 vector)
    {
        AnimatorScript.SetFloat("moveX", vector.x);
        AnimatorScript.SetFloat("moveY", vector.y);
    }

    private void NoAction()
    {
        CurrentAttackAnimationName = "";
        StateManagerScript.CurrentState = ActionState.Idle;
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<SceneItem>() != null)
        {
            SceneItemTarget = collider2D.GetComponent<SceneItem>();
            IsSceneItemInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<SceneItem>() != null)
        {
            IsSceneItemInRange = false;
            SceneItemTarget = null;
        }
    }

    public override void Awake()
    {
        base.Awake();
        AnimatorScript = this.gameObject.GetComponent<Animator>();
        InputControllerScript = new InputController();
        InputControllerScript.Enable();
        InventoryScript = this.gameObject.GetComponent<Inventory>();
        Rigidbody2DScript = this.gameObject.GetComponent<Rigidbody2D>();
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        StatsScript = this.gameObject.GetComponent<Stats>();
        StatusModScript = this.gameObject.GetComponent<StatusMod>();
        QualitySettings.vSyncCount = 0;
    	Application.targetFrameRate = 45;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
}