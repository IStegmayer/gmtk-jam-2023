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
        if (other.gameObject.layer == 7) {
            GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
            currentMeals--;
            Destroy(meal);
            // TODO: award points based on how long the client's been waiting
        }
        else if (other.gameObject.layer == 8) {
            GameObject meal = mealSpawnPoints[currentMeals - 1].GetChild(0).gameObject;
            currentMeals--;
            Destroy(meal);
        }
    }
}
}