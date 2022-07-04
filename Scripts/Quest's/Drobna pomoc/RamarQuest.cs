using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class RamarQuest : MonoBehaviour
{
    public Quest quest;
    NPCConversation conversation;
    ShowNotification notification;
    InventorySystem inventory;
    LucjuszQuest lucjusz;
    void Start()
    {
        quest.NPCInQuest[1] = GameObject.Find("Ramar");
        conversation = quest.NPCInQuest[1].GetComponent<NPCConversation>();
        notification = quest.NPCInQuest[1].GetComponent<ShowNotification>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        lucjusz = GetComponent<LucjuszQuest>();
        quest.NPCInQuest[1].transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!quest.afterTalkWithSomebody|| quest.exitQuest || quest.beforeExitQuestTalk)
            return;

        if(quest.afterTalkWithSomebody)
            quest.NPCInQuest[1].GetComponent<ShowNotification>().enabled = true;

        if (Input.GetKeyDown(KeyCode.E) && notification.distance <= notification.distanceToShow && quest.NPCInQuest[1] != null && !lucjusz.hasRing)
        {
            ConversationManager.Instance.StartConversation(conversation);
            if (quest.afterTalkWithSomebody && quest.NPCInQuest[1] != null)
            {
                quest.NPCInQuest[1].gameObject.GetComponent<ShowNotification>().enabled = true;

            }
            else if (!quest.afterTalkWithSomebody)
                quest.NPCInQuest[1].GetComponent<ShowNotification>().enabled = false;


            ConversationManager.Instance.SetInt("HasEnoughMoney", inventory.playerGold);
        }

       



    }
    public void StartFight()
    {
        quest.NPCInQuest[1].transform.GetChild(0).gameObject.SetActive(true);
        quest.NPCInQuest[1].transform.GetChild(3).gameObject.SetActive(false);
        quest.NPCInQuest[1].GetComponent<ShowNotification>().enabled = false;
        quest.NPCInQuest[1].GetComponent<EnemyController>().enabled = true;
        quest.NPCInQuest[1].GetComponent<BoxCollider2D>().enabled = true;
        quest.beforeExitQuestTalk = true;
    }
    public void BuyRing()
    {
       
        quest.NPCInQuest[1].GetComponent<ShowNotification>().enabled = false;
     
        quest.NPCInQuest[1].transform.GetChild(3).gameObject.SetActive(false);
        inventory.playerGold -= 15;
        inventory.CreateShortcut(201);
        quest.beforeExitQuestTalk = true;
    }

}
