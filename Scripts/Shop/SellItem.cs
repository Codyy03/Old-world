using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SellItem : MonoBehaviour,IPointerClickHandler
{

    [SerializeField] AudioClip sellSound;

    ShowItemDescription itemDescription;
    InventorySystem inventoryManager;
    AudioManager audioManager;
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if(OpenShop.shopIsOpen && itemDescription.itemIdInSlot!=0)
        {
            
            inventoryManager.playerGold += inventoryManager.FoundItem(itemDescription.itemIdInSlot).value;
            inventoryManager.ReduceItem(itemDescription.itemIdInSlot,1);
            audioManager.PlayClip(sellSound);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      
        itemDescription = GetComponent<ShowItemDescription>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
