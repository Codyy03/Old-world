using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class GothicavaniaGuard : MonoBehaviour
{
    [SerializeField] Quest quest;

   
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
        if (notification.distance <= notification.distanceToShow && quest.bonus && quest.afterTalkWithSomebody && Input.GetKeyDown(KeyCode.E))
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }
}
