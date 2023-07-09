using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class Vendor : MonoBehaviour {
        [Header("Cooking config")] //
        public float timeToCook;

    public int maxMeals;
    public Slider cookProgressionBar;
    public GameObject mealPrefab;
    public Transform[] mealSpawnPoints;
    public Slider[] mealSliders;
    public LayerMask clientLayer;
    public LayerMask obstacleLayer;

        private float cookTimer;
        private int currentMeals;

    private void Awake() {
        cookTimer = 0f;
        currentMeals = 0;
        foreach (var mealSlider in mealSliders) {
            mealSlider.maxValue = timeToCook;
            mealSlider.value = cookTimer;
        }
    }

    private void Update() {

        // rotationManager();

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
    }


        // I know this is repeating code but it'll do for now
        private void OnTriggerEnter(Collider other) {

            if (other.gameObject.layer == 7) { //CLIENT layer

                if(currentMeals > 0)
                {
                    GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
                    currentMeals--;
                    Destroy(meal);

                    if(other.TryGetComponent<customerScript>(out customerScript currentCustomer))
                    {
                        currentCustomer.recievedAHotdog();
                    }
                    
                }
                else
                {
                    print("No meals available to give");
                }
                // TODO: award points based on how long the client's been waiting
            }
            else if (other.gameObject.layer == 8) { //OBSTACLE layer
                if(currentMeals > 0)
                {
                    GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
                    currentMeals--;
                    Destroy(meal);
                } else {
                    cookTimer = 0;  // If there is a single hotdog in progress, that is removed when going through a bush.
                }
            }
        }


        private void scorePoints()
        {

        }
    }
}