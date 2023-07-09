using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game {
public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public GameState CurrentGameState { get; private set; }

    public float LevelDuration;
    public Slider GameTimerSlider;
    private float gameTimer;
    private float score;


    // Kick the game off with the first state
    private void Start() {
        gameTimer = LevelDuration;
        ChangeState(GameState.Menu);
    }

    private void Update() {
        if (CurrentGameState != GameState.Started) return;

        if (gameTimer <= 0) {
            ChangeState(GameState.End);
        }
        else {
            gameTimer -= Time.deltaTime;
            GameTimerSlider.value += Time.deltaTime;
        }
    }

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        CurrentGameState = newState;
        switch (newState) {
            case GameState.Menu:
                HandleMenu();
                break;
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Started:
                HandleGameStarted();
                break;
            case GameState.End:
                HandleGameEnd();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }


    private void HandleMenu() {
        AudioSystem.Instance.PlayMusic("MainMenuLoop");
    }

    private void HandleStarting() {
        AudioSystem.Instance.PlayMusic("BaseGameLoop");
        GameTimerSlider.maxValue = LevelDuration;
        GameTimerSlider.value = 0f;

        ChangeState(GameState.Started);
    }

    private void HandleGameStarted() { }

    private void HandleGameEnd() {
        UIManager.Instance.EndGame();
    }
}

[Serializable]
public enum GameState {
    Menu = 0,
    Starting = 1,
    Started = 2,
    End = 5,
}
}