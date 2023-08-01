using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{

    private int number = 5;

    void Start() {
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }
}
