using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerSatisfactionController : MonoBehaviour
{
    public float customerSatisfaction = 0.5f;
    public float currentTipMultiplier = 1;

    [SerializeField] private float[] tipMultipliersByStep = {0, 0.5f, 1f, 1.5f, 2f};
    
    [SerializeField] private float CSIncreaseRate = 0.05f;
    [SerializeField] private float CSDecreaseRate = 0.05f;

    public Slider slider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private TMP_Text tipMultiplierText;

    [SerializeField] private Gradient sliderColor;

    void Update()
    {   
        customerSatisfaction = Mathf.Clamp(customerSatisfaction, 0, 1);

        slider.value = Mathf.Lerp(slider.value, customerSatisfaction + .02f , 5 * Time.deltaTime);
        sliderFill.color = sliderColor.Evaluate(slider.value);

        int currentTipMultiplierIndex = (int)(customerSatisfaction / .25f);
        currentTipMultiplier = tipMultipliersByStep[currentTipMultiplierIndex];

        tipMultiplierText.text = "x" + currentTipMultiplier.ToString("F1") + " Tip";
        tipMultiplierText.color = sliderColor.Evaluate(slider.value);
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
