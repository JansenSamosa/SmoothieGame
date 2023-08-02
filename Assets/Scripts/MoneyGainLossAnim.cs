using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyGainLossAnim : MonoBehaviour
{
    private float moneyAmount = -5;

    [SerializeField] private Color positiveAmountColor;
    [SerializeField] private Color positiveTipAmountColor;
    [SerializeField] private Color negativeAmountColor;

    [SerializeField] private TMP_Text text;

    [SerializeField] private AudioClip gainMoneySound;
    [SerializeField] private AudioClip loseMoneySound;
    
    public void StartPlaying(float amount, bool isTip) {
        AudioSource audioSource = GetComponent<AudioSource>();

        audioSource.enabled = true;
        GetComponent<Animator>().enabled = true;
        
        moneyAmount = amount;

        if(moneyAmount < 0) { // if negative amount
            text.color = negativeAmountColor;
            text.text = "-$" + Mathf.Abs(moneyAmount);
            audioSource.PlayOneShot(loseMoneySound);
            
        } else { // if positive amount
            text.color = isTip ? positiveTipAmountColor : positiveAmountColor;
            text.text = "$" + Mathf.Abs(moneyAmount);
            audioSource.PlayOneShot(gainMoneySound);
        } 
    }

    public void DestroyThis() {
        Destroy(gameObject);
    }
}
