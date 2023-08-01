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

    private float prevTime = 0;

    //Tip related variables
    [SerializeField] private int timeToMakeOrder = 45;
    [SerializeField] private float maxTip = 5;
    [SerializeField] private float timeRemainingForMaxTip = 30;
    [SerializeField] private float timeRemainingForMinTip = 5;
    [SerializeField] private float depreciationRate;
    private float tipAmount = 0;
    

    void Awake() {
        drinkOrderInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<Dictionaries>().drinkOrderInfo;
        moneyController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MoneyController>();

    }

    void Start() {
        //Sets depreciation rate for the tip to customer based on adjustable variables
        depreciationRate = maxTip / (timeRemainingForMaxTip - timeRemainingForMinTip);
    }
    
    void Update() {
        if(Time.time - prevTime >= 1) {
            for(int i = 0; i < activeOrders.Count; i++) {
                Order updatedOrder = activeOrders[i];
                updatedOrder.timeRemaining -= 1;

                activeOrders[i] = updatedOrder;
                activeOrdersUI[i].UpdateUI(updatedOrder);

                if(updatedOrder.timeRemaining <= 0) {
                    IncompleteOrder(updatedOrder);
                }
            }
            prevTime = Time.time;
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
    }

    public void CompleteOrder(Order order) {
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
        moneyController.AddMoney(order.totalCost);
        moneyController.AddMoney(tipAmount);
    }
    
    IEnumerator createRandomOrders() {
        while(true) {
            PlaceOrder(CreateRandomOrder());
            yield return new WaitForSeconds(10);
        }
    } 
}
