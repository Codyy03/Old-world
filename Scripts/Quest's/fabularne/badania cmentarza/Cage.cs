using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class Cage : MonoBehaviour
{
    [SerializeField] Quest quest, nextQuest;
    [SerializeField] AudioClip destroyCage;

    DestroyPreviusQuest destroyPrevius;
    NPCConversation conversation;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        if (quest.questAccepted == 1)
            Destroy(gameObject);
        destroyPrevius = GameObject.Find("Quest Manager").GetComponent<DestroyPreviusQuest>();
        conversation = GetComponent<NPCConversation>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!quest.exitQuest)
        {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        audioManager.PlayClip(destroyCage);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        ConversationManager.Instance.StartConversation(conversation);
        nextQuest.questAccepted = 1;
        destroyPrevius.DestroyOldQuest();
        }
    }
}
