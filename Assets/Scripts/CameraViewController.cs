using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraViewController : MonoBehaviour
{
    //public string currentView = "cashier"; 
    private Animator cameraAC;

    void Start() {
        cameraAC = GetComponent<Animator>();
    }


    public void SwitchToCashierView() {
        cameraAC.CrossFade("SmoothieCashierView", 0.25f);
    }

    public void SwitchToMakerView() {
        cameraAC.CrossFade("SmoothieMakerView", 0.25f);
    }

    public void SwitchToPickUpView() {
        cameraAC.CrossFade("SmoothiePickUpView", 0.25f);
    }
}
