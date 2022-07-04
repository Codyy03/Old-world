using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    
    [SerializeField] DropItemToFastAccess drop1, drop2,drop3,drop4;
    [SerializeField] DisplayOffert[] offert;
    [SerializeField] Quest[] quests;
    [SerializeField] PoisonController poisonController;

    int[] saveIDFastAccess = new int[4];
    int[] saveOffert = new int[3];
    int[] questAkceptedSave;

    int[] afterTalkWithSomebodyConvertToInt;
    int[] beforExitQuestTalkConvertToInt;
    int[] exitQuestConvertToInt;

    //zapisuje czas jaki pozosta³ do pojawienia siê dodatkowej opcji dialogowej u sprzedawcy eliksirów
    float saveTimeEliksirShop;
    float howManyUsesLeftInPoisonSave;
    ElliksirSellerQuest eliksirSellerQuest;
    InventorySystem inventory;
    private void Awake()
    {
        eliksirSellerQuest = GameObject.Find("Œmiertelne wyzwanie").GetComponent<ElliksirSellerQuest>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        afterTalkWithSomebodyConvertToInt = new int[quests.Length];
        beforExitQuestTalkConvertToInt = new int[quests.Length];
        exitQuestConvertToInt = new int[quests.Length];
        questAkceptedSave = new int[quests.Length];

    }
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        // zapisuje szybki dostep
        saveIDFastAccess[0] = drop1.actualUseID;
        saveIDFastAccess[1] = drop2.actualUseID;
        saveIDFastAccess[2] = drop3.actualUseID;
        saveIDFastAccess[3] = drop4.actualUseID;


        // czas pozostay do odblokowania lini dialogowaej u sprzedawcy eliksirów
        saveTimeEliksirShop = eliksirSellerQuest.timeToWait;

        // ile pozostao uzyc trucizny;
        howManyUsesLeftInPoisonSave = poisonController.howManyUses;
        for (int i = 0; i < offert.Length; i++)
        {
            //zapisuje dodane zadania
            if (offert[i] != null)
                saveOffert[i] = offert[i].saveValue;

            questAkceptedSave[i] = quests[i].questAccepted;

            if (quests[i].afterTalkWithSomebody)
                afterTalkWithSomebodyConvertToInt[i] = 1;
            else afterTalkWithSomebodyConvertToInt[i] = 0;

            if (quests[i].beforeExitQuestTalk)
                beforExitQuestTalkConvertToInt[i] = 1;
            else beforExitQuestTalkConvertToInt[i] = 0;

            if (quests[i].exitQuest)
                exitQuestConvertToInt[i] = 1;
            else exitQuestConvertToInt[i] = 0;
        }
    

        if (Input.GetKeyDown(KeyCode.F5))
        {
            inventory.SaveInventory();
            //zapisuje szybki dostep
            PlayerPrefs.SetInt("drop1", saveIDFastAccess[0]);
            PlayerPrefs.SetInt("drop2", saveIDFastAccess[1]);
            PlayerPrefs.SetInt("drop3", saveIDFastAccess[2]);
            PlayerPrefs.SetInt("drop4", saveIDFastAccess[3]);

            //zapisuje ile czasu zosta³o do odblokowania lini dialogowej u sprzedawcy eliksirów
            PlayerPrefs.SetFloat("timeToUnlock", saveTimeEliksirShop);


            // zapisuje ile u¿yæ trucizny zosta³o
            PlayerPrefs.SetFloat("howManyPoisonUsesLeft", howManyUsesLeftInPoisonSave);


            SaveQuest();
            PlayerPrefs.Save();

        }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                inventory.LoadInventory();

                drop1.actualUseID = PlayerPrefs.GetInt("drop1");

                drop2.actualUseID = PlayerPrefs.GetInt("drop2");

                drop3.actualUseID = PlayerPrefs.GetInt("drop3");

                drop4.actualUseID = PlayerPrefs.GetInt("drop4");

                eliksirSellerQuest.timeToWait = PlayerPrefs.GetFloat("timeToUnlock");

             // wczytuje ile u¿yæ trucizny zosta³o
               poisonController.howManyUses = PlayerPrefs.GetFloat("howManyPoisonUsesLeft");
               poisonController.ChangeUsesValue();

                drop1.Load();
                drop2.Load();
                drop3.Load();
                drop4.Load();

       

            LoadQuest();
            }
        

    }

    public void SaveQuest()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            PlayerPrefs.SetInt("afterTalkWithSomebody" + i, afterTalkWithSomebodyConvertToInt[i]);
            PlayerPrefs.SetInt("beforExitQuest" + i, beforExitQuestTalkConvertToInt[i]);
            PlayerPrefs.SetInt("exitQuest" + i, exitQuestConvertToInt[i]);

            //zapisuje zadania
            PlayerPrefs.SetInt("questAkceptedSave" + i, questAkceptedSave[i]);
        }


        
    }

    public void LoadQuest()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].questAccepted = PlayerPrefs.GetInt("questAkceptedSave" + i);

            //odczytuje zadania
            if(offert[i]!=null)
            {
            
            offert[i].saveValue = PlayerPrefs.GetInt("questAkceptedSave" + i);
            offert[i].LoadOffert();

            }
         

            afterTalkWithSomebodyConvertToInt[i] = PlayerPrefs.GetInt("afterTalkWithSomebody" + i);
            //after
            if (afterTalkWithSomebodyConvertToInt[i] == 1)
            {
                quests[i].afterTalkWithSomebody = true;
            }
            else quests[i].afterTalkWithSomebody = false;


            //befor
            beforExitQuestTalkConvertToInt[i] = PlayerPrefs.GetInt("beforExitQuest" + i);
            if (beforExitQuestTalkConvertToInt[i] == 1)
            {

                quests[i].beforeExitQuestTalk = true;
            }
            else quests[i].beforeExitQuestTalk = false;


            //exit quest
            exitQuestConvertToInt[i] = PlayerPrefs.GetInt("exitQuest" + i);
            if (exitQuestConvertToInt[i] == 1)
            {

                quests[i].exitQuest = true;
            }
            else quests[i].exitQuest = false;
        }



       
    }
}
