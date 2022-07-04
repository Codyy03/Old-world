using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class KingBuissnesQuest : MonoBehaviour
{
    public Quest quest;

    NPCConversation conversation;
    ShowNotification notification;
    InventorySystem inventory;
    void Start()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        conversation = GetComponent<NPCConversation>();
        notification = GetComponent<ShowNotification>();
    }

    // Update is called once per frame
    void Update()
    {
        if(quest.afterTalkWithSomebody)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            return;
        }
        if (quest.questAccepted != 1 )
            return;

        if (quest.questAccepted == 1)
            notification.enabled = true;

        if (Input.GetKeyDown(KeyCode.E) && notification.distance <= notification.distanceToShow && !quest.exitQuest)
        {
            ConversationManager.Instance.StartConversation(conversation);
            ConversationManager.Instance.SetBool("AfterTalkWithKingGuard", quest.afterTalkWithSomebody);
        }

        if (ConversationManager.Instance.IsConversationActive)
            ConversationManager.Instance.SetInt("HasMoneyToPass", inventory.playerGold);
    }

    public void AfterTalkWithGuard()
    {
        quest.afterTalkWithSomebody = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
            quest.questAccepted = 1;
    }

    public void PayToPass()
    {
        inventory.playerGold -= 10;
    }

}
