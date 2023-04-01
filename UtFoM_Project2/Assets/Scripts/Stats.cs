using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // State Fields
    public bool IsInvulnerable { get; set; } = false;
    public int ComboCount { get; set; } = 0;

    [Header("Health")]
    [SerializeField] protected internal float OriginalHealth;
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; } = -1; // Defaulted for Start method logic
    [SerializeField] private float BaseHealthRegen;

    [Header("Stamina")]
    [SerializeField] protected internal float OriginalStamina;
    public float MaxStamina { get; set; }
    public float CurrentStamina { get; set; } = -1; // Defaulted for Start method logic
    [SerializeField] private float BaseStaminaRegen;

    [Header("Mana")]
    [SerializeField] protected internal float OriginalMana;
    public float MaxMana { get; set; }
    public float CurrentMana { get; set; } = -1; // Defaulted for Start method logic
    [SerializeField] private float BaseManaRegen;

    [Header("Lifeblood")]
    [SerializeField] protected internal float OriginalLifeblood;
    public float MaxLifeblood { get; set; }
    public float CurrentLifeblood { get; set; } = -1; // Defaulted for Start method logic
    [SerializeField] private float BaseLifebloodRegen;

    [Header("Trinket")]
    [SerializeField] protected internal int OriginalTrinketPoints;
    public int MaxTrinketPoints { get; set; }
    public int CurrentTrinketPoints { get; set; } = -1; // Defaulted for Start method logic

    // Script References
    private StatusMod StatusModScript; // Used only to inflict exhaust
    private StateManager StateManagerScript;

    [Header("Runtime Values")]
    [SerializeField] private bool DisplayRuntimeValues = false;
    [SerializeField] private bool RuntimeIsInvulnerable;
    [SerializeField] private int RuntimeComboCount;
    [SerializeField] private float RuntimeMaxHealth;
    [SerializeField] private float RuntimeCurrentHealth;
    [SerializeField] private float RuntimeMaxStamina;
    [SerializeField] private float RuntimeCurrentStamina;
    [SerializeField] private float RuntimeMaxMana;
    [SerializeField] private float RuntimeCurrentMana;
    [SerializeField] private float RuntimeMaxLifeblood;
    [SerializeField] private float RuntimeCurrentLifeblood;
    [SerializeField] private float RuntimeMaxTrinketPoints;
    [SerializeField] private float RuntimeCurrentTrinketPoints;

    /// <summary>
    /// Updates the Runtime variables for debugging purposes when DisplayRuntimeValues is true
    /// </summary>
    private void UpdateRuntimeValues()
    {
        if (DisplayRuntimeValues)
        {
            RuntimeIsInvulnerable = IsInvulnerable;
            RuntimeCurrentHealth = ComboCount;
            RuntimeMaxHealth = MaxHealth;
            RuntimeCurrentHealth = CurrentHealth;
            RuntimeMaxStamina = MaxStamina;
            RuntimeCurrentStamina = CurrentStamina;
            RuntimeMaxMana = MaxMana;
            RuntimeCurrentMana = CurrentMana;
            RuntimeMaxLifeblood = MaxLifeblood;
            RuntimeCurrentLifeblood = CurrentLifeblood;
            RuntimeMaxTrinketPoints = MaxTrinketPoints;
            RuntimeCurrentTrinketPoints = CurrentTrinketPoints;
        }
    }

    /// <summary>
    /// Damages CurrentHealth based on the damage received and increases the damage by the missing health percentage if execute damage is on.
    /// </summary>
    public void DamageHealth(float damage, bool execute = false)
    {
        if (damage > 0.0f)
        {
            if (execute)
            {
                float percent = (MaxHealth - CurrentHealth) / MaxHealth;
                damage += (percent * 4);
            }
            CurrentHealth = CurrentHealth - damage <= 0.0f ? 0.0f : CurrentHealth - damage;
        }
    }

    /// <summary>
    /// Damages CurrentStamina by the damage received.
    /// </summary>
    public void DamageStamina(float damage)
    {
        if (damage > 0.0f)
        {
            CurrentStamina = CurrentStamina - damage <= 0.0f ? 0.0f : CurrentStamina - damage;
        }
    }

    /// <summary>
    /// Damages CurrentMana by the damage received.
    /// </summary>
    public void DamageMana(float damage)
    {
        if (damage > 0.0f)
        {
            CurrentMana = CurrentMana - damage <= 0.0f ? 0.0f : CurrentMana - damage;
        }
    }

    /// <summary>
    /// Damages CurrentLifeblood by the damage received.
    /// </summary>
    public void DamageLifeblood(float damage)
    {
        if (damage > 0.0f)
        {
            CurrentLifeblood = CurrentLifeblood - damage < 0.0f ? 0.0f : CurrentLifeblood - damage;
        }
    }

    /// <summary>
    /// Heals CurrentHealth by the amount received.
    /// </summary>
    public void HealHealth(float amount)
    {
        if (amount > 0.0f)
        {
            CurrentHealth = CurrentHealth + amount > MaxHealth ? MaxHealth : CurrentHealth + amount;
        }
    }

    /// <summary>
    /// Heals CurrentStamina by the amount received.
    /// </summary>
    public void HealStamina(float amount)
    {
        if (amount > 0.0f)
        {
            CurrentStamina = CurrentStamina + amount > MaxStamina ? MaxStamina : CurrentStamina + amount;
        }
    }

    /// <summary>
    /// Heals CurrentMana by the amount received.
    /// </summary>
    public void HealMana(float amount)
    {
        if (amount > 0.0f)
        {
            CurrentMana = CurrentMana + amount > MaxMana ? MaxMana : CurrentMana + amount;
        }
    }

    /// <summary>
    /// Heals CurrentLifeblood by the amount received.
    /// </summary>
    public void HealLifeblood(float amount)
    {
        if (amount > 0.0f)
        {
            CurrentLifeblood = CurrentLifeblood + amount > MaxLifeblood ? MaxLifeblood : CurrentLifeblood + amount;
        }
    }

    /// <summary>
    /// Regenerates CurrentHealth according to the HealthRegen rate.
    /// </summary>
    public void RegenHealth()
    {
        if (CurrentHealth < MaxHealth && BaseHealthRegen > 0.0f)
        {
            CurrentHealth += BaseHealthRegen * Time.deltaTime;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }

    /// <summary>
    /// Regenerates CurrentHealth according to the regen rate.
    /// </summary>
    public void RegenHealth(float regen)
    {
        if (CurrentHealth < MaxHealth && regen > 0.0f)
        {
            CurrentHealth += regen * Time.deltaTime;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }

    /// <summary>
    /// Regenerates CurrentStamina according to the StaminaRegen rate.
    /// </summary>
    public void RegenStamina()
    {
        if (CurrentStamina < MaxStamina && BaseStaminaRegen > 0.0f)
        {
            CurrentStamina += BaseStaminaRegen * Time.deltaTime;
            if (CurrentStamina > MaxStamina)
            {
                CurrentStamina = MaxStamina;
            }
        }
    }

    /// <summary>
    /// Regenerates CurrentStamina according to the regen rate.
    /// </summary>
    public void RegenStamina(float regen)
    {
        if (CurrentStamina < MaxStamina && regen > 0.0f)
        {
            CurrentStamina += regen * Time.deltaTime;
            if (CurrentStamina > MaxStamina)
            {
                CurrentStamina = MaxStamina;
            }
        }
    }

    /// <summary>
    /// Regenerates CurrentMana according to the ManaRegen rate.
    /// </summary>
    public void RegenMana()
    {
        if (CurrentMana < MaxMana && BaseManaRegen > 0.0f)
        {
            CurrentMana += BaseManaRegen * Time.deltaTime;
            if (CurrentMana > MaxMana)
            {
                CurrentMana = MaxMana;
            }
        }
    }

    /// <summary>
    /// Regenerates CurrentMana according to the regen rate.
    /// </summary>
    public void RegenMana(float regen)
    {
        if (CurrentMana < MaxMana && regen > 0.0f)
        {
            CurrentMana += regen * Time.deltaTime;
            if (CurrentMana > MaxMana)
            {
                CurrentMana = MaxMana;
            }
        }
    }

    /// <summary>
    /// Regenerates CurrentLifeblood according to the LifebloodRegen rate.
    /// </summary>
    public void RegenLifeblood()
    {
        if (CurrentLifeblood < MaxLifeblood && BaseLifebloodRegen > 0.0f)
        {
            CurrentLifeblood += BaseLifebloodRegen * Time.deltaTime;
            if (CurrentLifeblood > MaxLifeblood)
            {
                CurrentLifeblood = MaxLifeblood;
            }
        }
    }

    /// <summary>
    /// Regenerates CurrentLifeblood according to the regen rate.
    /// </summary>
    public void RegenLifeblood(float regen)
    {
        if (CurrentLifeblood < MaxLifeblood && regen > 0.0f)
        {
            CurrentLifeblood += regen * Time.deltaTime;
            if (CurrentLifeblood > MaxLifeblood)
            {
                CurrentLifeblood = MaxLifeblood;
            }
        }
    }

    /// <summary>
    /// Adjusts CurrentState if CurrentHealth drops to or below 0.
    /// </summary>
    private void KillEntity()
    {
        if (CurrentHealth <= 0 && (StateManagerScript.CurrentState == ActionState.Idle || StateManagerScript.CurrentState == ActionState.Move))
        {
            IsInvulnerable = true;
            StateManagerScript.CurrentState = ActionState.Death;
        }
    }

    /// <summary>
    /// Inflicts Exhaust if the StatusMod script is present and the CurrentStamina drops to or below 0.
    /// </summary>
    private void InflictExhaust()
    {
        if (StatusModScript != null && CurrentStamina <= 0 && OriginalStamina > 0)
        {
            StatusModScript.AddStatus(Status.Exhaust, MaxStamina, 0f);
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
 
    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set Health
        MaxHealth = OriginalHealth;
        if (CurrentHealth == -1)
        {
            CurrentHealth = MaxHealth;
        }

        // Set Stamina
        MaxStamina = OriginalStamina;
        if (CurrentStamina == -1)
        {
            CurrentStamina = MaxStamina;
        }

        // Set Mana
        MaxMana = OriginalMana;
        if (CurrentMana == -1)
        {
            CurrentMana = MaxMana;
        }

        // Set Lifeblood
        MaxLifeblood = OriginalLifeblood;
        if (CurrentLifeblood == -1)
        {
            CurrentLifeblood = MaxLifeblood;
        }

        // Set Trinket Points
        MaxTrinketPoints = OriginalTrinketPoints;
        if (CurrentTrinketPoints == -1)
        {
            CurrentTrinketPoints = MaxTrinketPoints;
        }

        // Retrieve Script References
        StatusModScript = this.gameObject.GetComponent<StatusMod>();
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        UpdateRuntimeValues();
        InflictExhaust();
        // if (!running && inTown)
        //    RegenStamina();
        KillEntity();
    }
}
