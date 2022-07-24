using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class KingQuestPart : MonoBehaviour
{
    [SerializeField] GameObject[] enemysToKill;
    [SerializeField] GameObject[] kingGuards, enemiesWhoFightWithGuards;
    public Quest quest, nextQuest;

    bool conversationWasActivate;
    AudioSource audioSource;
    NPCConversation conversation;
    ScenManager scenManager;
    DestroyPreviusQuest destroyPrevius;
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        audioSource = GameObject.Find("End battle").GetComponent<AudioSource>();
        scenManager = GameObject.Find("Game Manager").GetComponent<ScenManager>();
        destroyPrevius = GameObject.Find("Quest Manager").GetComponent<DestroyPreviusQuest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!allEnemysAllDead())
            return;

        if(!conversationWasActivate)
        {

            for (int i = 0; i < kingGuards.Length; i++)
            {
                kingGuards[i].GetComponent<Animator>().SetTrigger("Idle");
                Destroy(enemiesWhoFightWithGuards[i]);
            }
        conversationWasActivate = true;
        ConversationManager.Instance.StartConversation(conversation);
        audioSource.Stop();

        }

        if (quest.exitQuest)
            gameObject.SetActive(false);


    }

    bool allEnemysAllDead()
    {
        bool areDead = false;

        for (int i = 0; i < enemysToKill.Length; i++)
        {
            if (enemysToKill[i] != null)
            {
                areDead = false;
                return areDead;
            }
            else areDead = true;
        }
        return areDead;
    }

   public void Teleport()
   {
        gameObject.transform.position = new Vector3(262.5f, -3.47f, 0);

        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(266.5f, -4.3f, 0);
    }

    public void TeleportToCapital()
    {
        scenManager.SetPositionToCity();
        scenManager.LoadFirstCity();
        ExitQuest();
    }

    public void ExitQuest()
    {
        quest.exitQuest = true;
        gameObject.SetActive(false);
        destroyPrevius.DestroyOldQuest();
        nextQuest.questAccepted = 1;
    }
}
