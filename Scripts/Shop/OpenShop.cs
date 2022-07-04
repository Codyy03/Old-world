using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class OpenShop : MonoBehaviour
{
    public static bool shopIsOpen;
    [SerializeField] GameObject notification, shopCanvas;

    bool open;
    NPCConversation conversation;
    InventorySystem inventoryManager;
    private void Awake()
    {
      

    }
    void Start()
    {
         conversation = GetComponent<NPCConversation>();
         inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
       
    }

    // Update is called once per fra
    void Update()
    {
       
        if (open)
            SetActive(notification, true);

        else SetActive(notification, false);



        if (open && Input.GetKeyDown(KeyCode.E))
        {
            ConversationManager.Instance.StartConversation(conversation);
            
        }
       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableShop();
        }

    

    }
 

    public void Open()
    {
        SetActive(shopCanvas, true);

       GlobalComponents.fastAccess.GetComponent<RectTransform>().localPosition = new Vector3(-288f, 0, 0);

        shopIsOpen = true;

        SetActive(GlobalComponents.inventory, true);
    }
   public void DisableShop()
    {
        SetActive(shopCanvas, false);
        GlobalComponents.fastAccess.GetComponent<RectTransform>().localPosition = new Vector3(562.5f, 2.1f, 0);
        shopIsOpen = false;
       
        SetActive(GlobalComponents.inventory, false);

    }

    void SetActive(GameObject o,bool how)
    {
        o.SetActive(how);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        open = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shopIsOpen = false;
        open = false;
    }
}
