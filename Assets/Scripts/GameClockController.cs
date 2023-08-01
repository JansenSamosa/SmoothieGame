using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClockController : MonoBehaviour
{   
    [SerializeField] 
    private TMP_Text clock; 
    [SerializeField]
    private Transform sun;
    
    public int inGameTime = 0; // in game time in minutes i.e. 480 = 8:00AM
    
    [SerializeField]
    private int timePassRate = 1; // How much minutes the game time passes per real-time second

    private float realTimePassed = 0;

    public bool isClockRunning = true;

    void Update() {
        if(isClockRunning) {
            realTimePassed += Time.deltaTime;

            if(realTimePassed >= 1) {
                inGameTime += timePassRate;
                UpdateUI();                
                realTimePassed = 0;
            }
        }
        UpdateSun();
    }

    void UpdateSun() {
        Quaternion newRot = Quaternion.Euler((float)inGameTime/1440 * 180,0,0);
        sun.rotation = Quaternion.Lerp(sun.rotation, newRot, 2 * Time.deltaTime);
    }

    void UpdateUI() {
        int minute = inGameTime % 60;
        int hour = inGameTime / 60;
        string AMorPM = "AM";

        if(hour >= 12) {
            hour -= 12;
            AMorPM = "PM";
        }
        
        if(hour == 0) hour = 12;
        
        string minuteString = minute < 10 ? "0" + minute : minute.ToString();
        
        clock.text = hour + ":" + minuteString + " " + AMorPM;
    }

    public void PauseClock() {
        isClockRunning = false;
    }
}
