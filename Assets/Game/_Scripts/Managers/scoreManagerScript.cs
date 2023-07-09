using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManagerScript : MonoBehaviour
{


    // public float highScore;
    public float currentScore = 0;
    public float pointsConstant = 10;

    private float maxPointsAtOnce = 250;

    void Awake()
    {
        // highScore = 0;
        currentScore = 0;
    }

    public void addPoints(float timeTaken)
    {
        float pointsHolder = pointsConstant*(Mathf.Floor(100*Mathf.Log((timeTaken/2)+1,10)));

        if(pointsHolder > maxPointsAtOnce)
        {
            pointsHolder = maxPointsAtOnce;
        }
        currentScore += pointsHolder;
    }

    

}
