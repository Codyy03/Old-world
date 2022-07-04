using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class ElliksirSellerQuest : MonoBehaviour
{
    [SerializeField] AudioClip getRewordSound;
    [SerializeField] GameObject potionInShop;
    public Quest quest;

    public float timeToWait;
    AudioManager audioManager;
    InventorySystem inventory;
    // Start is called before the first frame update
    void Start()
    {

        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.SetInt("QuestAccepted", quest.questAccepted);

            ConversationManager.Instance.SetBool("AfterTalkWithEliksir", quest.afterTalkWithSomebody);

            ConversationManager.Instance.SetBool("HasEnoughBones", quest.beforeExitQuestTalk);
            ConversationManager.Instance.SetBool("ExitQuest", quest.exitQuest);

            if (quest.afterTalkWithSomebody)
                AfterTalkWithSeller();

            if (inventory.HowManyItemsInSlot(205) >= 7)
                quest.beforeExitQuestTalk = true;
            else quest.beforeExitQuestTalk = false;

            if (quest.exitQuest)
                ConversationManager.Instance.SetInt("PlayerWaitEnough",(int)timeToWait);
        }

        if (quest.exitQuest && timeToWait > 0)
            timeToWait -= Time.deltaTime;

        if (timeToWait <= 0)
            potionInShop.SetActive(true);
    }

    public void AfterTalkWithSeller()
    {
        quest.afterTalkWithSomebody = true;
        ConversationManager.Instance.SetBool("AfterTalkWithEliksir", quest.afterTalkWithSomebody);
    }
    public void GetReword()
    {
        inventory.playerGold += quest.reword;
        inventory.ReduceItem(205, 7);
        audioManager.PlayClip(getRewordSound);
        quest.exitQuest = true;
    }

    public void GetExtraReword()
    {
        inventory.CreateShortcut(4);
    }
}
