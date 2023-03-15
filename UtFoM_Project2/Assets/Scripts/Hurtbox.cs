using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public float CurrentStagger { get; set; } = 0.0f;
    private Vector3 HomePosition;
    public string LastHitBy { get; set; } = "";

    // Script References
    private Rigidbody2D Rigidbody2DScript;
    private StateManager StateManagerScript;
    public ParticleSystem ParticleSystemScript;
    public ParticleSystem VFXSystemScript;

    /// <summary>
    /// When the entity is idle save their position so we know where to end freefall
    /// </summary>
    private void UpdateHomePosition()
    {
        if (StateManagerScript.CurrentState == ActionState.Idle)
        {
            HomePosition = this.transform.position;
        }
    }

    /// <summary>
    /// When the entity begins descending after being juggled, they should enter Freefall
    /// </summary>
    private void EnterFreefall()
    {
        if (Rigidbody2DScript.velocity.y < 0f && StateManagerScript.CurrentState == ActionState.Juggle)
        {
            StateManagerScript.CurrentState = ActionState.Freefall;
        }
    }

    /// <summary>
    /// When the entity falls back to their starting position, they should return to Idle
    /// </summary>
    private void EndFreefall()
    {
        if (StateManagerScript.CurrentState == ActionState.Freefall && this.transform.position.y <= HomePosition.y)
        {
            EnterIdle();
        }
    }

    /// <summary>
    /// While the enemy is staggered, decrement their CurrentStagger until they should return to Idle
    /// </summary>
    private void EndStagger()
    {
        if (StateManagerScript.CurrentState == ActionState.Stagger)
        {
            if (CurrentStagger <= 0.0f)
            {
                EnterIdle();
            }
            else
            {
                CurrentStagger -= Time.deltaTime;
            }
        }
    }

    private void EnterIdle()
    {
        LastHitBy = "";
        StateManagerScript.CurrentState = ActionState.Idle;
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
        StateManagerScript = this.gameObject.GetComponent<StateManager>();
        Rigidbody2DScript = this.gameObject.GetComponent<Rigidbody2D>();
        HomePosition = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateHomePosition();
        EndStagger();
        EnterFreefall();
        EndFreefall();
    }
}
