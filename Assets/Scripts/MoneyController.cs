using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    public float playerMoney = 0; // how much money the player currently has 
    public TMP_Text moneyUI;

    void Update() {
        moneyUI.text = "$" + playerMoney;
    }

    public void AddMoney(float amount) {
        playerMoney += amount;
    }

    public void SubtractMoney(float amount) {
        playerMoney -= amount;
    }
}
