using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Dialogue : MonoBehaviour
{
    public NPCConversation conversation;
    public int playerResponse = 0;
    public int active = 0;
    //private Conversation convo;
    // Start is called before the first frame update
    void Start()
    {
        //convo = conversation.Deserialize();
        //Debug.Log(convo.Root.Name);
        ConversationManager.Instance.StartConversation(conversation);
    }

    // Update is called once per frame
    void Update()
    {
        active = ConversationManager.Instance.GetInt("active");
        if (active == 0) {
            playerResponse = ConversationManager.Instance.GetInt("playerResponse");
        }
    }
}
