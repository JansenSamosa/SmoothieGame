using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public GameObject playerChoicesUI;
    public GameObject npcDialogueUI;

    public string npcDialogue = "Hi";
    public TMP_Text npcText;

    // button option player has selected (either 1, 2, or 3) 
    public int playerResponse = 0;
    public bool dialogueEnabled = true;

    public void ChooseResponse(int index) {
        Debug.Log(index);
        playerResponse = index;
    }

    void Update() {
        npcText.text = npcDialogue;

        if(!dialogueEnabled) {
            playerResponse = 4;
            playerChoicesUI.SetActive(false);
            npcDialogueUI.SetActive(false);
        } else {
            playerChoicesUI.SetActive(true);
            npcDialogueUI.SetActive(true);
        }
    }

    public void SetDialogue(string newDialogue) {
        npcDialogue = newDialogue;
    }
}
