using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
public class InventoryManager : MonoBehaviour
{


    public List<GameObject> slots;
    public List<GameObject> childsSlot;
    public List<TextMeshProUGUI> amountText;

    public List<int> slotID;
    
    public List<int> slotAmount;

    public List<bool> slotIsOccupied;
    

    public TextMeshProUGUI goldValueText;
    public int playerGold;



    public List<GameObject> inventoryObject;

   
  
    string saveSaparator = "##";
    private void Awake()
    {
       
    }
    void Start()
    {
        for (int i = 0; i < amountText.Count; i++)
        {
            
            amountText[i] = slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slotAmount[i] = Mathf.Clamp(slotAmount[i], 0, 999);
            amountText[i].text = "" + slotAmount[i];
            if(!slotIsOccupied[i])
            {
                slotID[i] = 0;
                slotAmount[i] = 0;
            }
        }
        goldValueText.text = "" + playerGold;

        amountText[2].text = "" + slotAmount[2];
        amountText[8].text = "" + slotAmount[8];

    }

    public void CreateShortcut(GameObject prefab, int ID)
    {
        for (int i = 0; i < slots.Count; i++)
        {

            if (slotIsOccupied[i] && ID == slotID[i] )
            {
                slotAmount[i]++;
                return;
            }
        
        }
        CreateItem(prefab, ID);
    }

    void CreateItem(GameObject prefab, int ID)
    {

        for (int i = 0; i < slots.Count; i++)
        {
            if (!slotIsOccupied[i] && ID != slotID[i])
            {
                slotAmount[i]++;
               
                slotID[i] = ID;

                Instantiate(prefab, slots[i].transform);
                childsSlot[i] = slots[i].transform.GetChild(2).gameObject;

                slotIsOccupied[i] = true;
                break;

            }
        }

        }

    public bool IsItemInInventory(int id)
    {
        int counter = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slotID[i] == id)
                counter++;

        }

        if (counter == 0)
            return false;
        else return true;
    }


    public void ReduceItem(int id)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slotIsOccupied[i] && id == slotID[i])
            {
                slotAmount[i]--;
                if(slotAmount[i]<=0)
                {
                    Destroy(slots[i].transform.GetChild(2).gameObject);
                    slotID[i] = 0;
                    slotIsOccupied[i] = false;
                    return;
                }
                    
            }
        }
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
                ""+slotID[25],
                ""+slotID[26],
                 ""+slotID[27],
                  



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
                    ""+slotAmount[25],
                    ""+slotAmount[26],
                    ""+slotAmount[27],
                  





                    ""+playerGold,
                   
        };
        string saveString = string.Join(saveSaparator, constents);

        File.WriteAllText(Application.dataPath + "/save.txt", saveString);

    }


  public  void LoadInventory()
    {
     
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

        string[] contents = saveString.Split(new[] { saveSaparator }, System.StringSplitOptions.None);

        for (int i = 0; i < slotID.Count; i++)
        {
            
            MakeInventoryFull(int.Parse(contents[i]));
            
            slotID[i] = int.Parse(contents[i]);
           
            slotAmount[i] = int.Parse(contents[i + 28]);
          
        }

        
        playerGold = int.Parse(contents[slotID.Count*2 ]);

      
    }

        void MakeInventoryFull( int id)
    {
        switch (id)
        {
            //0
            case 101: CreateShortcut(inventoryObject[0], id); break;
            //1
            case 100: CreateShortcut(inventoryObject[1], id); break;
            //2
            case 200: CreateShortcut(inventoryObject[2], id); break;
            //3
            case 1: CreateShortcut(inventoryObject[3], id); break;
            //4
            case 2: CreateShortcut(inventoryObject[4], id); break;
            //5
            case 10: CreateShortcut(inventoryObject[5], id); break;
            //6
            case 3: CreateShortcut(inventoryObject[6], id); break;
             //7
           case 11: CreateShortcut(inventoryObject[7], id); break;
            //8
            case 12: CreateShortcut(inventoryObject[8], id); break;
            // 9
            case 201: CreateShortcut(inventoryObject[9], id); break;
            //10
            case 202: CreateShortcut(inventoryObject[10], id); break;
            //11
            case 203: CreateShortcut(inventoryObject[11], id); break;
            //12
            case 204: CreateShortcut(inventoryObject[12], id); break;
            // 13
            case 205: CreateShortcut(inventoryObject[13], id); break;
        }
    }

}



