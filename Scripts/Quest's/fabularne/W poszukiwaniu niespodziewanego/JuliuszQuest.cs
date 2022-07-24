using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class JuliuszQuest : MonoBehaviour
{
    public Quest quest;


    
    NPCConversation conversation;
    ShowNotification notification;
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        notification = GetComponent<ShowNotification>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && notification.distance <= notification.distanceToShow && !quest.exitQuest )
        {
            ConversationManager.Instance.StartConversation(conversation);
            ConversationManager.Instance.SetBool("AfterTalkWithJuliusz", quest.afterTalkWithSomebody);
        }
    }
    public void AfterTalkWithJuliusz()
    {
        quest.afterTalkWithSomebody = true;
    }
}
