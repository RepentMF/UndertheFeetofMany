using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitboxType
{
    Light,
    Medium,
    Launch,
    Heavy
}

public class Hitbox : MonoBehaviour
{
    [SerializeField] protected internal string HitboxName = "";
    [SerializeField] private HitboxType Type;
    [SerializeField] public Weapon WeaponInfo;
    [SerializeField] private Vector2 Thrust = new Vector2(0.0f, 0.0f); //  This controls distance of movement during hit
    [SerializeField] private float GravityScale = 0.0f;
    [SerializeField] private Vector2 Velocity = new Vector2(-1.0f, -1.0f); // Defaulted this way to handle knife logic; this controls speed of movement during hit
    [SerializeField] private float StaggerAmount = 0.0f;
    [SerializeField] private ActionState StateToInflict = ActionState.None;
    [SerializeField] public int ID;


    // Script References
    private Stats StatsScript;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Hurtbox targetHurtboxScript = collider.GetComponent<Hurtbox>();
        Rigidbody2D targetRigidbody2DScript = collider.GetComponent<Rigidbody2D>();
        Stats targetStatsScript = collider.GetComponent<Stats>();
        StateManager targetStateManagerScript = collider.GetComponent<StateManager>();
        StatusMod targetStatusModScript = collider.GetComponent<StatusMod>();

        if (((GetComponentInParent<EnemyAi>() && collider.GetComponentInParent<PlayerController>()) ||
                GetComponentInParent<PlayerController>() && collider.GetComponentInParent<EnemyAi>()) &&
                collider.isTrigger)
        {
            if (targetHurtboxScript != null && targetRigidbody2DScript != null && targetStatsScript != null && !targetStatsScript.IsInvulnerable)
            {
                if (targetHurtboxScript.LastHitBy != HitboxName || HitboxName.IndexOf("Knife") > -1)
                {
                    targetHurtboxScript.LastHitBy = HitboxName; // Prevent double hits
                    StatsScript.ComboCount++; // Increase the ComboCount
                    if (StateToInflict != ActionState.None) // Inflict the relevant ActionState
                    {
                        targetStateManagerScript.CurrentState = StateToInflict;
                        targetHurtboxScript.CurrentStagger = StaggerAmount;
                    }
                    if (targetStatusModScript != null) // Inflict any StatusEffect(s)
                    {
                        AddStatuses(targetStatusModScript);
                    }
                    targetRigidbody2DScript.gravityScale = GravityScale; // Inflict the appropriate GravityScale; This needs to happen before velocity and force
                    if (Velocity.x >= 0f && Velocity.y >= 0f) // Inflict the appropriate velocity
                    {
                        targetRigidbody2DScript.velocity = Velocity;
                        targetRigidbody2DScript.AddForce(Thrust, ForceMode2D.Force);
                    }
                    DamageHealth(targetStatsScript); // Damaging health should happen at the end of this logic

                    if (this.gameObject.GetComponentInParent<Hurtbox>() != null)
                    {
                        Vector3 tempPosition = collider.transform.position;
                        tempPosition.y -= .1f;
                        Instantiate(this.gameObject.GetComponentInParent<Hurtbox>().ParticleSystemScript, tempPosition, Quaternion.identity);
                        if (ID == 1 && this.gameObject.GetComponentInParent<Hurtbox>().MainVFXSystemScript != null)
                        {
                            Instantiate(this.gameObject.GetComponentInParent<Hurtbox>().MainVFXSystemScript, tempPosition, Quaternion.identity);
                        }
                        else if (ID == 0 && this.gameObject.GetComponentInParent<Hurtbox>().AltVFXSystemScript)
                        {
                            Instantiate(this.gameObject.GetComponentInParent<Hurtbox>().AltVFXSystemScript, tempPosition, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    private void AddStatuses(StatusMod target)
    {
        switch (Type)
        {
            case HitboxType.Light:
                if (WeaponInfo.LightAttackStatusesToInflict.Count > 0)
                {
                    target.AddStatuses(WeaponInfo.LightAttackStatusesToInflict);
                }
                break;
            case HitboxType.Medium:
                if (WeaponInfo.LightAttackStatusesToInflict.Count > 0)
                {
                    target.AddStatuses(WeaponInfo.LightAttackStatusesToInflict);
                }
                break;
            case HitboxType.Launch:
                if (WeaponInfo.LaunchAttackStatusesToInflict.Count > 0)
                {
                    target.AddStatuses(WeaponInfo.LaunchAttackStatusesToInflict);
                }
                break;
            case HitboxType.Heavy:
                if (WeaponInfo.HeavyAttackStatusesToInflict.Count > 0)
                {
                    target.AddStatuses(WeaponInfo.HeavyAttackStatusesToInflict);
                }
                break;
            default:
                break;
        }
    }

    private void DamageHealth(Stats target)
    {
        switch (Type)
        {
            case HitboxType.Light:
                target.DamageHealth(WeaponInfo.LightAttackDamage, WeaponInfo.LightAttackExecuteDamage);
                break;
            case HitboxType.Medium:
                target.DamageHealth(WeaponInfo.MediumAttackDamage, WeaponInfo.LightAttackExecuteDamage);
                break;
            case HitboxType.Launch:
                target.DamageHealth(WeaponInfo.LaunchAttackDamage, WeaponInfo.LaunchAttackExecuteDamage);
                break;
            case HitboxType.Heavy:
                target.DamageHealth(WeaponInfo.HeavyAttackDamage, WeaponInfo.HeavyAttackExecuteDamage);
                break;
            default:
                break;
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
        StatsScript = this.gameObject.GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
