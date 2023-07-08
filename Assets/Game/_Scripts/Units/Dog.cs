using System;
using UnityEngine;

namespace Game {
public class Dog : MonoBehaviour {
    [Header("Movement configs")] //
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float groundedGravity;

    [Header("Interaction configs")] //
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactRange;

    private CharacterController controller;
    private InputManager inputManager;
    private UIManager uiManager;
    private Transform cameraTransform;

    private GameObject ColliderSensorChild;
    private MeshCollider ColliderSensor;
    private GameObject InspectedInteractable;
    
    public Interactable scriptOfHeldObject = null; 
    public bool DogIsHoldingSomething = false;

    private void Awake() {
        controller = GetComponent<CharacterController>();

        ColliderSensorChild = this.gameObject.transform.GetChild(0).gameObject;
        ColliderSensor = ColliderSensorChild.GetComponent<MeshCollider>();

    }

    private void Start() {
        inputManager = InputManager.Instance;
        uiManager = UIManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Update() {
        float delta = Time.deltaTime;

        HandleMovement(delta);
        HandleRotation(delta);
        interactionChecker();

    }

    private void interactionChecker()
    {
        if (inputManager.InteractInput)
        {
            if(DogIsHoldingSomething)
            {
                scriptOfHeldObject.Interact(this);
            }
            if(InspectedInteractable != null)
            {
                if(InspectedInteractable.TryGetComponent<Interactable>(out Interactable interHolder))
                {
                    interHolder.Interact(this);
                    if (uiManager.IsInteractablePopUpActive())
                    {
                        uiManager.ToggleInteractableTextActivation(false); 
                        InspectedInteractable = null;        
                    }
                }
            }
        };

    }

    private void OnTriggerEnter(Collider other) 
    {

        if(!DogIsHoldingSomething)
        {
            if(other.gameObject.layer == 6) //Checks to see if layer is interactable
            {
                InspectedInteractable = other.gameObject;

                if (!uiManager.IsInteractablePopUpActive())
                {
                    Interactable interHolder = other.gameObject.GetComponent<Interactable>();
                    uiManager.changeinteractablePopUpContainerText(interHolder.InteractableText);
                    uiManager.ToggleInteractableTextActivation(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == InspectedInteractable) //Checks to see if layer is interactable
        {
            print(other.gameObject.name);
            if (uiManager.IsInteractablePopUpActive())
            {
                uiManager.ToggleInteractableTextActivation(false); 
                InspectedInteractable = null;        
            }
        }
    }



    // private void CheckForInteractables() { 

    //     Interactable interactable;
    //     RaycastHit hit;

    //     Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, interactRange, interactableLayer);
    //     // TODO: inefficient
    //     if (!hit.transform || !hit.transform.TryGetComponent(out interactable)) {
    //         if (uiManager.IsInteractablePopUpActive()) uiManager.ToggleInteractableTextActivation(false);
    //         return;
    //     }

    //     //TODO: change text of the popup here
    //     // string interactableText = interactable.InteractableText;
    //     // interactableUI.InteractableText.text = interactableText;
    //     uiManager.ToggleInteractableTextActivation(true);

    //     if (inputManager.InteractInput) interactable.Interact(this);
    // }

    private void HandleMovement(float delta) {
        Vector3 movement = Vector3.zero;
        movement.x = inputManager.MovementInput.x;
        movement.z = inputManager.MovementInput.y;
        movement.y = groundedGravity;
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