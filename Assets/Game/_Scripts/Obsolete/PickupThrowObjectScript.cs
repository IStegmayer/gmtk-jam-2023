using UnityEngine;
using System.Collections;

namespace Game {
    public class PickupThrowObjectScript : Interactable {
        
        private bool ObjectIsHeld = false;

        GameObject mouthHolder; //When the Object is hold, it uses this as a reference.
        GameObject worldHolder; //When the Object is thrown, it loses its mouthHolder parent.
        GameObject dogGameObject;

        Rigidbody rb;

        public float throwForce = 500;
        public float throwAngle = 50;

        public void Start()
        {
            mouthHolder = GameObject.Find("Dog/Model/Head/Mouth");
            worldHolder = GameObject.Find("InteractableObjects");
            dogGameObject = GameObject.Find("Dog");

            rb = GetComponent<Rigidbody>();

        }

        public void FixedUpdate()
        {
            if(ObjectIsHeld)
            {
                transform.parent = mouthHolder.transform;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        public override void Interact(Dog dog) 
        {
            // print("interactionMade");
            if (ObjectIsHeld)
            {
                dog.scriptOfHeldObject = null;
                throwObject();
            }
            else
            {
                dog.scriptOfHeldObject = this;
                pickUpObject();
            }
        }

        public void throwObject()
        {
            
            ObjectIsHeld=false;
            rb.useGravity = true;
            transform.parent = worldHolder.transform;
            dogGameObject.GetComponent<Dog>().DogIsHoldingSomething = false;

            Vector3 throwVector = dogGameObject.transform.forward * throwForce;
            throwVector = Quaternion.AngleAxis(-throwAngle,dogGameObject.transform.right) * throwVector;

            rb.AddForce(throwVector);
        }

        public void pickUpObject()
        {
            
            ObjectIsHeld=true;
            rb.useGravity = false;
            
            dogGameObject.GetComponent<Dog>().DogIsHoldingSomething = true;

            transform.parent = mouthHolder.transform;
            transform.position = mouthHolder.transform.position;
            
        }
    }
}
