using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Status
{
    None,
    Bleed,
    Exhaust,
    Leech,
    Poison,
    Struggle
}

[System.Serializable]
public class StatusEffect
{
    public float StatTimer;
    private float OriginalTimer;
    public float Intensity;
    public Status Name;
    public Stats Inflicter;

    public StatusEffect(Status name, float statTimer, float intensity)
    {
        this.Name = name;
        this.StatTimer = statTimer;
        this.OriginalTimer = statTimer;
        this.Intensity = intensity;
    }

    public StatusEffect(Status name, float statTimer, float intensity, Stats inflicter)
    {
        this.Name = name;
        this.StatTimer = statTimer;
        this.OriginalTimer = statTimer;
        this.Intensity = intensity;
        this.Inflicter = inflicter;
    }
}

public class StatusMod : MonoBehaviour
{
    private Stats StatsScript;
    private Animator AnimatorScript;
    private List<StatusEffect> Statuses = new List<StatusEffect>();

    [Header("Runtime Values")]
    [SerializeField] private bool DisplayRuntimeValues = false;
    [SerializeField] private List<StatusEffect> RuntimeStatuses;

    /// <summary>
    /// Updates the Runtime variables for debugging purposes when DisplayRuntimeValues is true
    /// </summary>
    private void UpdateRuntimeValues()
    {
        if (DisplayRuntimeValues)
        {
            RuntimeStatuses = Statuses;
        }
    }

    /// <summary>
    /// Determines if the given status is currently inflicted on the entity.
    /// </summary>
    /// <returns>
    /// True if the status is currently inflicted; false otherwise.
    /// </returns>
    public bool GetStatus(Status name)
    {
        foreach (StatusEffect StatusEntry in Statuses)
        {
            if (StatusEntry.Name == name)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Adds or updates the proprties of the given status on the entity
    /// </summary>
    public void AddStatus(Status name, float statTimer, float intensity, Stats inflicter = null)
    {
        // Condition Defaults
        bool canAddStatus = true;
        int indexOfStruggle = -1;
        int indexOfExhaust = -1;

        int index = 0; // Track the index while iterating through the list
        foreach (StatusEffect statusEntry in Statuses)
        {
            if (statusEntry.Name == name) // If the status is being inflicted again, modify the original entry
            {
                canAddStatus = false;
                statusEntry.StatTimer = statusEntry.Intensity > intensity ? statusEntry.StatTimer : statTimer; // Always use the timer for the higher intensity
                statusEntry.Intensity = statusEntry.Intensity > intensity ? statusEntry.Intensity : intensity; // Always use the higher intensity
            }

            if (statusEntry.Name == Status.Exhaust)
            {
                indexOfExhaust = index; // Exhaust is present
            }
            else if (statusEntry.Name == Status.Struggle)
            {
                indexOfStruggle = index; // Struggle is present
            }
            index++; // Track the index while iterating through the list
        }

        if (name == Status.Exhaust && indexOfStruggle != -1) // When Exhaust is inflicted, remove Struggle if it's present
        {
            Statuses.RemoveAt(indexOfStruggle);
        }
        else if (name == Status.Struggle && indexOfExhaust != -1) // Do not add Struggle if the target is already Exhausted
        {
            canAddStatus = false;
        }

        if (canAddStatus) // This is a new status and should be added to the list
        {
            Statuses.Add(inflicter == null ? new StatusEffect(name, statTimer, intensity) : new StatusEffect(name, statTimer, intensity, inflicter));
        }
    }

    /// <summary>
    /// Adds or updates the properties for ever status in the given list
    /// </summary>
    public void AddStatuses(List<StatusEffect> statuses)
    {
        foreach (StatusEffect statusEntry in statuses)
        {
            AddStatus(statusEntry.Name, statusEntry.StatTimer, statusEntry.Intensity, statusEntry.Inflicter);
        }
    }

    /// <summary>
    /// Determines if the timer of the given status effect has dropped to or below 0.
    /// </summary>
    /// <returns>
    /// True if the status effect has timed out; false otherwise.
    /// </returns>
    private bool TimerHasReachedZero(StatusEffect status)
    {
        return status.StatTimer <= 0f;
    }

    /// <summary>
    /// Returns the animation speed to 1 if exhaust is not inflicted
    /// </summary>
    private void ResetAnimationSpeed()
    {
        if (!GetStatus(Status.Exhaust)) // If Exhaust is not present, ensure animations happen at the normal speed
        {
            AnimatorScript.speed = 1;
        }
    }

    /// <summary>
    /// Resets MaxHealth to its original value and heals CurrentHealth accordingly
    /// </summary>
    private void ResetHealthAfterPoison()
    {
        if (!GetStatus(Status.Poison) && StatsScript.MaxHealth < StatsScript.OriginalHealth)
        {
            StatsScript.MaxHealth = StatsScript.OriginalHealth;
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
        StatsScript = this.gameObject.GetComponent<Stats>();
        AnimatorScript = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRuntimeValues();
        if (Statuses.Count > 0)
        {
            foreach (StatusEffect StatusEntry in Statuses)
            {
                switch (StatusEntry.Name)
                {
                    case Status.Leech:
                        float amount = StatsScript.CurrentHealth - (StatusEntry.Intensity * Time.deltaTime) < 0 ? StatsScript.CurrentHealth : (StatusEntry.Intensity * Time.deltaTime);
                        StatsScript.DamageHealth(amount);
                        StatusEntry.Inflicter.HealHealth(amount);
                        break;
                    case Status.Poison:
                        if (StatsScript.MaxHealth == StatsScript.OriginalHealth) // If the target's health has not been halved, halve the target's maxHealth
                        {
                            StatsScript.MaxHealth /= 2;
                        }
                        if (StatsScript.MaxHealth < StatsScript.CurrentHealth) // Ensure the target's current health doesn't exceed their max
                        {
                            StatsScript.CurrentHealth = StatsScript.MaxHealth;
                        }
                        break;
                    case Status.Bleed:
                        StatsScript.DamageHealth(StatusEntry.Intensity);
                        break;
                    case Status.Struggle:
                        StatsScript.DamageStamina(StatusEntry.Intensity);
                        break;
                    case Status.Exhaust:
                        AnimatorScript.speed = 0.5f;
                        StatsScript.RegenStamina();
                        break;
                    default:
                        break;

                }
                StatusEntry.StatTimer -= Time.deltaTime;
            }
            // Modifications to the order/contents of the Statuses List should happen below this line
            Statuses.RemoveAll(TimerHasReachedZero); // If any status effects have timed out, remove them from the list
            ResetAnimationSpeed();
            ResetHealthAfterPoison();
        }
    }
}
