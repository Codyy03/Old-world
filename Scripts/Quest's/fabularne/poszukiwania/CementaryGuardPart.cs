using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class CementaryGuardPart : MonoBehaviour
{
    public Quest quest;
    NPCConversation conversation;

    ShowNotification notification;
    DestroyPreviusQuest destroyPrevius;
    CarrierQuest carrier;
    void Start()
    {
        quest.NPCInQuest[2] = GameObject.Find("darksouls-knight");
        conversation = quest.NPCInQuest[2].GetComponent<NPCConversation>();
        notification = quest.NPCInQuest[2].GetComponent<ShowNotification>();
        destroyPrevius = GetComponentInParent<DestroyPreviusQuest>();
        carrier = GameObject.Find("Przeprawa").GetComponent<CarrierQuest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!quest.afterTalkWithSomebody || !quest.beforeExitQuestTalk)
            return;

        if(notification.distance<=notification.distanceToShow && Input.GetKeyDown(KeyCode.E) && !quest.exitQuest)
        {
            ConversationManager.Instance.StartConversation(conversation);
           
        }

        if(ConversationManager.Instance.IsConversationActive)
            ConversationManager.Instance.SetBool("EnabledCarrier", quest.exitQuest); 
    }

    public void ExitQuest()
    {
        carrier.quest.questAccepted = 1;
        quest.exitQuest = true;
        destroyPrevius.DestroyOldQuest();
    }
}
