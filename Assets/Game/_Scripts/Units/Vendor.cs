using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
public class Vendor : MonoBehaviour {
    [Header("Cooking config")] //
    public float timeToCook;

    public int maxMeals;
    public GameObject mealPrefab;
    public Transform[] mealSpawnPoints;
    public Slider[] mealSliders;
    public LayerMask clientLayer;
    public LayerMask obstacleLayer;
    public float obstacleCooldown = 3f;
    public float timeOfLastInjury;
    public float uprightTorqueForce = 1;



    private Rigidbody rb;

    private float cookTimer;
    public int currentMeals;

    public GameObject dogGameObject;

    // public Animator vendorAnimator;
    [Range(0, 1)] public float vendorDraggedFloat;

    private void Awake() {
        cookTimer = 0f;
        currentMeals = 4;
        foreach (var mealSlider in mealSliders) {
            mealSlider.maxValue = timeToCook;
            mealSlider.value = cookTimer;
        }

        timeOfLastInjury = Time.time;

        rb = GetComponentInParent<Rigidbody>();

    }

    private void Update() {
        // if (GameManager.Instance.CurrentGameState != GameState.Started) return;

        if (cookTimer >= timeToCook) {
            currentMeals++;
            Instantiate(mealPrefab, mealSpawnPoints[currentMeals - 1].position, Quaternion.identity,
                mealSpawnPoints[currentMeals - 1]);
            cookTimer = 0f;
        }
        else if (currentMeals < maxMeals) {
            mealSliders[currentMeals].value += Time.deltaTime;
            cookTimer += Time.deltaTime;
        }

        rotationManager();

        // vendorAnimator.SetFloat("dragged", vendorDraggedFloat);


        
    }

    private void FixedUpdate()
    {
        rotationManager();
    }


    // I know this is repeating code but it'll do for now
    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.layer) {
            case 7: //client

                if (currentMeals > 0) {
                    GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
                    currentMeals--;
                    Destroy(meal);

                    if (other.TryGetComponent<CustomerScript>(out CustomerScript currentCustomer)) {
                        currentCustomer.recievedAHotdog();
                    }
                }
                else {
                    print("No meals available to give");
                }

                break;

            case 8: //obstacle

                if (obstacleCooldown <
                    (Time.time -
                     timeOfLastInjury)) //Quick time buffer so that injuried dont happen repeadely if a bunch of bushes are bunched together
                {
                    timeOfLastInjury = Time.time;

                    print("Just ran into " + other);

                    if (currentMeals > 0) {
                        GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
                        currentMeals--;
                        Destroy(meal);
                    }
                    else {
                        cookTimer =
                            0; // If there is a single hotdog in progress, that is removed when going through a bush.
                    }
                }
                    break;

                case 9: //obstacle

                if (obstacleCooldown <
                    (Time.time -
                     timeOfLastInjury)) //Quick time buffer so that injuried dont happen repeadely if a bunch of bushes are bunched together
                {
                    timeOfLastInjury = Time.time;

                    print("Just ran into " + other);

                    if (currentMeals > 0) {
                        GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
                        currentMeals--;
                        Destroy(meal);
                    }
                    else {
                        cookTimer =
                            0; // If there is a single hotdog in progress, that is removed when going through a bush.
                    }
                }

                break;
        }
    }

    private void rotationManager()
    {

        Vector3 targetVector = new Vector3(1,1,1);
        
        Vector3 focusVector = dogGameObject.transform.position - transform.position;

        Quaternion rotationHolder = Quaternion.LookRotation(focusVector, Vector3.up);

        transform.rotation = rotationHolder;

        if(focusVector.magnitude < 2)
        {
            // print("Slowdown");
            rb.velocity *= 0.5f;// * Time.fixedTime;
        }
    }

    private void scorePoints() { }
}
}