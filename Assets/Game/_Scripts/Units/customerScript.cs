using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Game {
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
    
    Random r = new Random();

    // Start is called before the first frame update

    
    void Start()
    {
        debugLight = GetComponentInChildren<Light>();
        updateCameraList();

        cameraObject = GameObject.Find("CameraSystemRev1");


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
        int rInt = r.Next(1, 4);
        AudioSystem.Instance.PlayCustomerSound("NPC_CustomersCommand_0" + rInt, transform.position);
        wantsAHotdog = true;
        startTimeOfWantingHotdog = Time.time;
        debugLight.intensity = 1;

    }

    public void recievedAHotdog()
    {
        wantsAHotdog = false;
        debugLight.intensity = 0;

        int rInt = r.Next(1, 4);
        AudioSystem.Instance.PlayCustomerSound("NPC_SellFood_0" + rInt, transform.position);
        print("YUM YUM YUM");
    }
}

}
