using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
  
public class CustomerController : MonoBehaviour
{
    // states = "in line" or "ordering" or "picking up" or "leaving"
    public string npcState = "in line"; 

    public DialogueSystem dialogue;

    // Order the customer wants to place
    private Order customerOrder; 

    private OrdersController ordersController;

    private CustomerSatisfactionController customerSatisfactionController;

    [SerializeField] private Vector3 pickUpZoneLeftCorner;
    [SerializeField] private Vector3 pickUpZoneRightCorner;
    private Vector3 pickupPosition;

    // time limit to take a customer's order
    [SerializeField] private int timeLimitToTakeOrder = 70;
    private float realTimePassed = 0;
    [SerializeField] private Slider timerUI;

    //customer specific variables related to their personality
    [SerializeField] private string nameOfCustomer;
    [SerializeField] private string preferredDrink;

    void Awake() {
        ordersController = GameObject.FindGameObjectWithTag("GameController").GetComponent<OrdersController>();
        customerSatisfactionController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CustomerSatisfactionController>();
    }

    void Start() {  
        // create order for customer based on their preferred drink
        //if preferredDrink is not found, random order is created and returned
        customerOrder = ordersController.FindOrder(preferredDrink);    
        customerOrder.customerOfOrder = this;

        // create dialogue for this customer based on the order and specific customer
        string newDialogue = "Hello! My name is " + nameOfCustomer + ". I would like a " + customerOrder.drinks[0].drinkName + " please.";
        dialogue.SetDialogue(newDialogue);

        // determine where the customer will go to pick up the order
        float randomX = Random.Range(pickUpZoneLeftCorner.x, pickUpZoneRightCorner.x);
        float randomZ = Random.Range(pickUpZoneLeftCorner.z, pickUpZoneRightCorner.z);
        pickupPosition = new Vector3(randomX, 0, randomZ);
    }

    void Update() {
        //only show timer if the customer is in line or ordering
        timerUI.gameObject.SetActive(npcState == "ordering");
        
        if(npcState == "in line") {
            HandleInLineState();
            incrementTimeLimitToTakeCustomerOrder();
        } else if(npcState == "ordering") {
            HandleOrderingState();
            incrementTimeLimitToTakeCustomerOrder();
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
                customerOrder.timeMax = timeLimitToTakeOrder;
                customerOrder.timeRemaining = timeLimitToTakeOrder - realTimePassed;
                ordersController.PlaceOrder(customerOrder);
                SetNPCState("picking up");
                break;
            case 2: 
                SetNPCState("leaving");

                //Decrease customer satisfaction for refusing to serve the customer
                customerSatisfactionController.decreaseCustomerSatisfaction();

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

    void incrementTimeLimitToTakeCustomerOrder() {
        // incrementing the time limit to take the customer's order
        realTimePassed += Time.deltaTime;
        if (realTimePassed >= timeLimitToTakeOrder) {
            SetNPCState("leaving");

            //Decrease customer satisfaction for taking too long to take the customer's order
            customerSatisfactionController.decreaseCustomerSatisfaction();
        }
        timerUI.value = (float)(timeLimitToTakeOrder-realTimePassed)/timeLimitToTakeOrder;
    }
    public void SetNPCState(string state) {
        npcState = state;
    }
}

