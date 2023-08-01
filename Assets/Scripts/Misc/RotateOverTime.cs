using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60;

    void Update() {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
