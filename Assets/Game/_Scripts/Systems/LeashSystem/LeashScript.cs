using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeashScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ownerHand;

    public GameObject collar;

    public GameObject leashHandle;
    private Rigidbody leashHandleRb;

    public float maxLeashLength = 5f;
    // public GameObject 

    public bool leashIsHeld = false;

    private float leashLength;

    void Start()
    {
        leashHandleRb = leashHandle.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 leashLinearVector = collar.transform.position - leashHandle.transform.position;
        leashLength = leashLinearVector.magnitude;

        if(leashLength > maxLeashLength)
        {
            leashHandleRb.AddForce((leashLinearVector/leashLength)*Mathf.Pow(maxLeashLength-leashLength,4));
        }

    }

    void ownerGrabLeash()
    {
        leashIsHeld = true;
    }


}
