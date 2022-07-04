using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class Loot : MonoBehaviour
{
    public static bool lootIsOpen = false;

    public enum LootType { 
    
        lootBag,
        chest
    }
    [SerializeField] List<TextMeshProUGUI> itemNames;
    [SerializeField] AudioClip open, take,close;

    [SerializeField] Sprite goldSprite;
    [SerializeField] bool drawGold, createInOrder;
    [SerializeField] int maxVlaue, minValue,howManyItem;
    [SerializeField] LootType lootType;

    [HideInInspector] public bool isOpen;
    public GameObject lootCanvas, notificationCnavas, contentObject,itemPrefab;
    public List<Item> itemToSpawn;
    //public List<Image> itemSprite;


    int[] drawItemValue;
    bool canBeOpen, tookLoot ;
    bool wasDraw, itemWasDraw;
    int actualGoldValue;
    float distance;
    Transform player;
    AudioManager audioManager;
    InventorySystem inventoryManger;
   
    void Start()
    {
        inventoryManger = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        drawItemValue = new int[itemToSpawn.Count];
    }

    // Update is called onc
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance<=1.3)
        {
            canBeOpen = true;
        } else
        {

            canBeOpen = false;
            isOpen = false;
            lootCanvas.SetActive(false);
            notificationCnavas.SetActive(false);
            lootIsOpen = false;
        }
        // jezeli gracz nie jest dosyæ blisko obiektu nie pozwól przejrzeæ zawartoœci
        if (!canBeOpen)
           return;

        notificationCnavas.SetActive(true);

        // jezeli gracz nie wcisnie E nie wyswietal zawartoœci
        if (!isOpen && Input.GetKeyDown(KeyCode.E) && !tookLoot)
        {
            // pokaz zawartoœæ
            lootCanvas.SetActive(true);
            isOpen = true;
            notificationCnavas.SetActive(false);
            audioManager.PlayClip(open);
            lootIsOpen = true;
            //wylosuj przydmioty
            if (!itemWasDraw && !createInOrder)            
                DrawItem();

            if (!itemWasDraw && createInOrder)
                CreateInOrder();


            //losuj wartoœc z³ota
            if (drawGold && !wasDraw)
            {
                wasDraw = true;
                actualGoldValue = Draw(minValue,maxVlaue);
                inventoryManger.playerGold += actualGoldValue;
                itemPrefab.GetComponent<Image>().sprite = goldSprite;
                itemPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text="Korony: "+ actualGoldValue;
                Instantiate(itemPrefab, contentObject.transform.GetChild(0));

            }
         
        } 
        // jezeli loot jest otwarty i gracz nasisn¹³ E usuñ przedmot i dodaj jego zawartoœæ do ekwipunku
        else if(isOpen && Input.GetKeyDown(KeyCode.E))
        {

            for (int i = 0; i < howManyItem; i++)
            {
                inventoryManger.CreateShortcut(itemToSpawn[drawItemValue[i]].ID);
                
            }
            audioManager.PlayClip(take);
            tookLoot = true;
            lootCanvas.SetActive(false);
            lootIsOpen = false;
            switch (lootType)
            {
                case LootType.lootBag: Destroy(gameObject); break;
            }

          
        }
        else if(isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            audioManager.PlayClip(close);
            isOpen = false;
            lootCanvas.SetActive(false);
            lootIsOpen = false;
        }
      
    }

    int Draw(int min, int max)
    {
        return Random.Range(min, max);
    }
   void CreateInOrder()
    {
        for (int i = 0; i < howManyItem; i++)
        {
            drawItemValue[i] = i;

            itemPrefab.GetComponent<Image>().sprite = itemToSpawn[i].image;
            itemNames[i] = itemPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            itemNames[i].text = itemToSpawn[i].objectName;
            Instantiate(itemPrefab, contentObject.transform.GetChild(0));
        }
        itemWasDraw = true;
    }
    void DrawItem()
    {
        int drawValue;
        for (int i = 0; i < howManyItem; i++)
        {
            drawValue= Draw(0, itemToSpawn.Count);
            drawItemValue[i] = drawValue;
            
            itemPrefab.GetComponent<Image>().sprite = itemToSpawn[drawValue].image;
            itemNames[i] = itemPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            itemNames[i].text = itemToSpawn[drawValue].objectName;
            Instantiate(itemPrefab, contentObject.transform.GetChild(0));

        }
        itemWasDraw = true;

    }

   

}
