using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class Innkeeper : MonoBehaviour
{
    [SerializeField] GameObject juliuszInHouse, enterJuliuszHouse,questPrefab;
    [SerializeField] Quest quest, nextQuest, giftOfThePast;

    NPCConversation conversation;
    ShowNotification notification;
    DestroyPreviusQuest destroyPrevius;
    SendQuest sendQuest;
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        notification = GetComponent<ShowNotification>();
        destroyPrevius = GameObject.Find("Quest Manager").GetComponent<DestroyPreviusQuest>();
        sendQuest = GameObject.Find("Quest Manager").GetComponent<SendQuest>();
        if (quest.exitQuest)
            ExitQuest();
    }

    // Update is called once per frame
    void Update()
    {
        if (notification.distance <= notification.distanceToShow && quest.bonus && quest.afterTalkWithSomebody && Input.GetKeyDown(KeyCode.E))
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }

    public void ExitQuest()
    {
        juliuszInHouse.SetActive(true);
        GameObject.Find("Juliusz").SetActive(false);
        quest.exitQuest = true;
      
        enterJuliuszHouse.GetComponent<ShowNotification>().enabled = true;
        enterJuliuszHouse.GetComponent<Teleport>().enabled = true;
        destroyPrevius.DestroyOldQuest();
        nextQuest.questAccepted = 1;
    }

    public void giftOfThePastAccept()
    {
        sendQuest.SendQuestToCanvas(questPrefab);
        giftOfThePast.questAccepted = 1;
    }
}
