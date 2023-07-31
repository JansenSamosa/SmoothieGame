using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLiquid : MonoBehaviour
{
    private LiquidHolderController controller;

    void Start() {
        controller = GetComponent<LiquidHolderController>();
    }

    void Update() {
        controller.volumeOfLiquid = controller.maxVolume;
    }
}
