using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class GuardQuest : MonoBehaviour
{
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
        quest.NPCInQuest[1] = GameObject.Find("guard2");

        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        conversation = quest.NPCInQuest[1].GetComponent<NPCConversation>();
        notification = quest.NPCInQuest[1].GetComponent<ShowNotification>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && notification.distance <= notification.distanceToShow )
        {
            ConversationManager.Instance.StartConversation(conversation);
            ConversationManager.Instance.SetBool("HasWormNote", hasNote);
        }

        if (inventory.IsItemInInventory(400))
            hasNote = true;
        else
            hasNote = false;
    }
    public void GetExtraReword()
    {
        inventory.playerGold += 10;
        inventory.ReduceItem(400, 1);
        audioManager.PlayClip(getRewordSound);
    }
}
