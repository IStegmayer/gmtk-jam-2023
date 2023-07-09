using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeashScriptRev1 : MonoBehaviour
{
    public float forceConstant;

    public Material leashMaterial;

    public GameObject vendorAttachPoint;

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
        vendorRb = vendorAttachPoint.GetComponent<Rigidbody>();
        vendorAttachPoint.AddComponent<LineRenderer>();

        Vector3 leashLinearVector = collar.transform.position - vendorAttachPoint.transform.position;

        leashMidPointsList.Add(vendorAttachPoint);

        for(int i=0; i<=(numberOfLeashSegments-2); i+=1)
        {
            Vector3 spawnLocation = (i+1)*leashLinearVector/numberOfLeashSegments;

            spawnLocation -= leashLinearVector;
            spawnLocation += collar.transform.position;

            GameObject midpoint = Instantiate(leashMidPoint);

            midpoint.AddComponent<LineRenderer>();
            LineRenderer lrHolder = midpoint.GetComponent<LineRenderer>();
            lrHolder.SetWidth(0.1f,0.1f);
            lrHolder.material = leashMaterial;
            midpoint.transform.position = spawnLocation;
            leashMidPointsList.Add(midpoint);
        }

        leashMidPointsList.Add(collar);

    }

    void FixedUpdate()
    {

        Vector3 leashLinearVector;
        Rigidbody rbHolder;

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
                rbHolder.AddForce(-forceConstant*leashLinearVector*leashSegmentLength);

                }
            }

        }
        
        leashLinearVector = transform.position - leashMidPointsList[0].transform.position;
        leashSegmentLength = leashLinearVector.magnitude;


        rbHolder = leashMidPointsList[0].GetComponent<Rigidbody>();

    

    }

    void LateUpdate()
    {
        GameObject goHolder;
        LineRenderer lrHolder;

        for(int i=0; i<leashMidPointsList.Count-1; i+=1)
        {
            goHolder = leashMidPointsList[i];
            lrHolder = goHolder.GetComponent<LineRenderer>();
            lrHolder.SetPosition(0,goHolder.transform.position);
            lrHolder.SetPosition(1,leashMidPointsList[i+1].transform.position);

        }
        // goHolder = leashMidPointsList[leashMidPointsList.Count];
        // goHolder = leashMidPointsList[0];


        lrHolder = vendorAttachPoint.GetComponent<LineRenderer>();
        lrHolder.SetWidth(0.1f,0.1f);
        lrHolder.material = leashMaterial;
        lrHolder.SetPosition(0,vendorAttachPoint.transform.position);
        lrHolder.SetPosition(1,leashMidPointsList[1].transform.position);
        // leashMidPointsList[leashMidPointsList.Count]

    }
}
