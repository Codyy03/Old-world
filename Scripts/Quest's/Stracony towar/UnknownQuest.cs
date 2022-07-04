using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class UnknownQuest : MonoBehaviour
{

    [SerializeField] AudioClip getRewordSound;
    [SerializeField] GameObject questPrefab;
    public Quest quest;


    private bool afterTalk;
    NPCConversation conversation;
    ShowNotification notification;
    SendQuest sendQuest;
    AudioManager audioManager;
    InventorySystem inventory;
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        notification = GetComponent<ShowNotification>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        sendQuest = GameObject.Find("Quest Manager").GetComponent<SendQuest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (quest.exitQuest)
            return;

        if (quest.questAccepted == 0)
           GetComponent<ShowNotification>().enabled = true;
       else if(quest.exitQuest) GetComponent<ShowNotification>().enabled = false;


        if ( notification.distance <= notification.distanceToShow && !quest.exitQuest && !quest.afterTalkWithSomebody && !afterTalk)
        {
            afterTalk = true;
            quest.questAccepted = 1;
            ConversationManager.Instance.StartConversation(conversation);
            sendQuest.SendQuestToCanvas(questPrefab);

        } else if(Input.GetKeyDown(KeyCode.E) && notification.distance <= notification.distanceToShow && !quest.exitQuest && quest.afterTalkWithSomebody)
        {
            ConversationManager.Instance.StartConversation(conversation);

            ConversationManager.Instance.SetBool("AfterTalkWithUnknown", quest.afterTalkWithSomebody); 
            ConversationManager.Instance.SetBool("HasCargo", quest.beforeExitQuestTalk);
        }

        if(inventory.IsItemInInventory(206))
        {
            quest.beforeExitQuestTalk = true;
        }
        else quest.beforeExitQuestTalk = false;
    }

   public void AfterTalkWithUnknown()
    {
        quest.afterTalkWithSomebody = true;

    }
    public void GetReword()
    {
        audioManager.PlayClip(getRewordSound);
        inventory.playerGold += quest.reword;
        quest.exitQuest = true;
        inventory.ReduceItem(206,1);

        for(int i=0; i<3; i++)
        inventory.CreateShortcut(2);
    }
}
