using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [SerializeField] GameObject gainLossAnimPrefab;
    public float playerMoney = 0; // how much money the player currently has 

    [SerializeField] private TMP_Text moneyUI;

    void Update() {
        moneyUI.text = "$" + playerMoney;
        playerMoney = TruncateToTwoDecimalPoints(playerMoney);
    }

    public void AddMoney(float amount) {
        playerMoney += TruncateToTwoDecimalPoints(amount);
    }

    public void SubtractMoney(float amount) {
        playerMoney -= TruncateToTwoDecimalPoints(amount);
    }

    public void PlayGainLossAnim(float amount, Vector3 spawnPos, bool isTip) {
        Debug.Log(spawnPos);
        MoneyGainLossAnim newAnim = Instantiate(gainLossAnimPrefab, spawnPos, Quaternion.identity).GetComponent<MoneyGainLossAnim>();
        newAnim.transform.LookAt(Camera.main.transform);
        newAnim.StartPlaying(TruncateToTwoDecimalPoints(amount), isTip);
    }

    float TruncateToTwoDecimalPoints(float amount) {
        return Mathf.Round(amount * 100f) / 100f;
    }
}
