using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemScriptRev1 : MonoBehaviour
{
    public GameObject dogGameObject;
    private Vector3 dogLocation;
    private Camera mainCamera;

    public float pitchAngle = -50f;
    public float cameraDistance = 30f;

    public bool cameraSystem = true;

    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        CameraUpdate();
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

        Vector3 offsetVector = new Vector3(0,Mathf.Sin(degToRad(pitchAngle))*-1*cameraDistance,Mathf.Cos(degToRad(pitchAngle))*-1*cameraDistance);
        transform.position = dogGameObject.transform.position + offsetVector;

    }

    float degToRad(float degrees)
    {
        return (degrees * 3.14f /180f);

    }
}