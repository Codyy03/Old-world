using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_UI : MonoBehaviour
{
    public static Player_UI instance { get; private set; }


    [SerializeField] Sprite nullSprite,potion;
    [SerializeField] AudioClip drinkSound;
    [SerializeField]  DropItemToFastAccess dropItem1, dropItem2;
    [SerializeField] FastAccessUi fastAccessUi1, fastAccessUi2;

    Image healthBar;
    int howManyUsesR=0, howManyUsesF=0;
    

    
    InventorySystem inventory;
    PlayerHealth playerHealth;
    AudioManager audioManager;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();

        healthBar = GameObject.Find("Health Bar player").GetComponent<Image>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
    }

    // Update is called once per
    void Update()
    {
       
        // sprawdza ile jest mikstur pod slotem 1 (R)
        if (dropItem1.actualUseID != 0)
            howManyUsesR = inventory.HowManyItemsInSlot(dropItem1.actualUseID);

        
        // sprawdza ile jest mikstur pod slotem 2 (F)
        if (dropItem2.actualUseID != 0)
            howManyUsesF = inventory.HowManyItemsInSlot(dropItem2.actualUseID);


        if (howManyUsesR >= 1 && Input.GetKeyDown(KeyCode.R))
        {
            playerHealth.ChangeHealth(inventory.FoundItem(dropItem1.actualUseID).health);
            inventory.ReduceItem(dropItem1.actualUseID, 1);
            audioManager.PlayClip(drinkSound);
           
        }
        if (inventory.HowManyItemsInSlot(dropItem1.actualUseID) <= 0)
        {
            fastAccessUi1.name.text = null;
            fastAccessUi1.potionSprite.sprite = nullSprite;
        }


        if (howManyUsesF >= 1 && Input.GetKeyDown(KeyCode.F))
        {
            playerHealth.ChangeHealth(inventory.FoundItem(dropItem2.actualUseID).health);
            inventory.ReduceItem(dropItem2.actualUseID, 1);
            audioManager.PlayClip(drinkSound);
            
        }
        if (inventory.HowManyItemsInSlot(dropItem2.actualUseID) <= 0)
        {
            fastAccessUi2.name.text = null;
            fastAccessUi2.potionSprite.sprite = nullSprite;
        }


    }
    public void ChangeHealth(float value)
    {
       
        healthBar.fillAmount = value;

    }
   
}
