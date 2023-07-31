using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{   
    public float cost = 2;
    
    public GameObject ingredientPrefab;
    public Transform spawnPoint;

    public float distanceToSpawnNew = 1;
    
    private Transform currentIngredient;
    
    private MoneyController moneyController;

    void Start() {
        moneyController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MoneyController>();
    }

    void Update() {
        if(currentIngredient == null) {
            spawnNewIngredient();
        } else {
            if(Vector3.Distance(spawnPoint.position, currentIngredient.position) >= distanceToSpawnNew) {
                currentIngredient = null;
            }
        }
    }

    void spawnNewIngredient() {
        currentIngredient = Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity).transform;

        moneyController.SubtractMoney(cost);
    }
}
