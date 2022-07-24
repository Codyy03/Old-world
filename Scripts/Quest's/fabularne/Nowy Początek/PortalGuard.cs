using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class PortalGuard : MonoBehaviour
{
    [SerializeField] Quest quest;

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
        if (notification.distance <= notification.distanceToShow && !quest.bonus && quest.afterTalkWithSomebody && !conversationWasStarted)
        {
            ConversationManager.Instance.StartConversation(conversation);
            conversationWasStarted = true;
        }
    }
    public void CrossePortal()
    {
        quest.bonus = true;
    }
}
