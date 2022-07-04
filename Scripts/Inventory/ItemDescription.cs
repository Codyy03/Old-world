using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler
{
    public Item item;
    public bool displayDamage,displayArmor;
   
    
    GameObject descriptionObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
            return;
            

        descriptionObject.SetActive(true);
        SetDescription(item);
        
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        descriptionObject = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //wyœwietla opis przedmiotu
    void SetDescription(Item it)
    {
        descriptionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = it.objectName;
        descriptionObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = it.descripotion;
        descriptionObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text ="Cena przedmiotu: "+ it.value;

        if(displayDamage)
         descriptionObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text ="Obra¿enia miecza: "+ it.damage+"per/s";

        if (displayArmor)
            descriptionObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Pancerz: " + it.armor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        descriptionObject.SetActive(false);
    }
}
