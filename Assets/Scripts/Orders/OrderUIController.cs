using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderUIController : MonoBehaviour
{
    private Order order;

    public TMP_Text drinkName;
    public Slider timer;
    
    private bool initialized = false;

    void Update() {
        if(initialized) {
            timer.value = order.timeRemaining/order.timeMax;
        }
    }

    public void InitializeUI(Order orderToInitialize) {
        order = orderToInitialize;

        drinkName.text = order.drinks[0].drinkName;
        initialized = true;
    }

    public void UpdateUI(Order updatedOrder) {
        order = updatedOrder;
    }
}
