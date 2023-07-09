using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerScript : MonoBehaviour
{
    public GameObject cameraObject;


    private float startTimeOfWantingHotdog;

    public float maxWaitTime;

    private float currentWaitTime;

    private float percentageOfTimeTaken;

    public float orderCooldown = 20;

    private bool wantsAHotdog = true;

    private Light debugLight;

    // Start is called before the first frame update

    
    void Start()
    {
        debugLight = GetComponentInChildren<Light>();
        updateCameraList();


        requestOrder();
    
    }

    void Awake()
    {
        updateCameraList();
    }

    // Update is called once per frame
    void Update()
    {
        if(wantsAHotdog){
            currentWaitTime = (Time.time - startTimeOfWantingHotdog);

        }
    }

    void updateCameraList()
    {

        cameraObject.GetComponent<CameraSystemScriptRev1>().ObjectsUpdate();

    }

    public void requestOrder()
    {

        wantsAHotdog = true;
        startTimeOfWantingHotdog = Time.time;
        debugLight.intensity = 1;

    }

    public void recievedAHotdog()
    {
        wantsAHotdog = false;
        debugLight.intensity = 0;
        print("YUM YUM YUM");
    }
}
