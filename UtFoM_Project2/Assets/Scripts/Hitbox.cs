using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float Damage = 0.0f;
    [SerializeField] private bool Execute = false;
    [SerializeField] private Vector2 Thrust = new Vector2(0.0f, 0.0f);
    [SerializeField] private float GravityScale = 0.0f;
    [SerializeField] private Vector2 Velocity = Vector2.negativeInfinity; // Defaulted this way to handle knife logic
    [SerializeField] private float StaggerAmount = 0.0f;
    [SerializeField] private ActionState StateToInflict = ActionState.None;
    public List<StatusEffect> StatusesToInflict;

    [SerializeField] private string HitboxName = "";

    // Script References
    private Stats StatsScript;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Hurtbox targetHurtboxScript = collider.GetComponent<Hurtbox>();
        Rigidbody2D targetRigidbody2DScript = collider.GetComponent<Rigidbody2D>();
        Stats targetStatsScript = collider.GetComponent<Stats>();
        StateManager targetStateManagerScript = collider.GetComponent<StateManager>();
        StatusMod targetStatusModScript = collider.GetComponent<StatusMod>();

        if (targetHurtboxScript != null && targetRigidbody2DScript != null && targetStatsScript != null && !targetStatsScript.IsInvulnerable)
        {
            if (targetHurtboxScript.LastHitBy != HitboxName || HitboxName.IndexOf("Knife") > -1)
            {
                StatsScript.ComboCount++; // Increase the ComboCount
                if (StateToInflict != ActionState.None) // Inflict the relevant ActionState
                {
                    targetStateManagerScript.CurrentState = StateToInflict;
                    targetHurtboxScript.CurrentStagger = StaggerAmount;
                }
                if (targetStatusModScript != null && StatusesToInflict.Count > 0) // Inflict any StatusEffect(s)
                {
                    targetStatusModScript.AddStatuses(StatusesToInflict);
                }
                targetRigidbody2DScript.gravityScale = GravityScale; // Inflict the appropriate GravityScale; This needs to happen before velocity and force
                if (Velocity != Vector2.negativeInfinity) // Inflict the appropriate velocity
                {
                    targetRigidbody2DScript.velocity = Velocity;
                    targetRigidbody2DScript.AddForce(Thrust, ForceMode2D.Force);
                }
                targetStatsScript.DamageHealth(Damage, Execute); // Damaging health should happen at the end of this logic
            }
        }
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
