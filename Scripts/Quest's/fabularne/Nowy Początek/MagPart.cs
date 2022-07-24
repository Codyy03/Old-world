using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class MagPart : MonoBehaviour
{
    [SerializeField] Quest quest;
    NPCConversation conversation;
    ShowNotification notification;
    
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        notification = GetComponent<ShowNotification>();
        if (quest.questAccepted != 1)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (notification.distance <= notification.distanceToShow && Input.GetKeyDown(KeyCode.E) && quest.afterTalkWithSomebody)
        {
            ConversationManager.Instance.StartConversation(conversation);

        }
    }

   
}
