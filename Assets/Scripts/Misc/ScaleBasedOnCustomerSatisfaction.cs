using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBasedOnCustomerSatisfaction : MonoBehaviour
{
    [SerializeField] private CustomerSatisfactionController CSController;

    [SerializeField] private float scaleWhenFocused = 1.25f;
    [SerializeField] private float scaleWhenNotFocused = .75f;

    [SerializeField] private float minCSToFocus = 0;
    [SerializeField] private float maxCSToFocus = .25f;

    void Update() {
        float cs = CSController.slider.value;
        Vector3 newLocalScale = new Vector3(scaleWhenNotFocused, scaleWhenNotFocused, 1);
        
        if(cs >= minCSToFocus && cs < maxCSToFocus) {
            newLocalScale.x = scaleWhenFocused;
            newLocalScale.y = scaleWhenFocused;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, newLocalScale, 5*Time.deltaTime);
    }
}
