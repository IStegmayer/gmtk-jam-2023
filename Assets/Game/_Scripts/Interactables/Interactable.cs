using System;
using UnityEngine;

namespace Game
{
    public abstract class Interactable : MonoBehaviour
    {
        public string InteractableText;

        public virtual void Interact(Dog dog) {
            Debug.Log("Interacted with object");
        }
    }
}