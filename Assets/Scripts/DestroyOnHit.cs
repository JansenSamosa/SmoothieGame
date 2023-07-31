using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
