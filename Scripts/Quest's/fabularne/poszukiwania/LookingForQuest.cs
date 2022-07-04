using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class LookingForQuest : MonoBehaviour
{
   
    public Quest quest;

 
    NPCConversation conversation, tytusConversation;
    ShowNotification notification;
    
    void Start()
    {
        conversation = GetComponent<NPCConversation>();

        quest.NPCInQuest[0] = GameObject.Find("old man");

        tytusConversation = quest.NPCInQuest[0].GetComponent<NPCConversation>();

        notification = quest.NPCInQuest[0].GetComponent<ShowNotification>();

        if (quest.questAccepted==0)
            ConversationManager.Instance.StartConversation(conversation);

    }

    // Update is called once per frame
    void Update()
    {

       

        if (quest.afterTalkWithSomebody)
        {
            notification.enabled = false;
            notification.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }
       
        if (notification.distance<=notification.distanceToShow && Input.GetKeyDown(KeyCode.E) && quest.questAccepted == 1)
        {
            ConversationManager.Instance.StartConversation(tytusConversation);
        }




    }

    public void AfterTalk()
    {
        quest.questAccepted = 1;

    }

    public void AfterTalkWithTytus()
    {
        quest.afterTalkWithSomebody = true;
        //notification.enabled = false;
        //notification.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        ConversationManager.Instance.SetBool("afterTalkWithTitus", quest.afterTalkWithSomebody);
    }
}
