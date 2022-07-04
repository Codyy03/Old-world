using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class EurycyQuest : MonoBehaviour
{
    [SerializeField] GameObject houseDoor, fireWorm;
    [SerializeField] AudioClip getRewordSound;
    public Quest quest;

    private bool hasNote;
    NPCConversation conversation;
    ShowNotification notification;
    AudioManager audioManager;
    InventorySystem inventory;
    // Start is called before the first frame update
    void Start()
    {
        quest.NPCInQuest[0] = GameObject.Find("Eurycy");
       
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        conversation = quest.NPCInQuest[0].GetComponent<NPCConversation>();
        notification = quest.NPCInQuest[0].GetComponent<ShowNotification>();
    }

    // Update is called once per frame
    void Update()
    {

        if (quest.exitQuest)
        {
            quest.NPCInQuest[0].GetComponent<ShowNotification>().enabled = false;
            return;

        }
        if (quest.questAccepted == 0)
            return;

        if (Input.GetKeyDown(KeyCode.E) && notification.distance <= notification.distanceToShow && !quest.exitQuest)
        {
            ConversationManager.Instance.StartConversation(conversation);

            if (quest.afterTalkWithSomebody)
                AfterTalkWithEurycy();

            if (quest.beforeExitQuestTalk)
                ConversationManager.Instance.SetBool("BeforExitQuest", quest.beforeExitQuestTalk);

            
                ConversationManager.Instance.SetBool("HasNote", hasNote);
              
        }

        if (inventory.IsItemInInventory(400))
            hasNote = true;
        else
            hasNote = false;

        if (quest.afterTalkWithSomebody && !quest.beforeExitQuestTalk)
        {
            houseDoor.GetComponent<Teleport>().enabled = true;
            houseDoor.GetComponent<ShowNotification>().enabled = true;
        } else
        {
            houseDoor.GetComponent<Teleport>().enabled = false;
            houseDoor.GetComponent<ShowNotification>().enabled = false;
        }

        if (fireWorm == null)
            quest.beforeExitQuestTalk = true;
    }

    public void AfterTalkWithEurycy()
    {
        quest.afterTalkWithSomebody = true;

        ConversationManager.Instance.SetBool("AfterTalkWithEurycy", quest.afterTalkWithSomebody);
    }
    public void GetReword()
    {
        quest.exitQuest = true;
        inventory.playerGold += quest.reword;
        quest.NPCInQuest[0].GetComponent<ShowNotification>().enabled = false;
        quest.NPCInQuest[0].GetComponent<ShowNotification>().gameObject.transform.GetChild(1).gameObject.SetActive(false);
        houseDoor.GetComponent<Teleport>().enabled = false;
        houseDoor.GetComponent<ShowNotification>().enabled = false;
        audioManager.PlayClip(getRewordSound);
    }

    public void GetExtraReword()
    {
        inventory.ReduceItem(400, 1);
        inventory.playerGold += 15;
        audioManager.PlayClip(getRewordSound);
    }
}
