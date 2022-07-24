using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;
public class FastTravel : MonoBehaviour
{
    public static bool isFastTravel;
    [SerializeField] GameObject notification;
    [SerializeField] Vector2 cityPositon, desertPosition;
   
   
    Transform player;
    bool canTravel;

    ScenManager sceneManager;
    NPCConversation conversation;
    // Start is called before the first frame update
    void Start()
    {
         sceneManager = GameObject.Find("Game Manager").GetComponent<ScenManager>();
         player = GameObject.FindGameObjectWithTag("Player").transform;
         conversation = GetComponent<NPCConversation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTravel && Input.GetKeyDown(KeyCode.E) )
        {
            ConversationManager.Instance.StartConversation(conversation);

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0: ConversationManager.Instance.SetBool("City", false); ConversationManager.Instance.SetBool("Desert", true); isFastTravel = true;  break;

                case 1: ConversationManager.Instance.SetBool("Desert", false); ConversationManager.Instance.SetBool("City", true); isFastTravel = true; break;

            }
        }
    }
    public void SetPostionCity()
    {
        sceneManager.LoadFirstCity();
        player.transform.position = cityPositon;
    }
    public void SetDesertPosition()
    {
        sceneManager.LoadDesert();
        player.transform.position = desertPosition;
    }


   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           
            canTravel = true;
            notification.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canTravel = false;
        notification.SetActive(false);
    }
}
