using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainLiquid : MonoBehaviour
{
    public float volumeDrainRate;

    private LiquidHolderController controller;
    
    void Start() {
        controller = GetComponent<LiquidHolderController>();
    }

    void Update() {
        controller.volumeOfLiquid -= volumeDrainRate * Time.deltaTime;
    }
}
