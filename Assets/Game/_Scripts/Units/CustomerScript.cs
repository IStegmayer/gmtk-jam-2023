using System;
using UnityEngine;
using Random = System.Random;

namespace Game {
public class CustomerScript : MonoBehaviour {
    private GameObject cameraObject;
    public float maxWaitTime;

    public bool wantsAHotdog = true;

    private float startTimeOfWantingHotdog;
    private float currentWaitTime;
    private float percentageOfTimeTaken;

    public float MinTimeBetweenOrders = 10f;
    private float lastHotDogRecieved = -100;

    private Light debugLight;

    Random r = new Random();

    private Vector3 normalLocation;
    // private scoreManagerScript scoreScript;
    // private CameraSystemScriptRev1 CameraSysScript;
    // Start is called before the first frame update
    private void Awake() {
        debugLight = GetComponentInChildren<Light>();
    }

    void Start() {
        updateCameraList();

        chooseClothes();

        updateCameraList();

        // requestOrder();

        normalLocation = transform.position;
    }

    void Update() {
        if (GameManager.Instance.CurrentGameState != GameState.Started) return;

        if (wantsAHotdog) {
            currentWaitTime = (Time.time - startTimeOfWantingHotdog);
        }

        if (wantsAHotdog) {
            transform.position = normalLocation + new Vector3(0, Mathf.Abs(1f * Mathf.Sin(10 * Time.time)), 0);
            // print("I want a hotdog");
        }
        else {
            transform.position = normalLocation;
        }
    }

    public void chooseClothes() {
        // GameObject clothing1 = transform.Find("Customer");
        // GameObject clothing2 = transform.Find("Customer1");
        // GameObject clothing3 = transform.Find("Customer2");

        for (int i = 0; i < 3; i += 1) {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);

            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(1).GetChild(r.Next(0, 3)).gameObject.SetActive(true);
    }

    void updateCameraList() {
        cameraObject = GameObject.Find("CameraSystemRev1");
        if (cameraObject.TryGetComponent<CameraSystemScriptRev1>(out CameraSystemScriptRev1 CameraSysScript)) { }
        // CameraSysScript.ObjectsUpdate();
    }

    public void requestOrder(bool withSound = true) {
        if (MinTimeBetweenOrders < (Time.time - lastHotDogRecieved)) {
            wantsAHotdog = true;
            if (withSound) {
                int rInt = r.Next(1, 4);
                AudioSystem.Instance.PlayCustomerSound("NPC_CustomersCommand_0" + rInt, transform.position);
            }
            startTimeOfWantingHotdog = Time.time;
            debugLight.intensity = 15;
            updateCameraList();
        }
    }

    public void recievedAHotdog() {
        wantsAHotdog = false;
        debugLight.intensity = 0;

        int rInt = r.Next(1, 4);
        AudioSystem.Instance.PlayCustomerSound("NPC_SellFood_0" + rInt, transform.position);
        lastHotDogRecieved = Time.time;
        GameManager.Instance.AddPoints();

        currentWaitTime = 0;

        updateCameraList();
    }
}
}