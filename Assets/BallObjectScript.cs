using UnityEngine;
using System.Collections;

namespace Game {
    public class BallObjectScript : Interactable {
        
        private bool ballIsHeld = false;

        GameObject mouthHolder; //When the ball is hold, it uses this as a reference.
        GameObject worldHolder; //When the ball is thrown, it loses its mouthHolder parent.
        GameObject dogGameObject;

        Rigidbody rb;

        float throwForce = 500;
        float throwAngle = 50;

        public void Start()
        {
            mouthHolder = GameObject.Find("Dog/Model/Head/Mouth");
            worldHolder = GameObject.Find("InteractableObjects");
            dogGameObject = GameObject.Find("Dog");

            rb = GetComponent<Rigidbody>();

        }

        public override void Interact(Dog dog) 
        {
            print("interactionMade");
            if (ballIsHeld)
            {
                throwBall();
            }
            else
            {
                pickUpBall();
            }
        }

        public void throwBall()
        {
            ballIsHeld=false;
            rb.useGravity = true;
            transform.parent = worldHolder.transform;

            // GameObject. 
            print(dogGameObject);
            Vector3 throwVector = dogGameObject.transform.forward * throwForce;
            throwVector = Quaternion.AngleAxis(-throwAngle,dogGameObject.transform.right) * throwVector;

            rb.AddForce(throwVector);

        }

        public void pickUpBall()
        {
            ballIsHeld=true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            transform.parent = mouthHolder.transform;
            transform.position = mouthHolder.transform.position;

            print("Ball Pickedup");
            
        }
    }
}
