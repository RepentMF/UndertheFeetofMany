using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Gameplay,
    Paused,
    Interacting
}

public class GameStateManager : GenericSingleton<GameStateManager>
{
    public GameState CurrentGameState { get; private set; }

    public delegate void GameStateChangedHandler(GameState newGameState);
    public event GameStateChangedHandler OnGameStateChanged;
    private PlayerInput PlayerInputScript;
    private InputController InputControllerScript;

    private void SetGameState(GameState newGameState)
    {
        if (CurrentGameState == newGameState)
        {
            return;
        }
        switch (newGameState)
        {
            case GameState.Gameplay:
                PlayerInputScript.SwitchCurrentActionMap("Player");
                break;
            case GameState.Paused:
                PlayerInputScript.SwitchCurrentActionMap("Paused");
                break;
            case GameState.Interacting:
                PlayerInputScript.SwitchCurrentActionMap("Interacting");
                break;
            default:
                break;
        }

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(CurrentGameState);
    }

    public void StartGameplay()
    {
        SetGameState(GameState.Gameplay);
    }

    public bool IsGameplay()
    {
        return CurrentGameState == GameState.Gameplay;
    }

    public void PauseGame()
    {
        SetGameState(GameState.Paused);
    }

    public bool IsPaused()
    {
        return CurrentGameState == GameState.Paused;
    }

    public void StartInteracting()
    {
        SetGameState(GameState.Interacting);
    }

    public bool IsInteracting()
    {
        return CurrentGameState == GameState.Interacting;
    }

    void Awake()
    {
        PlayerInputScript = this.gameObject.GetComponent<PlayerInput>();
        InputControllerScript = new InputController();
        StartGameplay();
    }
    
    void FixedUpdate()
    {
        //Debug.Log(CurrentGameState);
    }
}