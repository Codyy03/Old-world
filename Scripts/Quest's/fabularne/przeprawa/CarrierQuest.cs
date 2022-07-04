using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class CarrierQuest : MonoBehaviour
{
    [SerializeField] GameObject nextQuestPrefab;
    public Quest quest;
    NPCConversation conversation;
    InventorySystem inventory;
    ShowNotification notification;
    DestroyPreviusQuest destroyPrevius;
    SendQuest sendQuest;
    void Start()
    {
        quest.NPCInQuest[0] = GameObject.Find("carrier");
        destroyPrevius = GetComponentInParent<DestroyPreviusQuest>();
        conversation = quest.NPCInQuest[0].GetComponent<NPCConversation>();
        notification = quest.NPCInQuest[0].GetComponent<ShowNotification>();
        sendQuest = GameObject.Find("Quest Manager").GetComponent<SendQuest>();

        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && notification.distance <= notification.distanceToShow )
        {
            ConversationManager.Instance.StartConversation(conversation);

            ConversationManager.Instance.SetBool("AfterTalkWithCarrier", quest.afterTalkWithSomebody);

            ConversationManager.Instance.SetInt("HasEnoughMoneyToTravel", inventory.playerGold);

            ConversationManager.Instance.SetBool("BeforTravel", quest.beforeExitQuestTalk);

            ConversationManager.Instance.SetBool("ExitCarrierQuest", quest.exitQuest);
        }
    }

    public void AfterTalkWithCarrier()
    {
        quest.afterTalkWithSomebody = true;
        ConversationManager.Instance.SetBool("AfterTalkWithCarrier", quest.afterTalkWithSomebody);
    }

    public void BefroTravel()
    {
        quest.beforeExitQuestTalk = true;
        ConversationManager.Instance.SetBool("BeforTravel", quest.beforeExitQuestTalk);
    }

    public void PayForTravel()
    {
        quest.exitQuest = true;
        ConversationManager.Instance.SetBool("ExitCarrierQuest", quest.exitQuest);
        inventory.playerGold -= 75;
        destroyPrevius.DestroyOldQuest();
        sendQuest.SendQuestToCanvas(nextQuestPrefab);
    }

}
