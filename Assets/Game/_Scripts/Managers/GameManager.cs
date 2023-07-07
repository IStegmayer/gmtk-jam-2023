using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Game
{
    public class GameManager : StaticInstance<GameManager>
    {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;
        public GameState CurrentGameState { get; private set; }

        // Kick the game off with the first state
        void Start() => ChangeState(GameState.Menu);

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

            // Debug.Log($"New state: {newState}");
        }


        private void HandleMenu() {
            //
        }

        private void HandleStarting() {
            AudioSystem.Instance.PlayMusic("BaseGameLoop");

            ChangeState(GameState.Started);
        }
        
        private void HandleGameStarted() {
            
        }

        private void HandleGameEnd() {
            AudioSystem.Instance.PlaySound("gameLose", Vector3.zero);
        }
        
    }

    [Serializable]
    public enum GameState
    {
        Menu = 0,
        Starting = 1,
        Started = 2,
        End = 5,
    }
}