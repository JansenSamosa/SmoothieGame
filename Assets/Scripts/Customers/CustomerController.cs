using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    // states = "in line" or "ordering" or "picking up" or "leaving"
    public string npcState = "in line"; 

    public DialogueSystem dialogue;

    // Order the customer wants to place
    private Order customerOrder; 

    private OrdersController ordersController;

    [SerializeField] private Vector3 pickUpZoneLeftCorner;
    [SerializeField] private Vector3 pickUpZoneRightCorner;
    private Vector3 pickupPosition;

    void Awake() {
        ordersController = GameObject.FindGameObjectWithTag("GameController").GetComponent<OrdersController>();
    }

    void Start() {  
        // create random order and assign it to this customer
        customerOrder = ordersController.CreateRandomOrder();     
        customerOrder.customerOfOrder = this;

        // create dialogue for this customer based on the order and specific customer
        string newDialogue = "Hello! I would like a " + customerOrder.drinks[0].drinkName + " please.";
        dialogue.SetDialogue(newDialogue);

        // determine where the customer will go to pick up the order
        float randomX = Random.Range(pickUpZoneLeftCorner.x, pickUpZoneRightCorner.x);
        float randomZ = Random.Range(pickUpZoneLeftCorner.z, pickUpZoneRightCorner.z);
        pickupPosition = new Vector3(randomX, 0, randomZ);
    }

    void Update() {
        if(npcState == "in line") {
            HandleInLineState();
        } else if(npcState == "ordering") {
            HandleOrderingState();
        } else if (npcState == "picking up") {
            HandlePickingUpState();
        } else if (npcState == "leaving") {
            HandleLeavingState();
        } else {
            HandleUndefinedState();
        }
    }
    void HandleInLineState() {
        dialogue.dialogueEnabled = false;
    }
    
    void HandleOrderingState() {
        //Tracks the players response to dialogue
        int playerResponse = dialogue.playerResponse;

        switch(playerResponse) {
            case 1:
                dialogue.dialogueEnabled = false;
                ordersController.PlaceOrder(customerOrder);
                SetNPCState("picking up");
                break;
            case 2: 
                SetNPCState("leaving");
                break;
            default:
                dialogue.dialogueEnabled = true;
                break;
        }
    }

    void HandlePickingUpState() {
        transform.position = pickupPosition;
    }

    void HandleLeavingState() {
        Destroy(gameObject);
    }
    
    void HandleUndefinedState() {
        dialogue.dialogueEnabled = false;
    }

    public void SetNPCState(string state) {
        npcState = state;
    }
}
