using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class GuardPart : MonoBehaviour
{
    public Quest quest;
    NPCConversation conversation;

    ShowNotification notification;

    public bool converstationWasStarted;
    // Start is called before the first frame update
    void Start()
    {
        quest.NPCInQuest[1] = GameObject.Find("gurd4");
        conversation= quest.NPCInQuest[1].GetComponent<NPCConversation>();
        notification = quest.NPCInQuest[1].GetComponent<ShowNotification>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!quest.afterTalkWithSomebody  || quest.exitQuest)
        {
            notification.enabled = false;
            return;
        } else if(quest.afterTalkWithSomebody)
            notification.enabled = true;

        if (notification.distance <= notification.distanceToShow  && quest.questAccepted == 1 && !converstationWasStarted)
        {
            converstationWasStarted = true;
            ConversationManager.Instance.StartConversation(conversation);
        }

        if (quest.beforeExitQuestTalk)
        {
            notification.enabled = false;
            notification.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }    
    }

    public void AfterTalkWithGuard()
    {
        quest.beforeExitQuestTalk = true;
        
    }
}
