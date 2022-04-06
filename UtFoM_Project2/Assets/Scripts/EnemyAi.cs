using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAi : MonoBehaviour
{
    private Transform Target;
    protected internal Vector3 Direction;
    protected internal float ActionTimer;
    protected internal float CooldownTimer;
    private float StaticCooldownTimer = 0.01f;
    private delegate void ActionDelegate();
    private ActionDelegate StartAction;
    private ActionDelegate Action;

    [Header("Movement")]
    [SerializeField] private float MovementSpeed = -1;
    [SerializeField] private float MovementRange = -1;
    [SerializeField] private float MovementCost = -1;
    [SerializeField] private bool QuadDirectionOnly = false;

    private bool InMovementRange;

    [Header("Attacks")]
    [SerializeField] private float AttackOneRange = -1;
    [SerializeField] private float AttackOneTimer = -1;
    [SerializeField] private float AttackOneCost = -1;
    [SerializeField] private string AttackOneAnimationName = "";
    private bool InAttackOneRange;
    [SerializeField] private float AttackTwoRange = -1;
    [SerializeField] private float AttackTwoTimer = -1;
    [SerializeField] private float AttackTwoCost = -1;
    [SerializeField] private string AttackTwoAnimationName = "";
    private bool InAttackTwoRange;
    [SerializeField] private float AttackThreeRange = -1;
    [SerializeField] private float AttackThreeTimer = -1;
    [SerializeField] private float AttackThreeCost = -1;
    [SerializeField] private string AttackThreeAnimationName = "";
    private bool InAttackThreeRange;

    [Header("Spells")]
    [SerializeField] private float SpellOneRange = -1;
    [SerializeField] private float SpellOneTimer = -1;
    [SerializeField] private float SpellOneCost = -1;
    [SerializeField] private string SpellOneAnimationName = "";
    private bool InSpellOneRange;

    [SerializeField] private float SpellTwoRange = -1;
    [SerializeField] private float SpellTwoTimer = -1;
    [SerializeField] private float SpellTwoCost = -1;
    [SerializeField] private string SpellTwoAnimationName = "";
    private bool InSpellTwoRange;

    [SerializeField] private float SpellThreeRange = -1;
    [SerializeField] private float SpellThreeTimer = -1;
    [SerializeField] private float SpellThreeCost = -1;
    [SerializeField] private string SpellThreeAnimationName = "";
    private bool InSpellThreeRange;

    // Script References
    private StatusMod StatusModScript; // Used only to check if Exhaust is inflicted
    private Stats StatsScript; // Used only to check current mana and stamina values
    private Animator AnimatorScript;
    protected internal Rigidbody2D Rigidbody2DScript;
    private StateManager StateManagerScript;
    private SpriteRenderer SpriteRendererScript; // Used to manage layering compared to the player

    [Header("Runtime Values")]
    [SerializeField] private bool DisplayRuntimeValues = false;
    [SerializeField] private Transform RuntimeTarget;
    [SerializeField] private Vector3 RuntimeDirection;
    [SerializeField] private bool RuntimeCanAct;
    [SerializeField] private float RuntimeActionTimer;
    [SerializeField] private float RuntimeCooldownTimer;
    [SerializeField] private bool RuntimeInMovementRange;
    [SerializeField] private bool RuntimeInAttackOneRange;
    [SerializeField] private bool RuntimeInAttackTwoRange;
    [SerializeField] private bool RuntimeInAttackThreeRange;
    [SerializeField] private bool RuntimeInSpellOneRange;
    [SerializeField] private bool RuntimeInSpellTwoRange;
    [SerializeField] private bool RuntimeInSpellThreeRange;

    /// <summary>
    /// Updates the Runtime variables for debugging purposes when DisplayRuntimeValues is true
    /// </summary>
    private void UpdateRuntimeValues()
    {
        if (DisplayRuntimeValues)
        {
            RuntimeTarget = Target;
            RuntimeDirection = Direction;
            RuntimeCanAct = CanAct();
            RuntimeActionTimer = ActionTimer;
            RuntimeCooldownTimer = CooldownTimer;
            RuntimeInMovementRange = InMovementRange;
            RuntimeInAttackOneRange = InAttackOneRange;
            RuntimeInAttackTwoRange = InAttackTwoRange;
            RuntimeInAttackThreeRange = InAttackThreeRange;
            RuntimeInSpellOneRange = InSpellOneRange;
            RuntimeInSpellTwoRange = InSpellTwoRange;
            RuntimeInSpellThreeRange = InSpellThreeRange;
        }
    }

    /// <summary>
    /// Determines if the enemy is in a state where it can act.
    /// </summary>
    /// <returns>
    /// True if the enemy can act; false otherwise.
    /// </returns>
    protected internal virtual bool CanAct()
    {
        if (StateManagerScript.CurrentState == ActionState.Attack || StateManagerScript.CurrentState == ActionState.Stagger || StateManagerScript.CurrentState == ActionState.Juggle || StateManagerScript.CurrentState == ActionState.Freefall || StateManagerScript.CurrentState == ActionState.Death)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Determines if the target is in range of any action options and sets the appropriate InRange booleans to true
    /// </summary>
    private void CheckRange()
    {
        ResetRangeCheck();
        float distance = Vector3.Distance(this.transform.position, Target.transform.position);
        // Not using Else-If statments as multiple flags can be set to true based on player position
        if (distance <= SpellThreeRange)
        {
            InSpellThreeRange = true;
        }
        if (distance <= SpellTwoRange)
        {
            InSpellTwoRange = true;
        }
        if (distance <= SpellOneRange)
        {
            InSpellOneRange = true;
        }
        if (distance <= AttackThreeRange)
        {
            InAttackThreeRange = true;
        }
        if (distance <= AttackTwoRange)
        {
            InAttackTwoRange = true;
        }
        if (distance <= AttackOneRange)
        {
            InAttackOneRange = true;
        }
        if (distance <= MovementRange)
        {
            InMovementRange = true;
        } 
    }

    /// <summary>
    /// Sets all InRange booleans to false
    /// </summary>
    private void ResetRangeCheck()
    {
        InMovementRange = false;
        InAttackOneRange = false;
        InAttackTwoRange = false;
        InAttackThreeRange = false;
        InSpellOneRange = false;
        InSpellTwoRange = false;
        InSpellThreeRange = false;
    }

    /// <summary>
    /// Checks to see if any of the InRange booleans are true
    /// </summary>
    /// <returns>
    /// True if one or more of the InRange booleans are true; false otherwise
    /// </returns>
    private bool IsInRange()
    {
        if (InMovementRange || InAttackOneRange || InAttackTwoRange || InAttackThreeRange || InSpellOneRange || InSpellTwoRange || InSpellThreeRange)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Determines the direction in which to face the target
    /// </summary>
    private void CheckDirection()
    {
        if (IsInRange() && !StatusModScript.GetStatus(Status.Exhaust))
        {
            Direction = (Target.transform.position - this.transform.position);
            if (QuadDirectionOnly)
            {
                if(Direction.x > 0 && Mathf.Abs(Direction.x) > Mathf.Abs(Direction.y))
                {
                    Direction = Vector3.right;
                }
                else if(Direction.y > 0 && Mathf.Abs(Direction.y) > Mathf.Abs(Direction.x))
                {
                    Direction = Vector3.up;
                }
                else if(Direction.x < 0 && Mathf.Abs(Direction.x) > Mathf.Abs(Direction.y))
                {
                    Direction = Vector3.left;
                }
                else if(Direction.y < 0 && Mathf.Abs(Direction.y) > Mathf.Abs(Direction.x))
                {
                    Direction = Vector3.down;
                }
            }
            SetAnimatorFloats(Direction);
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

    /// <summary>
    /// Chooses which valid action (based on mana/stamina cost and range) to take prioritizing spells over attacks and attacks over movement
    /// </summary>
    private void ChooseAction()
    {
        bool isExhausted = StatusModScript.GetStatus(Status.Exhaust);
        if (InSpellThreeRange && StatsScript.CurrentMana >= SpellThreeCost && !isExhausted)
        {
            StartAction = StartSpellThree;
            Action = SpellThree;
        }
        else if (InSpellTwoRange && StatsScript.CurrentMana >= SpellTwoCost && !isExhausted)
        {
            StartAction = StartSpellTwo;
            Action = SpellTwo;
        }
        else if (InSpellOneRange && StatsScript.CurrentMana >= SpellOneCost && !isExhausted)
        {
            StartAction = StartSpellOne;
            Action = SpellOne;
        }
        else if (InAttackThreeRange && StatsScript.CurrentStamina >= AttackThreeCost && !isExhausted)
        {
            StartAction = StartAttackThree;
            Action = AttackThree;
        }
        else if (InAttackTwoRange && StatsScript.CurrentStamina >= AttackTwoCost && !isExhausted)
        {
            StartAction = StartAttackTwo;
            Action = AttackTwo;
        }
        else if (InAttackOneRange && StatsScript.CurrentStamina >= AttackOneCost && !isExhausted)
        {
            StartAction = StartAttackOne;
            Action = AttackOne;
        }
        else if (InMovementRange && (MovementCost <= 0 || (MovementCost > 0 && StatsScript.CurrentStamina > 0)) && !isExhausted)
        {
            StartAction = Move;
            Action = NoAction;
        }
        else
        {
            StartAction = NoAction;
            Action = NoAction;
        }
    }

    /// <summary>
    /// Sets the state, timer and animation for SpellThree
    /// </summary>
    protected internal virtual void StartSpellThree()
    {
        StatsScript.DamageMana(SpellThreeCost);
        StateManagerScript.SetAttackAnimation(SpellThreeAnimationName, SpellThreeTimer);
        StateManagerScript.CurrentState = ActionState.Attack;
        SetCooldownTimer(StaticCooldownTimer);
    }

    /// <summary>
    /// REQUIRES IMPLEMENTATION: Check timer and perform logic accordingly, modify velocity, etc.
    /// </summary>
    protected internal virtual void SpellThree()
    {
        Debug.LogError("SpellThree has no implementation!");
    }

    /// <summary>
    /// Sets the state, timer and animation for SpellTwo
    /// </summary>
    protected internal virtual void StartSpellTwo()
    {
        StatsScript.DamageMana(SpellTwoCost);
        StateManagerScript.SetAttackAnimation(SpellTwoAnimationName, SpellTwoTimer);
        StateManagerScript.CurrentState = ActionState.Attack;
        SetCooldownTimer(StaticCooldownTimer);
    }

    /// <summary>
    /// REQUIRES IMPLEMENTATION: Check timer and perform logic accordingly, modify velocity, etc.
    /// </summary>
    protected internal virtual void SpellTwo()
    {
        Debug.LogError("SpellTwo has no implementation!");
    }

    /// <summary>
    /// Sets the state, timer and animation for SpellOne
    /// </summary>
    protected internal virtual void StartSpellOne()
    {
        StatsScript.DamageMana(SpellOneCost);
        StateManagerScript.SetAttackAnimation(SpellOneAnimationName, SpellOneTimer);
        StateManagerScript.CurrentState = ActionState.Attack;
        SetCooldownTimer(StaticCooldownTimer);
    }

    /// <summary>
    /// REQUIRES IMPLEMENTATION: Check timer and perform logic accordingly, modify velocity, etc.
    /// </summary>
    protected internal virtual void SpellOne()
    {
        Debug.LogError("SpellOne has no implementation!");
    }

    /// <summary>
    /// Sets the state, timer and animation for AttackThree
    /// </summary>
    protected internal virtual void StartAttackThree()
    {
        StatsScript.DamageStamina(AttackThreeCost);
        StateManagerScript.SetAttackAnimation(AttackThreeAnimationName, AttackThreeTimer);
        StateManagerScript.CurrentState = ActionState.Attack;
        SetCooldownTimer(StaticCooldownTimer);
    }

    /// <summary>
    /// REQUIRES IMPLEMENTATION: Check timer and perform logic accordingly, modify velocity, etc.
    /// </summary>
    protected internal virtual void AttackThree()
    {
        Debug.LogError("AttackThree has no implementation!");
    }

    /// <summary>
    /// Sets the state, timer and animation for AttackTwo
    /// </summary>
    protected internal virtual void StartAttackTwo()
    {
        StatsScript.DamageStamina(AttackTwoCost);
        StateManagerScript.SetAttackAnimation(AttackTwoAnimationName, AttackTwoTimer);
        StateManagerScript.CurrentState = ActionState.Attack;
        SetCooldownTimer(StaticCooldownTimer);
    }

    /// <summary>
    /// REQUIRES IMPLEMENTATION: Check timer and perform logic accordingly, modify velocity, etc.
    /// </summary>
    protected internal virtual void AttackTwo()
    {
        Debug.LogError("AttackTwo has no implementation!");
    }

    /// <summary>
    /// Sets the state, timer and animation for AttackOne
    /// </summary>
    protected internal virtual void StartAttackOne()
    {
        StatsScript.DamageStamina(AttackOneCost);
        StateManagerScript.SetAttackAnimation(AttackOneAnimationName, AttackOneTimer);
        StateManagerScript.CurrentState = ActionState.Attack;
        SetCooldownTimer(StaticCooldownTimer);
    }

    /// <summary>
    /// REQUIRES IMPLEMENTATION: Check timer and perform logic accordingly, modify velocity, etc.
    /// </summary>
    protected internal virtual void AttackOne()
    {
        Debug.LogError("AttackOne has no implementation!");
    }

    /// <summary>
    /// Sets the state and timer for Movement
    /// </summary>
    protected internal virtual void Move()
    {
        StatsScript.DamageStamina(MovementCost*Time.deltaTime);
        StateManagerScript.CurrentState = ActionState.Move;
        Rigidbody2DScript.velocity = Direction.normalized * MovementSpeed;
    }

    /// <summary>
    /// Empty function for when the AI has no targets in range or no attack options available
    /// </summary>
    protected internal virtual void NoAction() 
    {
        StateManagerScript.CurrentState = ActionState.Idle;
    }

    /// <summary>
    /// Checks the StateManagerScript for the ActionTimer and CooldownTimer, and if the ActionTimer hits 0 then sets state to idle
    /// </summary>
    private void CheckTimers()
    {
        ActionTimer = StateManagerScript.GetCurrentAnimationTimer();
        CooldownTimer = StateManagerScript.CooldownTimer;
        if (ActionTimer <= 0 && (StateManagerScript.CurrentState == ActionState.Attack || StateManagerScript.CurrentState == ActionState.Move))
        {
            StateManagerScript.CurrentState = ActionState.Idle;
        }
    }

    /// <summary>
    /// Sets the CooldownTimer in StateManagerScript based on the given cooldownTimer
    /// </summary>
    protected internal virtual void SetCooldownTimer(float cooldownTimer)
    {
        StateManagerScript.CooldownTimer = cooldownTimer;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        // If a player has entered the enemy's space
        if (collider.GetComponent<PlayerController>() != null)
        {
            if (this.gameObject.transform.position.y < collider.bounds.center.y)
            {
                SpriteRendererScript.sortingLayerName = "Enemy_Front";
            } else
            {
                SpriteRendererScript.sortingLayerName = "Enemy_Back";
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // If a player has exited the enemy's space
        if (collider.GetComponent<PlayerController>() != null)
        {
            SpriteRendererScript.sortingLayerName = "Enemy_Back";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StatsScript = this.gameObject.GetComponent<Stats>();
        StatusModScript = this.gameObject.GetComponent<StatusMod>();
        AnimatorScript = this.gameObject.GetComponent<Animator>();
        Rigidbody2DScript = this.gameObject.GetComponent<Rigidbody2D>();
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        SpriteRendererScript = this.gameObject.GetComponent<SpriteRenderer>();
        Target = GameObject.FindWithTag("P1").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRuntimeValues();
        CheckTimers();
        if (StateManagerScript.CurrentState == ActionState.Attack)
        {
            Action();
        }
        else if (CanAct())
        {
            if ((StateManagerScript.CurrentState == ActionState.Move || ActionTimer <= 0.0f) && CooldownTimer <= 0.0f)
            {
                CheckRange();
                ChooseAction();
                CheckDirection();
                StartAction();
            }
        }
    }
}
