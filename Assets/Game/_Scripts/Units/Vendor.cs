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

    private Rigidbody rb;

    private float cookTimer;
    private int currentMeals;

    public Animator vendorAnimator;
    [Range(0, 1)] public float vendorDraggedFloat;

    private void Awake() {
        cookTimer = 0f;
        currentMeals = 0;
        foreach (var mealSlider in mealSliders) {
            mealSlider.maxValue = timeToCook;
            mealSlider.value = cookTimer;
        }

        timeOfLastInjury = Time.time;

        rb = GetComponentInParent<Rigidbody>();
    }

    private void Update() {
        if (GameManager.Instance.CurrentGameState != GameState.Started) return;

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

        vendorAnimator.SetFloat("dragged", vendorDraggedFloat);
    }


    // I know this is repeating code but it'll do for now
    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.layer) {
            case 7: //client

                if (currentMeals > 0) {
                    GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
                    currentMeals--;
                    Destroy(meal);

                    if (other.TryGetComponent<customerScript>(out customerScript currentCustomer)) {
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
        }
    }


    private void scorePoints() { }
}
}