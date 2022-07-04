using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] AudioClip equipSound;
    int nativSlot, slotToChange, id;

    InventorySystem inventory;
    ShowItemDescription showDescription;
    AudioManager audioManager;
    public void OnDrop(PointerEventData eventData)
    {
       
        if (eventData.pointerDrag != null && gameObject.GetComponent<ShowItemDescription>().itemIdInSlot==0)
        {
            // slot z którego bierze siê przedmiot
            nativSlot = eventData.pointerDrag.GetComponent<ShowItemDescription>().slotNumber;
            // slot do którego daje siê przedmiot
            slotToChange = gameObject.GetComponent<ShowItemDescription>().slotNumber;
            // id przedmiotu podniesionego
            id = eventData.pointerDrag.GetComponent<ShowItemDescription>().itemIdInSlot;

            inventory.ChangeItemSlot(nativSlot, slotToChange, id);
            audioManager.PlayClip(equipSound);
        }
            
      
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        showDescription = GetComponent<ShowItemDescription>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
