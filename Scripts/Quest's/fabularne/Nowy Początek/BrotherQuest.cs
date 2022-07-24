using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class BrotherQuest : MonoBehaviour
{
    [SerializeField] Quest quest;
    NPCConversation conversation;
    ShowNotification notification;
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        notification = GetComponent<ShowNotification>();
        if (quest.questAccepted != 1)
            gameObject.SetActive(false);

        if(quest.afterTalkWithSomebody)
        {
            gameObject.transform.position = new Vector2(71.01f, 28.31f);
            GameObject.Find("Wide_Door_03").GetComponent<ShowNotification>().enabled = true;
            GameObject.Find("Wide_Door_03").GetComponent<Teleport>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (notification.distance <= notification.distanceToShow && Input.GetKeyDown(KeyCode.E) && !quest.afterTalkWithSomebody)
        {
            ConversationManager.Instance.StartConversation(conversation);

        }
    }

    public void TeleportToMagHouse()
    {
        gameObject.transform.position = new Vector2(71.01f, 28.31f);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(68.73f, 27.59f);
    }

    public void AfterTalkWithMag()
    {
        quest.afterTalkWithSomebody = true;
        GameObject.Find("Wide_Door_03").GetComponent<ShowNotification>().enabled = true;
        GameObject.Find("Wide_Door_03").GetComponent<Teleport>().enabled = true;
    }
    
}
