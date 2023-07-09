using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vendorRorationManager : MonoBehaviour
{

    public float rotationSpeed = 1f;
    public GameObject dogGameObject ;
    // Start is called before the first frame update
    void Start()
    {
        dogGameObject = GameObject.Find("Dog");
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 vectorTowardsDog = dogGameObject.transform.position - transform.position;
        // vectorTowardsDog.Normalize();

        // print(vectorTowardsDog);

        // Debug.DrawRay(transform.position, transform.position+vectorTowardsDog, Color.red);

        // vectorTowardsDog.y += 2;
        Vector3 newDirection = Vector3.RotateTowards(transform.up, Vector3.up, rotationSpeed * Time.deltaTime,0.0f);

        print(transform.up + "-" + newDirection);
        
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.up =Vector3.up;

    }
}
