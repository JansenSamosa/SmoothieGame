/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragController : MonoBehaviour
{   
    public float dragSpeed = 5;
    public Transform currentDraggable;
    private Rigidbody currentDraggableRB;

    private Vector3 currentDragVelocity;  
    
    private float prevTime;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Update() {      
        if(Input.GetMouseButtonDown(0)) {
            // pick up a draggable if there is one
            PickUp();
        }
        if(Input.GetMouseButton(0) && currentDraggable) {
            MoveDraggable();
        }

        if(Input.GetMouseButtonUp(0) && currentDraggable) {
            // let go of draggable
            LetGo();
        }
    }

    void PickUp() {
        Vector3 mousePos = Input.mousePosition + new Vector3(0,0,1);

        Ray camRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Default");

        if(Physics.Raycast(camRay, out hit, 100, mask)) {
            if(hit.collider.tag == "Draggable") {
                currentDraggable = hit.collider.attachedRigidbody.transform;
                currentDraggableRB = hit.collider.attachedRigidbody;
                currentDraggableRB.isKinematic = true;
            }
        }
    }

    void MoveDraggable() {
        Vector3 mousePos = Input.mousePosition + new Vector3(0,0,1);
        Vector3 newPos = currentDraggable.position;

        Ray camRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("DragPlane");

        if(Physics.Raycast(camRay, out hit, 100, mask)) {
            newPos = hit.point;
        } else {
            LetGo();
        }
        
        Vector3 prevDragPos = currentDraggable.position;

        currentDraggable.position = Vector3.Lerp(currentDraggable.position, newPos, dragSpeed * Time.deltaTime);
        
        //Calculate the velocity of the object which is being dragged
        currentDragVelocity = (currentDraggable.position - prevDragPos)/Time.deltaTime;
    }

    void LetGo() {
        Debug.Log(currentDragVelocity);
        currentDraggableRB.isKinematic = false;
        currentDraggableRB.velocity = currentDragVelocity;

        currentDraggable = null;
        currentDraggableRB = null;
    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragController : MonoBehaviour
{   
    public float dragSpeed = 5;
    public Transform currentDraggable;
    private Rigidbody currentDraggableRB;

    private Vector3 currentDragVelocity;  
    
    private float prevTime;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Update() {      
        if(Input.GetMouseButtonDown(0)) {
            // pick up a draggable if there is one
            PickUp();
        }
        else if(Input.GetMouseButton(0) && currentDraggable) {
            MoveDraggable();
        }
        else {
            if(currentDraggable) {
                LetGo();
            }
        }
    }

    void PickUp() {
        Vector3 mousePos = Input.mousePosition + new Vector3(0,0,1);

        Ray camRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        string[] layers = {"Default", "LiquidContainer"};
        LayerMask mask = LayerMask.GetMask(layers);

        if(Physics.Raycast(camRay, out hit, 100, mask)) {
            Rigidbody otherRB = hit.collider.attachedRigidbody;
            if(otherRB && otherRB.tag == "Draggable") {
                currentDraggable = otherRB.transform;
                currentDraggableRB = otherRB;
                currentDraggableRB.useGravity = false;
                currentDraggableRB.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            }
        }
        Debug.DrawRay(camRay.origin, camRay.direction, Color.green);
    }

    void MoveDraggable() {
        Vector3 mousePos = Input.mousePosition + new Vector3(0,0,1);
        Vector3 newPos = currentDraggable.position;

        Ray camRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("DragPlane");

        Vector3 prevDragPos = currentDraggable.position;

        if(Physics.Raycast(camRay, out hit, 100, mask)) {
            newPos = hit.point;
        } else {
            LetGo();
        }
        
        //Calculate and set the velocity of the object which is being dragged 
        currentDragVelocity = (newPos - prevDragPos) * dragSpeed;
        currentDraggableRB.velocity = currentDragVelocity;
    }

    void LetGo() {
        currentDraggableRB.useGravity = true;
        //currentDraggableRB.velocity = new Vector3(currentDraggableRB.velocity.x,currentDraggableRB.velocity.y,0);
        //currentDraggableRB.constraints = currentDraggableRB.constraints | RigidbodyConstraints.FreezePositionZ;

        currentDraggable = null;
        currentDraggableRB = null;
    }
}
