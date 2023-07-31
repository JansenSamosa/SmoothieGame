using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidHolderController : MonoBehaviour
{   
    public Transform raycastStart;
    public string liquid = "milk";
    
    public float maxVolume = 10;
    public float volumeOfLiquid = 0;
    public float volumePourRate = 5;

    private LiquidHolderController otherHolder;
        
    private Animator animator;
    private bool pourLiquid = false;
    
    private PlayerDragController playerDragController;

    void Start() {
        animator = GetComponent<Animator>();
        playerDragController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDragController>();
    }

    void FixedUpdate() {
        FindOtherHolder();
    }

    void Update() {
        volumeOfLiquid = Mathf.Clamp(volumeOfLiquid, 0, maxVolume);
        
        if(volumeOfLiquid <= 0) {
            liquid = "_empty";
        }

        Pouring();
    }

    void FindOtherHolder() {
        bool foundOtherHolder = false;
        float raycastLength = .5f;

        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("LiquidContainer");

        if(Physics.Raycast(raycastStart.position, -Vector3.up, out hit, raycastLength, mask)) {
            Rigidbody otherRB = hit.collider.attachedRigidbody;
            otherHolder = otherRB ? otherRB.GetComponent<LiquidHolderController>() : null;

            if(CheckIfCanPour()) {
                animator.SetBool("isPouring", true);
                foundOtherHolder = true;

                Debug.DrawLine(raycastStart.position, transform.position - raycastLength * Vector3.up, Color.red);
                //Debug.Log(otherHolder.name);
            } else {
                Debug.DrawLine(raycastStart.position, transform.position - raycastLength * Vector3.up, Color.blue);
            }
        }

        if(!foundOtherHolder) {
            EndPouring();
        } 
    }
    bool CheckIfCanPour() {
        return otherHolder && 
                otherHolder != this && 
                volumeOfLiquid > 0 && 
                otherHolder.maxVolume > otherHolder.volumeOfLiquid &&
                (otherHolder.liquid == liquid || otherHolder.liquid == "_empty");
    }

    void StartPouring() {
        pourLiquid = true;
    }
    
    void Pouring() {
        if(pourLiquid) {
            float deltaVolume = volumePourRate * Time.deltaTime;
            
            deltaVolume = Mathf.Min(deltaVolume, volumeOfLiquid, otherHolder.maxVolume - otherHolder.volumeOfLiquid);

            otherHolder.liquid = liquid;
            otherHolder.volumeOfLiquid += deltaVolume;
            volumeOfLiquid -= deltaVolume;

            if(!CheckIfCanPour()) {
                EndPouring();
            }
        }
    }

    void EndPouring() {
        otherHolder = null;
        pourLiquid = false;
        animator.SetBool("isPouring", false);
    }
}
