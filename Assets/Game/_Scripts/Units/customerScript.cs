using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerScript : MonoBehaviour
{
    public GameObject cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        updateCameraList();
    }

    void Awake()
    {
        updateCameraList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateCameraList(){

        cameraObject.GetComponent<CameraSystemScriptRev1>().ObjectsUpdate();

    }
}
