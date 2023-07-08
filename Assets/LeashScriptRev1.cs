using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeashScriptRev1 : MonoBehaviour
{
    // Start is called before the first frame update

    public float forceConstant;

    public GameObject vendor;

    public GameObject collar;

    public GameObject leashMidPoint;

    private Rigidbody vendorRb;

    private List<GameObject> leashMidPointsList = new List<GameObject>();

    public float maxLeashLength = 20f;

    public int numberOfLeashSegments = 5;
    // public GameObject 

    public bool leashIsHeld = false;

    private float leashSegmentLength;

    void Start()
    {
        vendorRb = vendor.GetComponent<Rigidbody>();

        Vector3 leashLinearVector = collar.transform.position - vendor.transform.position;

        leashMidPointsList.Add(vendor);

        for(int i=0; i<=(numberOfLeashSegments-2); i+=1)
        {
            Vector3 spawnLocation = (i+1)*leashLinearVector/numberOfLeashSegments;

            spawnLocation -= leashLinearVector;
            spawnLocation += collar.transform.position;

            GameObject midpoint = Instantiate(leashMidPoint);
            midpoint.transform.position = spawnLocation;

            leashMidPointsList.Add(midpoint);
        }

        leashMidPointsList.Add(collar);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 leashLinearVector;
        Rigidbody rbHolder;
        // Rigidbody rbHolder2;

        for(int i=0; i<=(numberOfLeashSegments-1); i+=1){
            
            
            leashLinearVector = leashMidPointsList[i+1].transform.position - leashMidPointsList[i].transform.position;
            leashSegmentLength = leashLinearVector.magnitude;
            leashLinearVector.Normalize();
            rbHolder = leashMidPointsList[i].GetComponent<Rigidbody>();

            if(leashSegmentLength > (maxLeashLength/numberOfLeashSegments))
            {

                rbHolder.AddForce(forceConstant*leashLinearVector*leashSegmentLength);

            } else
            {
                rbHolder.velocity *= 0.9f;
            }

            if(i != 0)
            {
                leashLinearVector = leashMidPointsList[i].transform.position - leashMidPointsList[i-1].transform.position;
                leashSegmentLength = leashLinearVector.magnitude;
                leashLinearVector.Normalize();

                if(leashSegmentLength > (maxLeashLength/numberOfLeashSegments))
                {
                rbHolder = leashMidPointsList[i].GetComponent<Rigidbody>();
                // print("2-"+leashLinearVector*leashSegmentLength);
                rbHolder.AddForce(-forceConstant*leashLinearVector*leashSegmentLength);

                }
            }

        }
        
        leashLinearVector = transform.position - leashMidPointsList[0].transform.position;
        leashSegmentLength = leashLinearVector.magnitude;
        // print(leashSegmentLength);


        rbHolder = leashMidPointsList[0].GetComponent<Rigidbody>();

        //  print("Force Added:" + leashSegmentLength + " > "+maxLeashLength/numberOfLeashSegments);
        if(leashSegmentLength > (maxLeashLength/numberOfLeashSegments))
        {
            // print("Force Added");
        }

    }

    void LateUpdate()
    {
        for(int i=0; i<=(numberOfLeashSegments-1); i+=1)
        {
            
        }
    }


}
