using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCurrentMoney : MonoBehaviour
{
    private float currentMoney = 0; // 0 = sunday... 6 = saturday

    private TMP_Text text;

    void Start() {
        currentMoney = PlayerPrefs.GetFloat("money", 0);

        text = GetComponent<TMP_Text>();

        text.text = "$" + currentMoney;
    }
}
