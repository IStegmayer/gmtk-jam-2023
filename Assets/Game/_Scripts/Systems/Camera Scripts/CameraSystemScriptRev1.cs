using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game;

public class CameraSystemScriptRev1 : MonoBehaviour
{
    private GameObject dogGameObject;
    private Vector3 dogLocation;
    private Camera mainCamera;

    private List<GameObject> objectsToFocusOn = new List<GameObject>();

    // private CustomerScript ab;
    
    private float maxX; 
    private float minX;
    private float maxZ;
    private float minZ;

    private float distanceX;
    private float distanceZ;

    private float angle2H;
    private float angle2W;

    private Vector3 cameraLookUnitVector;

    private Vector3 pointToLookAt;
    private Vector3 rotaionHolder;
    private float focalLength;


    [Header("Design Variables")] //
    public float pitchAngle = 50f;
    public float minimumCameraDistance = 0f;
    public float zoomBuffer = 1.2f;
    public bool cameraFollowsDog = true;
    public bool cameraFollowsVendor = true;
    public bool cameraFollowsCustomers = true;
    
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        UpdateCameraFocusPoints();
        pointToLookAt = GameObject.Find("Dog").transform.position;   //Need to update this 
        angle2H = mainCamera.fieldOfView;
        angle2W = mainCamera.fieldOfView * mainCamera.aspect;

        cameraLookUnitVector = Vector3.forward;

        cameraLookUnitVector = Quaternion.AngleAxis(pitchAngle,Vector3.right) * cameraLookUnitVector;

        transform.forward = cameraLookUnitVector;
    }


    void Update()
    {

        // print(mainCamera.aspect);
        UpdateCameraFocusPoints(); //   This function can be called from a cutomer after making or recieving an order to improve to improve optimsation.
        limitsUpdate();
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
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Customer"))
            {
                if(item.TryGetComponent<CustomerScript>(out CustomerScript custScript))
                {
                    print("ABC");
                    if(custScript.wantsAHotdog){
                        objectsToFocusOn.Add(item);
                    }
                }
            }
        }
    }

    float degToRad(float degrees)
    {
        return (degrees * 3.14f /180f);
    }

    void limitsUpdate()
    {

        Vector3 dogSpotHolder = GameObject.Find("Dog").transform.position;
        minX = dogSpotHolder.x;
        maxX = dogSpotHolder.x;
        minZ = dogSpotHolder.z;
        maxZ = dogSpotHolder.z;



        foreach(GameObject item in objectsToFocusOn){

            // Debug.DrawRay(transform.position,transform.position)

            if(item.transform.position.x < minX)
            {
                minX = item.transform.position.x;
            }

            if(item.transform.position.x > maxX)
            {
                maxX = item.transform.position.x;
            }

            if(item.transform.position.z < minZ)
            {
                minZ = item.transform.position.z;
            }

            if(item.transform.position.x > maxZ)
            {
                maxZ = item.transform.position.z;
            }
        }

        distanceX = Mathf.Abs(maxZ - minZ);

        distanceZ = Mathf.Abs(maxX - minX);
    }

    void cameraPositionUpdate()
    {
        pointToLookAt = new Vector3(minX+maxX,2f,minZ+maxZ);
        pointToLookAt /= 2;
        distanceUpdate();    
        transform.position = pointToLookAt + (1.1f*focalLength*(-1*cameraLookUnitVector));
    }

    void distanceUpdate()
    {
        float distW = zoomBuffer*distanceX / (2*Mathf.Tan(degToRad(angle2W/2)));
        float distH = zoomBuffer*distanceZ / (2*Mathf.Tan(degToRad(angle2H/2)));
        focalLength = Mathf.Max(minimumCameraDistance,distW,distH);
    }
}