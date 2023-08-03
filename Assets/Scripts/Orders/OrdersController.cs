using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order {
    public CustomerController customerOfOrder;

    public DrinkOrderInfo[] drinks;
    public float totalCost;
    public float timeMax;
    public float timeRemaining;
}

public class OrdersController : MonoBehaviour
{
    [SerializeField]
    public List<Order> activeOrders = new List<Order>();
    private List<OrderUIController> activeOrdersUI = new List<OrderUIController>();

    public GameObject orderUIPrefab;
    public Transform ordersUIContainer;

    private DrinkOrderInfo[] drinkOrderInfo;
    private MoneyController moneyController;

    private CustomerSatisfactionController customerSatisfactionController;

    private float realTimePassed = 0;

    //Tip related variables
    [SerializeField] private int timeToMakeOrder = 45;
    [SerializeField] private float maxTip = 5;
    [SerializeField] private float timeRemainingForMaxTip = 30;
    [SerializeField] private float timeRemainingForMinTip = 5;
    [SerializeField] private float CSTipModifier = 1;
    
    private float depreciationRate;
    private float tipAmount = 0;
    

    void Awake() {
        drinkOrderInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<Dictionaries>().drinkOrderInfo;
        moneyController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MoneyController>();
        customerSatisfactionController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CustomerSatisfactionController>();

    }

    void Start() {
        //Sets depreciation rate for the tip to customer based on adjustable variables
        depreciationRate = maxTip / (timeRemainingForMaxTip - timeRemainingForMinTip);
    }
    
    void Update() {
        realTimePassed += Time.deltaTime;
        float updateStepTime = 0.1f;
        
        if(realTimePassed >= updateStepTime) {
            for(int i = 0; i < activeOrders.Count; i++) {
                Order updatedOrder = activeOrders[i];
                updatedOrder.timeRemaining -= updateStepTime;

                activeOrders[i] = updatedOrder;
                activeOrdersUI[i].UpdateUI(updatedOrder);

                if(updatedOrder.timeRemaining <= 0) {
                    IncompleteOrder(updatedOrder);
                }
            }
            realTimePassed = 0;
        }
    }

    public Order CreateRandomOrder() {
        DrinkOrderInfo newDrink = drinkOrderInfo[Random.Range(0, drinkOrderInfo.Length)];
        DrinkOrderInfo[] newDrinks = {newDrink};
        
        // Create new order
        Order newOrder = new Order();
        newOrder.drinks = newDrinks;
        newOrder.totalCost = newDrink.cost;
        newOrder.timeMax = timeToMakeOrder;
        newOrder.timeRemaining = timeToMakeOrder;        

        return newOrder;
    }

    //this function will find the preferred drink of the customer in DrinkOrderInfo and return that order
    public Order FindOrder(string orderName) {
        for (int i = 0; i < drinkOrderInfo.Length; i++) {
            if (drinkOrderInfo[i].drinkName == orderName) {
                DrinkOrderInfo[] newDrinks = {drinkOrderInfo[i]};

                // Create new order
                Order newOrder = new Order();
                newOrder.drinks = newDrinks;
                newOrder.totalCost = drinkOrderInfo[i].cost;
                newOrder.timeMax = timeToMakeOrder;
                newOrder.timeRemaining = timeToMakeOrder;

                return newOrder;
            }
        }
        //if order not found, random order is created and returned
        Order randomOrder = CreateRandomOrder();
        return randomOrder;
    }

    public void PlaceOrder(Order order) {
        // Create UI object
        OrderUIController newUI = Instantiate(orderUIPrefab, ordersUIContainer).GetComponent<OrderUIController>();
        newUI.InitializeUI(order);

        activeOrders.Add(order);
        activeOrdersUI.Add(newUI);
    }

    public void IncompleteOrder(Order order) {
        order.customerOfOrder.SetNPCState("leaving");

        int index = activeOrders.IndexOf(order);
        Destroy(activeOrdersUI[index].gameObject);
        activeOrdersUI.RemoveAt(index);
        activeOrders.Remove(order);

        //Decrease customer satisfaction for an incomplete order
        customerSatisfactionController.decreaseCustomerSatisfaction();
    }

    public void CompleteOrder(Order order, Vector3 animPos) {
        order.customerOfOrder.SetNPCState("leaving");

        int index = activeOrders.IndexOf(order);
        Destroy(activeOrdersUI[index].gameObject);
        activeOrdersUI.RemoveAt(index);
        activeOrders.Remove(order);

        if (order.timeRemaining >= timeRemainingForMaxTip) {
            tipAmount = maxTip;
        } else if (order.timeRemaining > timeRemainingForMinTip) {
            tipAmount = maxTip - (depreciationRate * order.timeRemaining);
        } else {
            tipAmount = 0;
        }

        tipAmount = Mathf.Clamp(tipAmount, 0, maxTip);
        tipAmount = tipAmount * (CSTipModifier + customerSatisfactionController.customerSatisfaction);
    
        moneyController.AddMoney(order.totalCost);
        moneyController.PlayGainLossAnim(order.totalCost, animPos, false);

        moneyController.AddMoney(tipAmount);
        moneyController.PlayGainLossAnim(tipAmount, animPos + new Vector3(0.1f, 0.25f, 0.1f), true);

        //Increase customer satisfaction for completing an order on time
        customerSatisfactionController.increaseCustomerSatisfaction();
    }
    
    IEnumerator createRandomOrders() {
        while(true) {
            PlaceOrder(CreateRandomOrder());
            yield return new WaitForSeconds(10);
        }
    } 
}
