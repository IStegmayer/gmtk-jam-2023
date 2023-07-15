using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemScriptRev1 : MonoBehaviour
{
    private GameObject dogGameObject;
    private Vector3 dogLocation;
    private Camera mainCamera;

    private List<GameObject> objectsToFocusOn = new List<GameObject>();
    
    private float maxX; 
    private float minX;
    private float maxZ;
    private float minZ;

    private float distanceX;
    private float distanceZ;

    private float angle2;

    private Vector3 pointToLookAt;
    private Vector3 rotaionHolder;
    private float focalLength;


    [Header("Design Variables")] //
    public float pitchAngle = 50f;
    public float minimumCameraDistance = 30f;
    public bool cameraFollowsDog = true;
    public bool cameraFollowsVendor = true;
    public bool cameraFollowsCustomers = true;
    // public List<String> namesOfObjectsToFollow = new List<String>();
    

    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        UpdateCameraFocusPoints();
        pointToLookAt = GameObject.Find("Dog").transform.position;
        angle2 = mainCamera.fieldOfView * mainCamera.aspect;
        rotaionHolder = new Vector3(pitchAngle,0,0);
        transform.eulerAngles = rotaionHolder;
    }


    void Update()
    {
        UpdateCameraFocusPoints(); //   This function can be called from a cutomer after making or recieving an order to improve to improve optimsation.
        cameraPositionUpdate();
    }

    void UpdateCameraFocusPoints()
    {
        objectsToFocusOn = new List<GameObject>();

        if(cameraFollowsDog)
        {
            objectsToFocusOn.Add(GameObject.Find("Dog"));
        }

        if(cameraFollowsVendor)
        {
            objectsToFocusOn.Add(GameObject.Find("Vendor")); // Vendor Name needs to be checked.
        }

        if(cameraFollowsCustomers)
        {
            foreach (GameObject item in FindGameObjectsWithTag("Customer"))
            {
                objectsToFocusOn.Add(item);
            }
        }
    }

    float degToRad(float degrees)
    {
        return (degrees * 3.14f /180f);
    }

    void limitsUpdate()
    {
        minX = null;
        maxX = null;
        minZ = null;
        maxZ = null;

        foreach(GameObject item in objectsToFocusOn){
            if(objectsToFocusOn.transform.position.x < minX)
            {
                minX = objectsToFocusOn.transform.position.x;
            }

            if(objectsToFocusOn.transform.position.x > maxX)
            {
                maxX = objectsToFocusOn.transform.position.x;
            }

            if(objectsToFocusOn.transform.position.z < minZ)
            {
                minZ = objectsToFocusOn.transform.position.z;
            }

            if(objectsToFocusOn.transform.position.x > maxZ)
            {
                maxZ = objectsToFocusOn.transform.position.z;
            }
        }
    }

    void cameraPositionUpdate()
    {

        pointToLookAt = new Vector3(minX-maxX,0,minZ-maxZ);
        pointToLookAt /= 2;

        distanceUpdate();
        
        transform.position = pointToLookAt + (focalLength*(-transform.eulerAngles));

    }

    void distanceUpdate()
    {

        float distW = distanceX / (2 * Mathf.tan(degToRad(angle2)/2));

        float distH = distanceZ * Mathf.sin(degToRad(pitchAngle)) / (2 * Mathf.tan(degToRad(angle2)/2));

        focalLength = Mathf.Max(minimumCameraDistance,distW,distH);

    }

}