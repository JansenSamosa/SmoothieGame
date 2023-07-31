using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCompleter : MonoBehaviour
{
    public List<LiquidHolderController> drinks; 

    private OrdersController controller;
    private AudioSource audio;

    void Start() {
        audio = GetComponent<AudioSource>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<OrdersController>();
    }

    public void FindCompletedOrder() {
        bool orderFound = false;

        // Check every order to check if there is one that is fulfilled
        for(int i = 0; i < controller.activeOrders.Count && !orderFound; i++) {
            Order orderToFulfill = controller.activeOrders[i];

            List<DrinkOrderInfo> drinksToFulfill = new List<DrinkOrderInfo>(orderToFulfill.drinks);
            List<LiquidHolderController> drinksAvailable = new List<LiquidHolderController>(drinks);

            List<LiquidHolderController> drinksUsedToFulfillOrder = new List<LiquidHolderController>();

            bool isCompleted = drinksAvailable.Count == 0 ? false : true;

            //Check each drink that needs to be fulfilled and determine if it is fulfilled
            for(int j = 0; j < drinksToFulfill.Count && isCompleted; j++) {
                bool foundDrink = false;

                for(int k = 0; k < drinksAvailable.Count && !foundDrink; k++) {
                    // Check the name of the drink and the volume of it to check if it matches.
                    if(drinksAvailable[k].liquid == drinksToFulfill[j].drinkName && Mathf.Round(drinksAvailable[k].volumeOfLiquid) >= drinksToFulfill[j].volumeOfDrink) {
                        drinksUsedToFulfillOrder.Add(drinksAvailable[k]);
                        drinksAvailable.RemoveAt(k);
                        foundDrink = true;
                    } 
                }

                isCompleted = foundDrink;
            }   

            if(isCompleted) {
                audio.Play();
                Debug.Log("Completed order: " + orderToFulfill.drinks[0].drinkName);
                controller.CompleteOrder(orderToFulfill);

                //Destroy drinks used to fulfill order
                for(int j = 0; j < drinksUsedToFulfillOrder.Count; j++) {
                    GameObject drinkToDestroy = drinksUsedToFulfillOrder[j].gameObject;
                    drinks.Remove(drinksUsedToFulfillOrder[j]);
                    Destroy(drinkToDestroy);
                }

                orderFound = true;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        LiquidHolderController newDrink = other.attachedRigidbody.GetComponent<LiquidHolderController>();

        if(newDrink && !drinks.Contains(newDrink)) {
            drinks.Add(newDrink);
        }
    }

    void OnTriggerExit(Collider other) {
        LiquidHolderController oldDrink = other.attachedRigidbody.GetComponent<LiquidHolderController>();

        if(oldDrink && drinks.Contains(oldDrink)) {
            drinks.Remove(oldDrink);
        }
    }
}
