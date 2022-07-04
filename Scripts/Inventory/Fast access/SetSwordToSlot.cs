using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SetSwordToSlot : MonoBehaviour, IDropHandler
{
   
   [SerializeField] int[] swordsID;
   [SerializeField] AudioClip dragSound;
    public int actualID;

    public int saveID;

    FightSystem fightSystem;
    ItemHolder itemHolder;
    AudioManager audioManager;



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null )
        {
            saveID = actualID;
            LoadFastAccess();
            audioManager.PlayClip(dragSound);
        }
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
        fightSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        for (int i = 0; i < itemHolder.swords.Count; i++)
        {
            swordsID[i] = itemHolder.swords[i].ID;
        }

       
    }

    public void LoadFastAccess()
    {

        actualID = saveID;
        SetStart();
        for (int i = 0; i < swordsID.Length; i++)
        {

            if (actualID == swordsID[i])
            {
                gameObject.GetComponent<Image>().sprite = itemHolder.swords[i].image;
                fightSystem.damage = itemHolder.swords[i].damage;
            }

        }
       
    }


}
