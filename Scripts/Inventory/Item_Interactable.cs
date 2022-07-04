using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Item_Interactable : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    RectTransform rectTransform, frame;
    Canvas inventoryCanvas;
    CanvasGroup canvasGroup;
    SetPotionToSlot set1, set2;
    SetSwordToSlot setSword;
    SetArmorToSlot setArmor;
    public void OnBeginDrag(PointerEventData eventData)
    {
        set1.actualID = eventData.pointerDrag.GetComponent<ItemDescription>().item.ID;
        set2.actualID = eventData.pointerDrag.GetComponent<ItemDescription>().item.ID;
        setSword.actualID = eventData.pointerDrag.GetComponent<ItemDescription>().item.ID;
        setArmor.actualID = eventData.pointerDrag.GetComponent<ItemDescription>().item.ID;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor;
    }

   

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        if (eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = frame.localPosition;
        canvasGroup.blocksRaycasts = true;
    }

    // Start is called before the first frame u
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        inventoryCanvas = GameObject.Find("Inventory Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        frame = transform.parent.GetChild(0).GetComponent<RectTransform>();


        set1 = GameObject.Find("Potion placeholder1").GetComponent<SetPotionToSlot>();
        set2 = GameObject.Find("Potion placeholder2").GetComponent<SetPotionToSlot>();
        setSword = GameObject.Find("Sword placeholder").GetComponent<SetSwordToSlot>();
        setArmor = GameObject.Find("Armor placeholder").GetComponent<SetArmorToSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
