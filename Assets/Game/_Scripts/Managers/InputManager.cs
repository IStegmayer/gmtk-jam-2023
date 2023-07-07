using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public class InputManager : StaticInstance<InputManager>
    {
        private PlayerInputs inputActions;

        public Vector2 MovementInput { get; private set;}
        public bool InteractInput { get; private set;}

        private void OnEnable() {
            if (inputActions == null) {
                inputActions = new PlayerInputs();

                inputActions.PlayerActions.Movement.performed += input => MovementInput = input.ReadValue<Vector2>();
                inputActions.PlayerActions.Interact.performed += _ => InteractInput = true;
            }

            inputActions.Enable();
        }

        public void OnDisable() {
            // making sure to cleanup
            inputActions.Disable();
        }

        private void Update() {
        }
        
        private void LateUpdate() {
            InteractInput = false;
        }
    }
}