using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class BanditLeader : MonoBehaviour
{
    [SerializeField] GameObject[] bandits;
    public Quest quest, carrierQuest;


    private bool afterTalk, banditsAreDead;
    NPCConversation conversation;
    ShowNotification notification;
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        notification = GetComponent<ShowNotification>();
    }

    // Update is called once per frame
    void Update()
    {
        if (quest.exitQuest )
            return;

        if (quest.questAccepted == 1)
            GetComponent<ShowNotification>().enabled = true;
        else if (quest.exitQuest )
        {
            GetComponent<ShowNotification>().enabled = false;

        }


        if (notification.distance <= notification.distanceToShow && !quest.exitQuest  && !afterTalk)
        {
            afterTalk = true;
            ConversationManager.Instance.StartConversation(conversation);
        }

        if(bandits[0] == null && bandits[1]! == null && bandits[2] == null && !banditsAreDead)
        {
            banditsAreDead = true;
            
            afterTalk = false;
            quest.beforeExitQuestTalk = true;

             
        }

        if (ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.SetBool("BeforSpeakWityBanditLead", carrierQuest.beforeExitQuestTalk);
            ConversationManager.Instance.SetBool(" TalkAboutArmorSeller", carrierQuest.beforeExitQuestTalk);

            if(banditsAreDead)
                ConversationManager.Instance.SetBool("BnaditsAreDead", true);

        }

       
    }

    public void Attack()
    {
        
        for (int i = 0; i < bandits.Length; i++)
        {
            bandits[i].GetComponent<EnemyMode>().EneableAttackingMode();
        }
    }

    public void BeforTalkWithBandit()
    {
        carrierQuest.beforeExitQuestTalk = true;
    }
}
