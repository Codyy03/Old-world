using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class ExileQuestPart : MonoBehaviour
{
    public Quest quest;

    bool conversationWasStarted;

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

        if (!quest.beforeExitQuestTalk)
            notification.enabled = true;
        else
        {
            notification.enabled = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }

        if (notification.distance <= notification.distanceToShow && !conversationWasStarted)
        {
            ConversationManager.Instance.StartConversation(conversation);
            conversationWasStarted = true;

        }
    }

    public void TalkWithExile()
    {

        quest.beforeExitQuestTalk = true;
    }
}
