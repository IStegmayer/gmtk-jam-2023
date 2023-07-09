using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerScript : MonoBehaviour
{
 
    private GameObject cameraObject;
    public float maxWaitTime;

    public bool wantsAHotdog = true;

    private float startTimeOfWantingHotdog;
    private float currentWaitTime;
    private float percentageOfTimeTaken;
    
    private Light debugLight;

    private Vector3 normalLocation;
    // private scoreManagerScript scoreScript;
    // private CameraSystemScriptRev1 CameraSysScript;
    // Start is called before the first frame update

    
    void Start()
    {
        debugLight = GetComponentInChildren<Light>();

        
        chooseClothes();
        
   
        updateCameraList();

        requestOrder();

        normalLocation = transform.position;
    
    }

    void Awake()
    {
        updateCameraList();
    }

    void Update()
    {
        if(wantsAHotdog){
            currentWaitTime = (Time.time - startTimeOfWantingHotdog);
        }

        if(wantsAHotdog)
        {
            transform.position = normalLocation + new Vector3(0,0.5f*Mathf.Sin(10*Time.time),0);
            // print("I want a hotdog");
        }else
        {
            transform.position = normalLocation;
        }

    }

    public void chooseClothes()
    {
        // GameObject clothing1 = transform.Find("Customer");
        // GameObject clothing2 = transform.Find("Customer1");
        // GameObject clothing3 = transform.Find("Customer2");

        for(int i = 0; i<3;i+=1)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);

            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(1).GetChild(Random.Range(0, 3)).gameObject.SetActive(true);

    }

    void updateCameraList()
    {
        cameraObject = GameObject.Find("CameraSystemRev1");
        if(cameraObject.TryGetComponent<CameraSystemScriptRev1>(out CameraSystemScriptRev1 CameraSysScript)){}
        CameraSysScript.ObjectsUpdate();
    }

    public void requestOrder()
    {
        wantsAHotdog = true;
        startTimeOfWantingHotdog = Time.time;
        debugLight.intensity = 10;
        updateCameraList();
    }

    public void recievedAHotdog()
    {
        wantsAHotdog = false;
        debugLight.intensity = 0;
        print("YUM YUM YUM");

        GameObject SMHolder = GameObject.Find("ScoreManager");
        if(SMHolder.TryGetComponent<scoreManagerScript>(out scoreManagerScript scoreScript)){
            scoreScript.addPoints(currentWaitTime);
        }
        

        currentWaitTime = 0;

        updateCameraList();
    }
}

