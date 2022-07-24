using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class BanditLeader : MonoBehaviour
{
    [SerializeField] GameObject[] bandits;
    [SerializeField] GameObject gate;
    public Quest quest;


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

        if(quest.beforeExitQuestTalk)
        {
            for (int i = 0; i < bandits.Length; i++)
                Destroy(bandits[i]);

            gate.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (bandits[0] == null && bandits[1]! == null && bandits[2] == null && !banditsAreDead)
        {
            banditsAreDead = true;

            afterTalk = false;
            quest.beforeExitQuestTalk = true;


        }
        if (ConversationManager.Instance.IsConversationActive)
        {
            if (banditsAreDead)
            {
                ConversationManager.Instance.SetBool("BnaditsAreDead", true);
                ConversationManager.Instance.SetBool("BanditsAreDeadArmorSelle", true);
            }
        }
     

        if (quest.exitQuest)
            return;

        if (quest.questAccepted == 1)
            GetComponent<ShowNotification>().enabled = true;
        else if (quest.exitQuest )
        {
            GetComponent<ShowNotification>().enabled = false;
            gameObject.transform.GetChild(2).gameObject.SetActive(false);

        }


        if (notification.distance <= notification.distanceToShow && !quest.exitQuest  && !afterTalk)
        {
            afterTalk = true;
            ConversationManager.Instance.StartConversation(conversation);
        }

       

      

       
    }

    public void Attack()
    {
        
        for (int i = 0; i < bandits.Length; i++)
        {
            bandits[i].GetComponent<EnemyMode>().EneableAttackingMode();
        }
    }

   
}
