using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
public class InventorySystem : MonoBehaviour
{
    [SerializeField] Sprite nullSprite;
    [SerializeField] TextMeshProUGUI playerGoldText;
    
    public int playerGold;

    public List<GameObject> slots;

    public List<Item> items;

    public List<int> slotID;

    public List<int> slotAmount;

    public List<bool> slotIsOccupied;

    List<TextMeshProUGUI> amountText = new List<TextMeshProUGUI>();
    List<ShowItemDescription> itemsDescription = new List<ShowItemDescription>();

    string saveSaparator = "##";
    // Start is called before the first frame update
    void Start()
    {
       
        for (int i = 0; i < slots.Count; i++)
        {
            itemsDescription.Add(slots[i].transform.GetChild(2).GetComponent<ShowItemDescription>());

            amountText.Add(slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>());

            amountText[i] = slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < items.Count; i++)
            {
                CreateShortcut( items[i].ID);
            }
        }

        playerGoldText.text = "" + playerGold;


        for (int i = 0; i < slots.Count; i++)
        {
            slotAmount[i] = Mathf.Clamp(slotAmount[i], 0, 999);
            amountText[i].text = "" + slotAmount[i];
            itemsDescription[i].itemIdInSlot = slotID[i];

            itemsDescription[i].slotNumber = i;
           
        }
    }

    // void który sprawdza czy przedmiot w ekwipunku instnieje. Je¿eli tak dodaje kolejny
    public void CreateShortcut(int ID)
    {
        for (int i = 0; i < slotID.Count; i++)
        {

            if (slotIsOccupied[i] && ID == slotID[i] && slotID[i]!=0)
            {
                slotAmount[i]++;
               
                return;
            }

        }
        CreateItem(ID);
    }

    // void który tworzy przedmiot je¿eli nie ma go jeszcze w ekwipunku
    void CreateItem(int ID)
    {
        for (int i = 0; i < slotID.Count; i++)
        {
            if (!slotIsOccupied[i] && ID != slotID[i])
            {
                slotID[i] = ID;
                slotIsOccupied[i] = true;

                for (int j = 0; j < items.Count; j++)
                {
                    if (slotID[i] == items[j].ID)
                        slots[i].transform.GetChild(2).GetComponent<Image>().sprite = items[j].image;
                }
                slotAmount[i]++;

                return;

            }

        
        }
    }

    public void ChangeItemSlot(int nativSlot, int slotToChangeNumber, int ID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].ID==ID)
            slots[slotToChangeNumber].transform.GetChild(2).GetComponent<Image>().sprite = items[i].image;
        }
        // ustawia opcje slotu do którego dane przedmiot przenosimy
        slotID[slotToChangeNumber] = ID;
        slotAmount[slotToChangeNumber] = slotAmount[nativSlot];
        slotIsOccupied[slotToChangeNumber] = true;

        //resetuje opcje slotu z któego przedmiot zosta³ przeniesiony
        slotID[nativSlot] = 0;
        slotAmount[nativSlot] = 0;
        slotIsOccupied[nativSlot] = false;
        slots[nativSlot].transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
    }
    // void który usuwa wszyskie przedmioty danego typu
    public void DeleteItem(int ID)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if(slotID[i]==ID)
            {
                slotIsOccupied[i] = false;
                slotAmount[i] = 0;
                slotID[i] = 0;
                slots[i].transform.GetChild(2).GetComponent<Image>().sprite = nullSprite;
            }
        }
    }
    // usuwa okreœlon¹ iloœæ przedmiotów z ekwipunku
    public void ReduceItem(int ID, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if(slotID[i] == ID && slotAmount[i]>=amount)
            {
                slotAmount[i] -= amount;
                if (slotAmount[i] == 0)
                    DeleteItem(ID);
            }
        }
    }
    public bool IsItemInInventory(int id)
    {
        bool isItem= false;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slotID[i]==id)
            {
                isItem = true;
                break;
            }
            else isItem = false;
            
        }
        return isItem;
    }

    public int HowManyItemsInSlot(int ID)
    {
        int value = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slotID[i] == ID)
                value= slotAmount[i];
        }
        return value;
    }

    public Item FoundItem(int ID)
    {
        Item it = null;
        for (int i = 0; i < items.Count; i++)
        {
            if (ID == items[i].ID)
                it = items[i];
        }
        return it;
    }

    public void SaveInventory()
    {

        string[] constents = new string[] {
            ""+slotID[0],
           ""+slotID[1],
            ""+slotID[2],
            ""+slotID[3],
            ""+slotID[4],
             ""+slotID[5],
             ""+slotID[6],
             ""+slotID[7],
             ""+slotID[8],
             ""+slotID[9],
             ""+slotID[10],
              ""+slotID[11],
              ""+slotID[12],
               ""+slotID[13],
               ""+slotID[14],
                ""+slotID[15],
                 ""+slotID[16],
                 ""+slotID[17],
                 ""+slotID[18],
                 ""+slotID[19],
                 ""+slotID[20],
                 ""+slotID[21],
                 ""+slotID[22],
                ""+slotID[23],
                ""+slotID[24],
              



                    ""+slotAmount[0],
                    ""+slotAmount[1],
                    ""+slotAmount[2],
                    ""+slotAmount[3],
                    ""+slotAmount[4],
                    ""+slotAmount[5],
                    ""+slotAmount[6],
                    ""+slotAmount[7],
                    ""+slotAmount[8],
                     ""+slotAmount[9],
                    ""+slotAmount[10],
                    ""+slotAmount[11],
                    ""+slotAmount[12],
                    ""+slotAmount[13],
                    ""+slotAmount[14],
                    ""+slotAmount[15],
                    ""+slotAmount[16],
                    ""+slotAmount[17],
                     ""+slotAmount[18],
                    ""+slotAmount[19],
                    ""+slotAmount[20],
                    ""+slotAmount[21],
                    ""+slotAmount[22],
                    ""+slotAmount[23],
                    ""+slotAmount[24],

                 


                    ""+playerGold,

        };
        string saveString = string.Join(saveSaparator, constents);

        File.WriteAllText(Application.dataPath + "/save.txt", saveString);

    }


    public void LoadInventory()
    {

        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

        string[] contents = saveString.Split(new[] { saveSaparator }, System.StringSplitOptions.None);

        for (int i = 0; i < slotID.Count; i++)
        {

             slotID[i] = int.Parse(contents[i]);
            for (int j = 0; j < items.Count; j++)
            {
                if (slotID[i] == items[j].ID)
                    slots[i].transform.GetChild(2).GetComponent<Image>().sprite = items[j].image;
            }
           

            slotAmount[i] = int.Parse(contents[i + 25]);
          
            

        }


        playerGold = int.Parse(contents[slotID.Count * 2]);


    }



}
