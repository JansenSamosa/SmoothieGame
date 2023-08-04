using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectActivity : MonoBehaviour
{      
    [SerializeField] private GameObject beforeActivityUI;
    [SerializeField] private GameObject beforeActivitySun;

    [SerializeField] private GameObject afterActivityUI;
    [SerializeField] private GameObject afterActivitySun;

    private string currentGameState;

    void Start() {
        currentGameState = PlayerPrefs.GetString("game-state", "activity-choose-day");

        if(currentGameState != "activity-choose-day" && currentGameState != "activity-choose-night") {
            currentGameState = "activity-choose-day";
        }     

        HandleDayOrNightStates();
    }

    void HandleDayOrNightStates() {
        bool isDay = currentGameState == "activity-choose-day";

        beforeActivityUI.SetActive(isDay);
        beforeActivitySun.SetActive(isDay);

        afterActivityUI.SetActive(!isDay);
        afterActivitySun.SetActive(!isDay);
    }

    public void OpenShop() {
        PlayerPrefs.SetString("game-state", "shop-open");
        SceneManager.LoadScene("SampleScene");
    }

    public void GatherMaterials() {
        PlayerPrefs.SetString("game-state", "minigame");
    }

    public void RoamShop() {
        PlayerPrefs.SetString("game-state", "shop-roam");
    }

    public void Sleep() {
        PlayerPrefs.SetString("game-state", "activity-choose-day");
        
        int currentDay = PlayerPrefs.GetInt("day-of-week", 0);
        currentDay += 1;
        if(currentDay > 6 || currentDay < 0) currentDay = 0;
        PlayerPrefs.SetInt("day-of-week", currentDay);

        SceneManager.LoadScene("SelectActivityScene");
    }
}
