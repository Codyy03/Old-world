using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;
public class ArmorSellerQuest : MonoBehaviour
{
    public Quest carrierQuest;
    InventorySystem inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();

        if (carrierQuest.bonus && SceneManager.GetActiveScene().name == "Desert")
            gameObject.SetActive(false);


        if (!carrierQuest.bonus && SceneManager.GetActiveScene().name == "City")
            gameObject.SetActive(false);

        if (carrierQuest.bonus && SceneManager.GetActiveScene().name == "City")
            gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       

        if (ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.SetBool("BeforSpeakWityBanditLead", carrierQuest.beforeExitQuestTalk);
            ConversationManager.Instance.SetBool("TalkAboutArmorSeller", carrierQuest.beforeExitQuestTalk);

            ConversationManager.Instance.SetBool("ExitQuestArmorSeller", carrierQuest.exitQuest);

            if (SceneManager.GetActiveScene().name == "City")
                ConversationManager.Instance.SetBool("BonusAkcepted", carrierQuest.bonus);
        }

 
    }

    public void BeforTalkWithBandit()
    {
        carrierQuest.beforeExitQuestTalk = true;
    }

    public void AkceptPropositon()
    {
        carrierQuest.bonus = true;
        inventory.playerGold += 50;
    }

    public void DenyProposition()
    {
        inventory.playerGold += 100;
        carrierQuest.exitQuest = true;
    }
    public void GetExtraReword()
    {
        carrierQuest.exitQuest = true;
        inventory.playerGold += 50;
        inventory.CreateShortcut(104);
        carrierQuest.exitQuest = true;

    }
}
