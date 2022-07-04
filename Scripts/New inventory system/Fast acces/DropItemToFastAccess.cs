using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropItemToFastAccess : MonoBehaviour, IDropHandler
{
    public enum ChooseAccess
    {
        potions,
        sword,
        armor
    }
    [SerializeField] DropItemToFastAccess dropItemToFastAccess;
    [SerializeField] Sprite basicSprite;
    [SerializeField] AudioClip dropSound;
    public ChooseAccess choose;
    public Item[] itemToUse;
    public int actualUseID;

    private Image poisonImage;
    private int howManyItems;
    FastAccessUi accessUI;
    InventorySystem inventory;
    AudioManager audioManager;
    FightSystem fightSystem;

    public void OnDrop(PointerEventData eventData)
    {
        for (int i = 0; i < itemToUse.Length; i++)
        {
            if (itemToUse[i].ID == eventData.pointerDrag.GetComponent<ShowItemDescription>().itemIdInSlot && choose==ChooseAccess.potions && dropItemToFastAccess.actualUseID != eventData.pointerDrag.GetComponent<ShowItemDescription>().itemIdInSlot)
            {
                gameObject.GetComponent<Image>().sprite = itemToUse[i].image;
                actualUseID = itemToUse[i].ID;
                audioManager.PlayClip(dropSound);

                 accessUI.potionSprite.sprite = itemToUse[i].image;
                 accessUI.name.text = itemToUse[i].objectName;   


            }
            else if(itemToUse[i].ID == eventData.pointerDrag.GetComponent<ShowItemDescription>().itemIdInSlot && choose != ChooseAccess.potions)
            {
                gameObject.GetComponent<Image>().sprite = itemToUse[i].image;
                actualUseID = itemToUse[i].ID;
                audioManager.PlayClip(dropSound);

                switch (choose)
                {
                    case ChooseAccess.armor: fightSystem.armor = itemToUse[i].armor; break;
                    case ChooseAccess.sword: fightSystem.damage = itemToUse[i].damage; break;

                }
            }

        }
        
        if (eventData.pointerDrag.GetComponent<ShowItemDescription>().itemIdInSlot == 4 && choose == ChooseAccess.sword)
        {
            poisonImage.gameObject.GetComponent<PoisonController>().howManyUses = poisonImage.gameObject.GetComponent<PoisonController>().maxUses;
            poisonImage.gameObject.GetComponent<PoisonController>().ChangeUsesValue();

            inventory.ReduceItem(4, 1);
        }
    }

    // Start is called before the first frame upda
    void Start()
    {
        if (ChooseAccess.potions == choose)
            accessUI = GetComponent<FastAccessUi>();

        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        fightSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>();
        poisonImage = GameObject.Find("poison").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
        howManyItems = inventory.HowManyItemsInSlot(actualUseID);

        if(howManyItems<=0)
        {
            gameObject.GetComponent<Image>().sprite = basicSprite;
            actualUseID = 0;

            switch (choose)
            {
                case ChooseAccess.armor: fightSystem.armor = 0;  break;
                case ChooseAccess.sword: fightSystem.damage = 10; break;

            }
        }

    }


    public void Load()
    {

        //howManyItems = inventory.HowManyItemsInSlot(actualUseID);
        for (int i = 0; i < itemToUse.Length; i++)
        {
            if(actualUseID==itemToUse[i].ID)
            {
            gameObject.GetComponent<Image>().sprite = itemToUse[i].image;

                switch (choose)
                {
                    case ChooseAccess.armor: fightSystem.armor = itemToUse[i].armor; break;
                    case ChooseAccess.sword: fightSystem.damage = itemToUse[i].damage; break;
                    case ChooseAccess.potions: accessUI.potionSprite.sprite = itemToUse[i].image; accessUI.name.text = itemToUse[i].objectName; break;

                }
              

            }
        }
       
    }

   
}
