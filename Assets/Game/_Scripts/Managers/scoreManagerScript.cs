using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManagerScript : MonoBehaviour
{


    // public float highScore;
    public float currentScore = 0;
    public float pointsConstant = 10;
    public TextMeshProUGUI scoreText;

    private float maxPointsAtOnce = 250;

    void Awake()
    {
        // highScore = 0;
        currentScore = 0;
    }

    public void addPoints(float timeTaken)
    {
        // float pointsHolder = pointsConstant*(Mathf.Floor(20*Mathf.Log((timeTaken/2)+1,10)));
        // print(pointsHolder);
        // if(pointsHolder > maxPointsAtOnce)
        // {
        //     pointsHolder = maxPointsAtOnce;
        // }
        currentScore ++;
        scoreText.text = currentScore + " Dogs Served";
    }

    

}
