using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionState
{
    Idle,
    Move,
    Dodge,
    Attack,
    Stagger,
    Juggle,
    Freefall,
    Death,
    None // Used in Hitbox logic for the knife
}

public class ActionStateAnimation
{
    public string AnimationName;
    public float AnimationTimer;
    public float AnimationStartFrame;
    public float AnimationBufferThreshold;

    public ActionStateAnimation(string animationName, float animationTimer, float animationStartFrame)
    {
        AnimationName = animationName;
        AnimationTimer = animationTimer < 0.0f ? 0.0f : animationTimer;
        AnimationStartFrame = animationStartFrame < 0.0f ? 0.0f : animationStartFrame;
        AnimationBufferThreshold = 0.0f;
    }

    public ActionStateAnimation(string animationName, float animationTimer)
    {
        AnimationName = animationName;
        AnimationTimer = animationTimer < 0.0f ? 0.0f : animationTimer;
        AnimationStartFrame = 0.0f;
        AnimationBufferThreshold = 0.0f;
    }

    public ActionStateAnimation(string animationName)
    {
        AnimationName = animationName;
        AnimationTimer = 0.0f;
        AnimationStartFrame = 0.0f;
        AnimationBufferThreshold = 0.0f;
    }

    public ActionStateAnimation()
    {
        AnimationName = "";
        AnimationTimer = 0.0f;
        AnimationStartFrame = 0.0f;
        AnimationBufferThreshold = 0.0f;
    }
}

public class StateManager : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Boolean RandomizeStartFrame = false;
    [SerializeField] public string IdleAnimationName = "idle";
    [SerializeField] public string MoveAnimationName = "move";
    [SerializeField] public float MoveAnimationTimer = -1.0f;
    [SerializeField] public string DodgeAnimationName = "Dodging";
    [SerializeField] public float DodgeAnimationTimer = -1.0f;
    [SerializeField] public float DodgeAnimationBufferThreshold = 0.0f;
    private ActionStateAnimation AttackAnimation = new ActionStateAnimation();
    [SerializeField] private string StaggerAnimationName = "stagger";
    [SerializeField] private string JuggleAnimationName = "stagger";
    [SerializeField] private string FreefallAnimationName = "stagger";
    [SerializeField] private string DeathAnimationName = "death";
    [SerializeField] private float DeathAnimationTimer = -1.0f;
    public ActionState CurrentState { get; set; } = ActionState.Idle;
    private ActionStateAnimation NextAnimation = new ActionStateAnimation();
    private ActionStateAnimation CurrentAnimation = new ActionStateAnimation();
    public float CooldownTimer { get; set; }

    // Script References
    private Animator AnimatorScript;
    private Rigidbody2D Rigidbody2DScript;

    [Header("Runtime Values")]
    [SerializeField] private bool DisplayRuntimeValues = false;
    [SerializeField] private ActionState RuntimeCurrentState;
    [SerializeField] private string RuntimeCurrentAnimationName;
    [SerializeField] private float RuntimeCurrentAnimationTimer;
    [SerializeField] private float RuntimeCooldownTimer;
    [SerializeField] private float RuntimeCurrentAnimationBufferThreshold;

    /// <summary>
    /// Updates the Runtime variables for debugging purposes when DisplayRuntimeValues is true
    /// </summary>
    private void UpdateRuntimeValues()
    {
        if (DisplayRuntimeValues)
        {
            RuntimeCurrentState = CurrentState;
            RuntimeCurrentAnimationName = CurrentAnimation.AnimationName;
            RuntimeCurrentAnimationTimer = CurrentAnimation.AnimationTimer;
            RuntimeCooldownTimer = CooldownTimer;
            RuntimeCurrentAnimationBufferThreshold = CurrentAnimation.AnimationBufferThreshold;
        }
    }

    private IEnumerator DetermineCurrentAnimation()
    {
        switch (CurrentState)
        {
            case ActionState.Idle:
                SetNextAnimation(IdleAnimationName);
                if (Rigidbody2DScript != null)
                {
                    Rigidbody2DScript.velocity = Vector2.zero;
                    Rigidbody2DScript.gravityScale = 0.0f;
                }
                break;
            case ActionState.Move:
                SetNextAnimation(MoveAnimationName, MoveAnimationTimer);
                break;
            case ActionState.Dodge:
                SetNextAnimation(DodgeAnimationName, DodgeAnimationTimer, 0, DodgeAnimationBufferThreshold);
                break;
            case ActionState.Attack:
                NextAnimation = AttackAnimation;
                break;
            case ActionState.Stagger:
                SetNextAnimation(StaggerAnimationName);
                break;
            case ActionState.Juggle:
                SetNextAnimation(JuggleAnimationName);
                break;
            case ActionState.Freefall:
                SetNextAnimation(FreefallAnimationName);
                break;
            case ActionState.Death:
                SetNextAnimation(DeathAnimationName, DeathAnimationTimer);
                break;
            default:
                SetNextAnimation();
                break;
        }
        if (RandomizeStartFrame)
        {
            RandomizeNextAnimationStartFrame();
        }
        // If there is a NextAnimation and it's not already playing, play that animation
        if ((NextAnimation.AnimationName != CurrentAnimation.AnimationName) || (NextAnimation.AnimationName != "idle" && NextAnimation.AnimationName == CurrentAnimation.AnimationName && CurrentAnimation.AnimationTimer <= 0.0f))
        {
            AnimatorScript.Play(NextAnimation.AnimationName, -1, NextAnimation.AnimationStartFrame);
            // Properties have to be copied individually so as not to copy reference and memory and have two variables pointing to the same object data
            CurrentAnimation.AnimationName = NextAnimation.AnimationName;
            CurrentAnimation.AnimationBufferThreshold = NextAnimation.AnimationBufferThreshold;
            if (NextAnimation.AnimationTimer == -1)
            {
                yield return new WaitForEndOfFrame();
                // In order to properly read the animation length, we must wait until the end of the current frame before reading the value
                CurrentAnimation.AnimationTimer = AnimatorScript.GetCurrentAnimatorStateInfo(0).length;
            }
            else
            {
                CurrentAnimation.AnimationTimer = NextAnimation.AnimationTimer;
            }
        }
    }

    private void SetNextAnimation(string animationName = "", float animationTimer = 0.0f, float animationStartFrame = 0.0f, float animationBufferThreshold = 0.0f)
    {
        NextAnimation.AnimationName = animationName;
        NextAnimation.AnimationTimer = animationTimer;
        NextAnimation.AnimationStartFrame = animationStartFrame;
        NextAnimation.AnimationBufferThreshold = animationBufferThreshold;
    }

    private void RandomizeNextAnimationStartFrame()
    {
        NextAnimation.AnimationStartFrame = UnityEngine.Random.Range(0.0f, 1.0f);
    }

    /// <summary>
    /// Checks to see if the animation playing matches the given name
    /// </summary>
    /// <returns>
    /// True if the animation names match; false otherwise
    /// </returns>
    private bool CheckPlayingAnimation(string animationName)
    {
        return AnimatorScript.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    /// <summary>
    /// Removes the gameObject once the death animation finishes playing
    /// </summary>
    private void RemoveEntity()
    {
        if (CurrentAnimation.AnimationName == DeathAnimationName && CurrentAnimation.AnimationTimer <= 0 && CurrentState == ActionState.Death)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetAttackAnimation(string animationName, float animationTimer, float animationBufferThreshold = 0.0f)
    {
        AttackAnimation.AnimationName = animationName;
        AttackAnimation.AnimationTimer = animationTimer;
        AttackAnimation.AnimationBufferThreshold = animationBufferThreshold;
    }

    public string GetCurrentAnimationName()
    {
        return CurrentAnimation.AnimationName;
    }

    public float GetCurrentAnimationTimer()
    {
        return CurrentAnimation.AnimationTimer;
    }

    private void ManageTimers()
    {
        if (CurrentAnimation.AnimationTimer > 0.0f)
        {
            CurrentAnimation.AnimationTimer -= Time.deltaTime;
        }
        else if (CooldownTimer > 0.0f)
        {
            CooldownTimer -= Time.deltaTime;
        }
    }

    public bool HasReachedAttackAnimationBufferThreshold()
    {
        if (CurrentState == ActionState.Attack && CurrentAnimation.AnimationTimer <= CurrentAnimation.AnimationBufferThreshold)
        {
            return true;
        }
        return false;
    }

    public bool HasReachedDodgeAnimationBufferThreshold()
    {
        if (CurrentState == ActionState.Dodge && CurrentAnimation.AnimationTimer <= CurrentAnimation.AnimationBufferThreshold)
        {
            return true;
        }
        return false;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        AnimatorScript = this.gameObject.GetComponent<Animator>();
        Rigidbody2DScript = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRuntimeValues();
        RemoveEntity();
        StartCoroutine(DetermineCurrentAnimation());
        ManageTimers();
    }
}