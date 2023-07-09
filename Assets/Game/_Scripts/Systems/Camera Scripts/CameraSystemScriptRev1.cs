using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemScriptRev1 : MonoBehaviour
{
    public GameObject dogGameObject;
    public GameObject vendorGameObject;
    private Vector3 dogLocation;
    private Camera mainCamera;

    public GameObject customerGameObject;   //this may need to be replaced with a tag or a layer.

    public List<GameObject> allObjectsToFocusOn = new List<GameObject>();

    private float highestPoint = 0;//z
    private float lowestPoint = 0;//z
    private float leftMostPoint = 0;//x
    private float rightMostPoint = 0; //x



    public float pitchAngle = -50f;
    public float minCameraDistance = 15f;

    public bool cameraSystem = true;
    
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        CameraUpdate();
        // allObjectsToFocusOn.Add(dogGameObject);
        ObjectsUpdate();

        dogGameObject = GameObject.Find("Dog");

        // ObjectsUpdate();
    }


    void Update()
    {
        ObjectsUpdate(); //(This can be called by the addition of a nerw customer for efficiency)
        CameraUpdate();
        
    }

    public void ObjectsUpdate()
    {
        allObjectsToFocusOn = new List<GameObject>();
        foreach(GameObject obHolder in GameObject.FindGameObjectsWithTag("Customer"))
        {
            allObjectsToFocusOn.Add(obHolder);
        }

        allObjectsToFocusOn.Add(dogGameObject);
        allObjectsToFocusOn.Add(vendorGameObject);

        highestPoint = 0;//z
        lowestPoint = 0;//z
        leftMostPoint = 0;//x
        rightMostPoint = 0; //x

    }

    void CameraUpdate()
    {
        Vector3 rotaionHolder = new Vector3(-1*pitchAngle,0,0);
        transform.eulerAngles  = rotaionHolder;
       

        foreach(GameObject focusObject in allObjectsToFocusOn)
        {
            
            if(focusObject.transform.position.z > highestPoint)
            {
                highestPoint = focusObject.transform.position.z;
            }

            if(focusObject.transform.position.z < lowestPoint)
            {
                lowestPoint = focusObject.transform.position.z;
            }

            if(focusObject.transform.position.x < leftMostPoint)
            {
                leftMostPoint = focusObject.transform.position.x;
            }

            if(focusObject.transform.position.x > rightMostPoint)
            {
                rightMostPoint = focusObject.transform.position.x;
            }
        }

        
        Vector3 focusPoint = new Vector3(((leftMostPoint+rightMostPoint)/2),0,((highestPoint+lowestPoint)/2));

        Vector3 offsetVector;

        float offsetLengthZ = ((highestPoint-lowestPoint)/2)/Mathf.Tan(degToRad(20));
        float offsetLengthX = ((rightMostPoint-leftMostPoint)/2)/Mathf.Tan(degToRad(20));

        float offsetLength = Mathf.Max(offsetLengthX,offsetLengthZ,minCameraDistance);

        Vector3 offsetDirection = new Vector3(0,0,1f);
        offsetDirection = Quaternion.AngleAxis(pitchAngle,  Vector3.left)*offsetDirection;

        offsetDirection.Normalize();
        transform.position = focusPoint + (-1 * offsetDirection * offsetLength);

    }


    float degToRad(float degrees)
    {
        return (degrees * 3.14f /180f);

    }
}