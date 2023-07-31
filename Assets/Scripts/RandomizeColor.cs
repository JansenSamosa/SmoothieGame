using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    void Start() {
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }
}
