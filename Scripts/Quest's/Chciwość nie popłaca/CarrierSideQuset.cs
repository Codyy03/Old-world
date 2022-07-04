using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class CarrierSideQuset : MonoBehaviour
{
    [SerializeField] AudioClip getRewordSound;
    [SerializeField] GameObject questPrefab;
    public Quest quest;


    private bool questSend;
    SendQuest sendQuest;
    AudioManager audioManager;
    InventorySystem inventory;
    void Start()
    {
        quest.NPCInQuest[0] = GameObject.Find("carrier");

        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        sendQuest = GameObject.Find("Quest Manager").GetComponent<SendQuest>();


    }

    // Update is called once per fra
    void Update()
    {
        if(ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.SetBool("AfterTalkWithCarrierSide", quest.afterTalkWithSomebody);
            ConversationManager.Instance.SetBool("questAkceptedCarrier", quest.afterTalkWithSomebody);
          
        }

        if(!questSend && quest.questAccepted == 1 && !quest.exitQuest)
        {
            questSend = true;
            sendQuest.SendQuestToCanvas(questPrefab);
        }
            
     }
    public void AcceptQuest()
    {
       
        quest.questAccepted = 1;
      
    }

    public void AfterTalkWithCarrier()
    {
        quest.afterTalkWithSomebody = true;
       
    }

    public void exitQuest()
    {
        quest.exitQuest = true;

    }
}
