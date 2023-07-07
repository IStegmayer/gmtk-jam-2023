using System;
using UnityEngine;

namespace Game {
public class Dog : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private CharacterController controller;
    private InputManager inputManager;
    private Transform cameraTransform;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void Start() {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Update() {
        float delta = Time.deltaTime;

        HandleMovement(delta);
        HandleRotation(delta);
    }

    private void HandleMovement(float delta) {
        Vector3 movement = Vector3.zero;
        movement.x = inputManager.MovementInput.x;
        movement.z = inputManager.MovementInput.y;
        controller.Move(movement * delta * speed);
    }

    private void HandleRotation(float delta) {
        Vector3 targetDir = Vector3.zero;
        targetDir = cameraTransform.forward * inputManager.MovementInput.y;
        targetDir += cameraTransform.right * inputManager.MovementInput.x;
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero) targetDir = transform.forward;

        Quaternion transformRotation = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, transformRotation, rotationSpeed * delta);

        transform.rotation = targetRotation;
    }
}
}