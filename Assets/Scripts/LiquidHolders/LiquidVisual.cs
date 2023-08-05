using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidVisual : MonoBehaviour
{
    public LiquidHolderController controller;

    public MeshRenderer[] liquidMeshes;

    private LiquidMaterialsDict materials; 

    void Start() {
        materials = GameObject.FindGameObjectWithTag("GameController").GetComponent<Dictionaries>().liquidMaterials;
    }

    void Update() {
        try {
            UpdateVisual();
        } catch {
            Debug.Log("crash");
        }
    }

    public void UpdateVisual() {
        Vector3 newScale = transform.localScale;
        newScale.y = Mathf.Clamp(controller.volumeOfLiquid/controller.maxVolume, 0.05f, 1);  
        transform.localScale = newScale;

        for(int i = 0; i < liquidMeshes.Length; i++) {
            //if(liquidMeshes[i].material != materials[controller.liquid]) {
                liquidMeshes[i].material = materials[controller.liquid];
            //}
        }
    }
}
