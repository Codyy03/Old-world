using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class LucjuszQuest : MonoBehaviour
{
    [SerializeField] AudioClip getRewordSound;
    public Quest quest;
    public bool hasRing;
    NPCConversation conversation;
    ShowNotification notification;

    AudioManager audioManager;
    InventorySystem inventory;
    void Start()
    {
        quest.NPCInQuest[0] = GameObject.Find("Lucjusz");

        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        conversation = quest.NPCInQuest[0].GetComponent<NPCConversation>();
        notification = quest.NPCInQuest[0].GetComponent<ShowNotification>();
       

    }

    // Update is called once per frame
    void Update()
    {
        if (quest.questAccepted==0 || quest.exitQuest)
            return;

        if(quest.questAccepted == 1)
            quest.NPCInQuest[0].GetComponent<ShowNotification>().enabled = true;

        if (Input.GetKeyDown(KeyCode.E) && notification.distance<=notification.distanceToShow && !quest.exitQuest)
        {
           
            ConversationManager.Instance.StartConversation(conversation);

            ConversationManager.Instance.SetBool("HasRing", hasRing);

            if (quest.afterTalkWithSomebody)
                AfterTalkWithLucjusz();

        }
        if (inventory.IsItemInInventory(201))
        {
            hasRing = true;
        }
        else
            hasRing = false;
    }
   
    public void GetReword()
    {
        inventory.playerGold += quest.reword;
        quest.exitQuest = true;
        inventory.ReduceItem(201, 1);
        quest.NPCInQuest[0].transform.GetChild(1).gameObject.SetActive(false);
        quest.NPCInQuest[0].GetComponent<ShowNotification>().enabled = false;
        audioManager.PlayClip(getRewordSound);
      
    }

    public void AfterTalkWithLucjusz()
    {
        quest.afterTalkWithSomebody = true;
        ConversationManager.Instance.SetBool("AfrterTalkWithLucjusz", quest.afterTalkWithSomebody);
    }
}
