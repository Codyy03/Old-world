using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuyItem : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] AudioClip buySound;
    [SerializeField] Item thisItem;



    AudioManager audioManager;
    InventorySystem inventory;
    ShowItemDescription showDescription;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory.playerGold >= thisItem.value)
        {
            inventory.CreateShortcut(thisItem.ID);
            inventory.playerGold -= thisItem.value;
            audioManager.PlayClip(buySound);
        }
    }

    // Start is called before the first frame updat
    void Start()
    {
        
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        showDescription = GetComponent<ShowItemDescription>();
        showDescription.itemIdInSlot = thisItem.ID;
        gameObject.GetComponent<Image>().sprite = thisItem.image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
