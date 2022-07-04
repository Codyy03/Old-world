using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SetArmorToSlot : MonoBehaviour,IDropHandler
{
    [SerializeField] int[] armorsID;
    [SerializeField] AudioClip dragSound;
    public int actualID;

    public int saveID;

    FightSystem fightSystem;
    ItemHolder itemHolder;
    AudioManager audioManager;



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
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
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        itemHolder = GameObject.Find("Item Holder").GetComponent<ItemHolder>();
        fightSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>();

        for (int i = 0; i < itemHolder.armor.Count; i++)
        {
            armorsID[i] = itemHolder.armor[i].ID;
        }


    }

    public void LoadFastAccess()
    {

        actualID = saveID;
        SetStart();
        for (int i = 0; i < armorsID.Length; i++)
        {

            if (actualID == armorsID[i])
            {
                gameObject.GetComponent<Image>().sprite = itemHolder.armor[i].image;
                fightSystem.armor = itemHolder.armor[i].armor;
            }

        }

    }

}
