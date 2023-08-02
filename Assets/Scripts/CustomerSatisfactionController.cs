using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSatisfactionController : MonoBehaviour
{
    public float customerSatisfaction = 0.5f;
    
    [SerializeField] private float CSIncreaseRate = 0.05f;
    [SerializeField] private float CSDecreaseRate = 0.05f;
    [SerializeField] private Slider slider;

    void Update()
    {
        slider.value = customerSatisfaction;
    }

    //This function is called 1 time: when an order is completed
    public void increaseCustomerSatisfaction() {
        customerSatisfaction += CSIncreaseRate;
    }

    //This function is called 3 times: when an order is incomplete, when an order is refused, and when an order is never taken
    public void decreaseCustomerSatisfaction() {
        customerSatisfaction -= CSDecreaseRate;
    }


}
