using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManagerScript : MonoBehaviour
{

    public int howManyCustomersToStartWith = 2;
    public List<GameObject> listOfCustomers = new List<GameObject>();

    public float waitForNewCustomerMin = 5;
    public float waitForNewCustomerMax = 10;

    private float lastOrderMadeTime;

    private float timeUntilNextOrder;

    void Start()
    {
        foreach(GameObject obHolder in GameObject.FindGameObjectsWithTag("Customer"))
        {
            if(obHolder.TryGetComponent<CustomerScript>(out CustomerScript currentCustomer))
            {
                currentCustomer.wantsAHotdog = false;
                listOfCustomers.Add(obHolder);
            }

            lastOrderMadeTime = Time.time;

        }


        for(int i=0; i < howManyCustomersToStartWith; i += 1)
        {
            CustomerScript CSHolder = listOfCustomers[Random.Range(0, listOfCustomers.Count)].GetComponent<CustomerScript>();
            
            if(CSHolder.wantsAHotdog)
            {
                i -= 1;
            } else
            {
                CSHolder.requestOrder();
            }
        }

        chooseNextOrderTimer();

    }

    void Update()
    {
        float holder = Time.time - lastOrderMadeTime;
        // print(timeUntilNextOrder + " < " + holder);
        if(timeUntilNextOrder < Time.time - lastOrderMadeTime)
        {
            print("ORDER REQUESTD");
            assignRandomOrder();
        }
    }

    void chooseNextOrderTimer()
    {
        timeUntilNextOrder = Random.Range(waitForNewCustomerMin,waitForNewCustomerMax);
    }

    void assignRandomOrder()
    {
        print("ORDER RECIEVED");
    CustomerScript CSHolder = listOfCustomers[Random.Range(0, listOfCustomers.Count)].GetComponent<CustomerScript>();

    CSHolder.requestOrder();

    lastOrderMadeTime = Time.time;

        
        chooseNextOrderTimer();
    }

    // Update is called once per frame
  
}
