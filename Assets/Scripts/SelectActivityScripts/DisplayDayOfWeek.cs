using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDayOfWeek : MonoBehaviour
{
    private int dayOfWeek = 0; // 0 = sunday... 6 = saturday

    private TMP_Text text;

    void Start() {
        dayOfWeek = PlayerPrefs.GetInt("day-of-week", 0);

        text = GetComponent<TMP_Text>();

        switch(dayOfWeek) {
            case 0:
                text.text = "Sunday";
                break;
            case 1:
                text.text = "Monday";
                break;
            case 2:
                text.text = "Tuesday";
                break;
            case 3:
                text.text = "Wednesday";
                break;
            case 4:
                text.text = "Thursday";
                break;
            case 5:
                text.text = "Friday";
                break;
            case 6:
                text.text = "Saturday";
                break;
            default:
                break;
        }
    }
}
