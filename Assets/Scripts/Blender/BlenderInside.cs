using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderInside : MonoBehaviour
{
    public BlenderController controller;

    void OnTriggerEnter(Collider other) {
        CollectibleData newCollectible = other.GetComponent<CollectibleData>();
        if(newCollectible && !controller.ingredients.Contains(newCollectible)) {
            controller.ingredients.Add(newCollectible);
        }
    }

    void OnTriggerExit(Collider other) {
        CollectibleData oldCollectible = other.GetComponent<CollectibleData>();
        if(oldCollectible && controller.ingredients.Contains(oldCollectible)) {
            controller.ingredients.Remove(oldCollectible);
        }
    }
}
