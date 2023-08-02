using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int shopOpenTime = 480;
    [SerializeField]
    private int shopCloseTime = 1200;

    [SerializeField] 
    private int rushHourStart = 780; //1:00PM
    
    [SerializeField]
    private int rushHourEnd = 900; //3:00PM

    private bool rushHourComplete = false;

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
        if (clockController.inGameTime >= rushHourStart && clockController.inGameTime < rushHourEnd) {
            customerLine.RushHour();
            Debug.Log("Rush Hour");
        }
        if (clockController.inGameTime >= rushHourEnd) {
            rushHourComplete = true;
            customerLine.notRushHour();
            Debug.Log("Not Rush Hour");
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

    public void Continue() {
        float currentMoney = PlayerPrefs.GetFloat("money", 0);

        PlayerPrefs.SetFloat("money", currentMoney + moneyController.playerMoney);
        PlayerPrefs.SetString("game-state", "activity-choose-night");

        SceneManager.LoadScene("SelectActivityScene");
    }
}
