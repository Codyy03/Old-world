using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryObject;  
    [SerializeField] DropItemToFastAccess drop1, drop2,drop3,drop4;
    [SerializeField] DisplayOffert[] offert;
    [SerializeField] Quest[] quests;
    [SerializeField] CarrierSideQuset carrierSideQuest;
    [SerializeField] PoisonController poisonController;
 
    int[] saveIDFastAccess = new int[4];
    int[] saveOffert = new int[3];
    int[] questAkceptedSave;

    // zapisuje dane do zadañ
    int[] afterTalkWithSomebodyConvertToInt;
    int[] beforExitQuestTalkConvertToInt;
    int[] exitQuestConvertToInt;
    int[] bonusConvertToInt;
    int hasRewordCarrierSideQuestInt;
    //zapisuje czas jaki pozosta³ do pojawienia siê dodatkowej opcji dialogowej u sprzedawcy eliksirów
    float saveTimeEliksirShop;
    float howManyUsesLeftInPoisonSave;
    ElliksirSellerQuest eliksirSellerQuest;


    //zapisuje dane gracza
    GameObject player;
    PlayerHealth playerHealth;
    float[] playerPosition;
    float savePlayerHealth;
    int scenSave;
    float playTimeSave;

    //reszta komponentów
    InventorySystem inventory;
    ScenManager scenManger;
    EnemyToDestroy enemyToDestroy;
    GameManager gameManager;
    private void Awake()
    {
        eliksirSellerQuest = GameObject.Find("Œmiertelne wyzwanie").GetComponent<ElliksirSellerQuest>();


        inventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        scenManger = GameObject.Find("Game Manager").GetComponent<ScenManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        afterTalkWithSomebodyConvertToInt = new int[quests.Length];
        beforExitQuestTalkConvertToInt = new int[quests.Length];
        exitQuestConvertToInt = new int[quests.Length];
        questAkceptedSave = new int[quests.Length];
        bonusConvertToInt = new int[quests.Length];

        playerPosition = new float[2];
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        enemyToDestroy = GetComponent<EnemyToDestroy>();

      
    }
    void Start()
    {
       

    }

    // Update is called once per fra
    void Update()
    {
        if (LoadingManager.isLoading)
        {
            LoadingManager.isLoading = false;
            
            Load();
        }
        // zapisuje szybki dostep
        saveIDFastAccess[0] = drop1.actualUseID;
        saveIDFastAccess[1] = drop2.actualUseID;
        saveIDFastAccess[2] = drop3.actualUseID;
        saveIDFastAccess[3] = drop4.actualUseID;

        if (carrierSideQuest.hasReword)
            hasRewordCarrierSideQuestInt = 1;
        else hasRewordCarrierSideQuestInt = 0;
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

            if (quests[i].bonus)
                bonusConvertToInt[i] = 1;
            else bonusConvertToInt[i] = 0;
        }


        // dane gracza
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        savePlayerHealth = playerHealth.health;
        scenSave = SceneManager.GetActiveScene().buildIndex;
        playTimeSave = gameManager.playTime;

        if (Input.GetKeyDown(KeyCode.F5) && !PlayerHealth.isDead)
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
           
            Load();
          
        }
        

    }
    public void Save()
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

        enemyToDestroy.SaveEnemy();
        SaveQuest();
        SavePlayerVariables();
        PlayerPrefs.Save();

        LoadingManager.isLoading = false;
    }
    public void OpenLoadScene()
    {
       
        SceneManager.LoadScene(2);
    }
    public void Load()
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
        LoadPlayerVaribales();
        enemyToDestroy.LoadEnemy();
        Time.timeScale = 1;
        //inventoryObject.SetActive(false);
    }
    void SavePlayerVariables()
    {
        PlayerPrefs.SetFloat("PlayerXPosition", playerPosition[0]);
        PlayerPrefs.SetFloat("PlayerYPosition", playerPosition[1]);
        PlayerPrefs.SetFloat("savePlayerHealth", savePlayerHealth);
        PlayerPrefs.SetInt("Scen", scenSave);
        PlayerPrefs.SetFloat("playTimeSave", playTimeSave);
    }
    void LoadPlayerVaribales()
    {
        switch (PlayerPrefs.GetInt("Scen"))
        {
            case 0: scenManger.LoadFirstCity(); break;
            case 1: scenManger.LoadDesert(); break;
            case 3: scenManger.LoadCementary(); break;
            case 4: scenManger.LoadGreenLand(); break;
            case 5: scenManger.LoadGothicvania(); break;
        }

        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerXPosition"), PlayerPrefs.GetFloat("PlayerYPosition") , 0);
        playerHealth.health = PlayerPrefs.GetFloat("savePlayerHealth");
        gameManager.playTime = PlayerPrefs.GetFloat("playTimeSave");
        playerHealth.ChangeHealth(0);
    }

    public void SaveQuest()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            PlayerPrefs.SetInt("afterTalkWithSomebody" + i, afterTalkWithSomebodyConvertToInt[i]);
            PlayerPrefs.SetInt("beforExitQuest" + i, beforExitQuestTalkConvertToInt[i]);
            PlayerPrefs.SetInt("exitQuest" + i, exitQuestConvertToInt[i]);
            PlayerPrefs.SetInt("bonus" + i, bonusConvertToInt[i]);
            //zapisuje zadania
            PlayerPrefs.SetInt("questAkceptedSave" + i, questAkceptedSave[i]);
        }
        PlayerPrefs.SetInt("HasRewordCarrierSideQuest", hasRewordCarrierSideQuestInt);

        
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


            //bonus 
            bonusConvertToInt[i] = PlayerPrefs.GetInt("exitQuest" + i);
            if (bonusConvertToInt[i] == 1)
            {

                quests[i].bonus = true;
            }
            else quests[i].bonus = false;
        }

        hasRewordCarrierSideQuestInt = PlayerPrefs.GetInt("HasRewordCarrierSideQuest");
        if (hasRewordCarrierSideQuestInt == 1)
            carrierSideQuest.hasReword = true;
        else carrierSideQuest.hasReword = false;





    }
}
