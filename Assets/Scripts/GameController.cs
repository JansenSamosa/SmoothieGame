using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int shopOpenTime = 480;
    [SerializeField]
    private int shopCloseTime = 1200;

    [SerializeField]
    private GameObject shopClosedUI;

    private GameClockController clockController;
    private OrdersController ordersController;
    private MoneyController moneyController;
    
    [SerializeField]
    private CustomerLineController customerLine;
    
    void Start()
    {
        clockController = GetComponent<GameClockController>();
        ordersController = GetComponent<OrdersController>();
        moneyController = GetComponent<MoneyController>();

        shopClosedUI.SetActive(false);
        clockController.inGameTime = shopOpenTime;
    }

    void Update() {
        if(clockController.inGameTime >= shopCloseTime) {
            CloseShop();
        }
    }

    void CloseShop() {
        for(int i = 0; i < ordersController.activeOrders.Count; i++) {
            ordersController.IncompleteOrder(ordersController.activeOrders[i]);
        } // mark all active orders as incomplete

        clockController.PauseClock(); // pause in game time
        customerLine.gameObject.SetActive(false); // disable customers
        shopClosedUI.SetActive(true);

        shopClosedUI.transform.GetChild(2).GetComponent<TMP_Text>().text = "Profits: $" + moneyController.playerMoney;
    }
}