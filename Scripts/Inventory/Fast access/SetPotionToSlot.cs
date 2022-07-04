using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class SetPotionToSlot : MonoBehaviour,IDropHandler
{

    [SerializeField] SetPotionToSlot setPosition;
    [SerializeField] AudioClip dragSound;
    public int[] potionsID;
    
    public int actualID;


    public int saveID;
    ItemHolder itemHolder;
    AudioManager audioManager;
    FastAccessUi fastAccess;
    
   
    public void OnDrop(PointerEventData eventData)
    {
      if(eventData.pointerDrag!=null && actualID!= setPosition.saveID)
        {
            saveID = actualID;
            LoadFastAccess();
            audioManager.PlayClip(dragSound);
        }
    }

    void SetFastAccess(Item i)
    {
        fastAccess.name.text = i.objectName;
        fastAccess.potionSprite.sprite = i.image;

       
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStart();

    }

    // Update is called once per frame
    void Update()
    {
     
    }
    // ustawia rzeczy które dziej¹ sie w funkcji start ze wzgledu na to, ¿e trzeba to te¿ zrobiæ w trakcie wczytywania
    void SetStart()
    {
        itemHolder = GameObject.Find("Item Holder").GetComponent<ItemHolder>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        for (int i = 0; i < itemHolder.potions.Count; i++)
        {
            potionsID[i] = itemHolder.potions[i].ID;
        }

        fastAccess = GetComponent<FastAccessUi>();
    }

    public void LoadFastAccess()
    { 
      
        actualID = saveID;
        SetStart();
        for (int i = 0; i < potionsID.Length; i++)
        {
          
            if (actualID == potionsID[i])
            {
                gameObject.GetComponent<Image>().sprite = itemHolder.potions[i].image;
                SetFastAccess(itemHolder.potions[i]);
            }

        }
    }

    

}
