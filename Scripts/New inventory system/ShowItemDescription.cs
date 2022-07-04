using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class ShowItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum WhereIsSlot
    {
        inventory,
        shop
    }

    [SerializeField] GameObject descriptionImage;

    public WhereIsSlot whereIsSlot;
    public int itemIdInSlot, slotNumber;
    private GameObject parentGameObject;
    private TextMeshProUGUI itemName, description, value, other;
    InventorySystem inventorySystem;
    //Canvas canvas;
    public void OnPointerEnter(PointerEventData eventData)
    {
        parentGameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        ShowDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        parentGameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        descriptionImage.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        inventorySystem = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        parentGameObject = gameObject.transform.parent.gameObject;

       
        itemName = descriptionImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        description = descriptionImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        value = descriptionImage.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        other = descriptionImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   
    public void ShowDescription()
    {
        
        for (int i = 0; i < inventorySystem.items.Count; i++)
        {
            
            if (itemIdInSlot==inventorySystem.items[i].ID )
            {
                descriptionImage.SetActive(true);

                switch (whereIsSlot)
                { 
                case WhereIsSlot.inventory: descriptionImage.transform.position = inventorySystem.slots[slotNumber].transform.position + new Vector3(320, 0, 0); break;
                case WhereIsSlot.shop: descriptionImage.transform.position = gameObject.transform.position + new Vector3(-320, 0, 0); break;

                }


                itemName.text = inventorySystem.items[i].objectName;
                description.text = inventorySystem.items[i].descripotion;

                if (inventorySystem.items[i].value > 1)
                    value.text = "Wartoœæ przedmiotu: " + inventorySystem.items[i].value;
                else value.text = null;

                if (inventorySystem.items[i].damage > 0)
                other.text ="Obra¿enia broni: " + inventorySystem.items[i].damage;

                if (inventorySystem.items[i].armor > 0)
                    other.text = "Pancerz przedmiotu: " + inventorySystem.items[i].armor;

                if (inventorySystem.items[i].health > 0)
                    other.text = "Mikstura odnawia: " + inventorySystem.items[i].health + " ¿ycia";

                if (inventorySystem.items[i].health <= 0 && inventorySystem.items[i].armor <= 0 && inventorySystem.items[i].damage <= 0)
                    other.text = null;
            }

        }
    }

   


  
}
