using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class ReserchCementary : MonoBehaviour
{
    [SerializeField] GameObject castleGuard;
    public Quest kingBuissnes, quest;

    ShowNotification notification;
    NPCConversation conversation;
    // Start is called before the first frame update
    void Start()
    {
        quest.NPCInQuest[0] = GameObject.Find("king");
        notification = quest.NPCInQuest[0].GetComponent<ShowNotification>();
        conversation = quest.NPCInQuest[0].GetComponent<NPCConversation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(kingBuissnes.exitQuest && !quest.afterTalkWithSomebody)
        {
            castleGuard.GetComponent<BoxCollider2D>().enabled = true;
            castleGuard.GetComponent<ShowNotification>().enabled = true;
        } else 
        {
            castleGuard.GetComponent<BoxCollider2D>().enabled = false;
            castleGuard.GetComponent<ShowNotification>().enabled = false;

        }
        if (notification.distance <= notification.distanceToShow && Input.GetKeyDown(KeyCode.E) && !quest.exitQuest)
        {
            ConversationManager.Instance.StartConversation(conversation);

        }

    }

    public void AfterTalkWithKing()
    {
        quest.afterTalkWithSomebody = true;
    }
}
