using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CustomerLineController : MonoBehaviour
{   
    [SerializeField] private Transform line;
    [SerializeField] private GameObject[] customerPrefabs;

    [SerializeField] private float spacingBetweenCustomers = 2; // spacing between each customer in the line
    private float xPosRandomizationOfCustomers = 0.5f;

    private List<CustomerController> customersInLine = new List<CustomerController>();

    [SerializeField] private float baseSpawnInterval = 10;
    [SerializeField] private float spawnIntervalVariance = 10;
    private float prevSpawnTime = 0;

    private float timeToNextSpawn = 0;

    [SerializeField] private AudioClip newCustomerSound;
    private AudioSource audio;

    //Rush hour variables and access to isRushHour bool from GameController
    [SerializeField] private float rushHourSpawnInterval = 7;
    [SerializeField] private float rushHourSpawnVariance = 7;
    [SerializeField] private GameController gameController;

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if(Time.time - prevSpawnTime >= timeToNextSpawn) {
            CreateRandomCustomer();

            if (gameController.isRushHour) {
                timeToNextSpawn = rushHourSpawnInterval + Random.Range(-rushHourSpawnVariance, rushHourSpawnVariance);
            } else {
                timeToNextSpawn = baseSpawnInterval + Random.Range(-spawnIntervalVariance, spawnIntervalVariance);
            }
            prevSpawnTime = Time.time;
        }

        // set the states of all the customers in line and 
        for(int i = 0; i < customersInLine.Count; i++) {
            string state = customersInLine[i].npcState;

            if(i == 0) { // if this customer is in front of the line
                if(state != "ordering" && state != "in line") {
                    RemoveCustomerFromLine(customersInLine[i]);
                } else {
                    customersInLine[i].npcState = "ordering";
                }
            } else {
                customersInLine[i].npcState = "in line";
            }
        }
    }
    
    void CreateRandomCustomer() {
        audio.PlayOneShot(newCustomerSound);

        int customerIndex = Random.Range(0, customerPrefabs.Length);
        CustomerController newCustomer = Instantiate(customerPrefabs[customerIndex], line.position, Quaternion.LookRotation(line.forward), line).GetComponent<CustomerController>();
        
        newCustomer.SetNPCState("in line");
        customersInLine.Add(newCustomer);

        float xPos = Random.Range(-xPosRandomizationOfCustomers, xPosRandomizationOfCustomers);
        newCustomer.transform.localPosition = new Vector3(xPos, 0, 0);

        PositionCustomers();
    }

    void RemoveCustomerFromLine(CustomerController customer) {
        customersInLine.Remove(customer);
        PositionCustomers();
    } 

    //Positions the customers so that they are in a line 
    void PositionCustomers() {
        for(int i = 0; i < customersInLine.Count; i++) {
            Transform customer = customersInLine[i].transform;

            customer.localPosition = new Vector3(customer.localPosition.x, 0, -i * spacingBetweenCustomers);
        }
    }

}

