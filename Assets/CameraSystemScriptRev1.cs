using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemScriptRev1 : MonoBehaviour
{
    public GameObject dogGameObject;
    private Vector3 dogLocation;
    private Camera mainCamera;

    public GameObject customerGameObject;   //this may need to be replaced with a tag or a layer.

    public List<GameObject> allObjectsToFocusOn = new List<GameObject>();



    public float pitchAngle = -50f;
    public float cameraDistance = 15f;

    public bool cameraSystem = true;
    
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        CameraUpdate();
        allObjectsToFocusOn.Add(dogGameObject);
    }


    void Update()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        Vector3 rotaionHolder = new Vector3(-1*pitchAngle,0,0);
        transform.eulerAngles  = rotaionHolder;

        dogGameObject = GameObject.Find("Dog");

        float highestPoint;
        float lowestPoint;
        float leftMostPoint;
        float rightMostPoint; 
        
        // ForEach(Gameobject focusPoint in allObjectsToFocusOn)

        Vector3 offsetVector = new Vector3(0,Mathf.Sin(degToRad(pitchAngle))*-1*cameraDistance,Mathf.Cos(degToRad(pitchAngle))*-1*cameraDistance);

        Vector3 focusPoint = 

        transform.position = dogGameObject.transform.position + offsetVector;

    }

    float degToRad(float degrees)
    {
        return (degrees * 3.14f /180f);

    }
}