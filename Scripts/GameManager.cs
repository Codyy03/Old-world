using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject inventory, playerUI, questPanel, menu;
    
    public bool inventoryIsOpen, menuIsOpen;
    bool questPanelIsOpen;
    void Start()
    {
        
    }

    // Update is called oncerame
    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive)
            return;

        OpenMenu();
        OpenQuestPanel();
        OpenInventory();

    }
    void OpenQuestPanel()
    {
        if (menuIsOpen || OpenObject.objectIsOpen)
            return;
        if (!questPanelIsOpen && Input.GetKeyDown(KeyCode.J))
        {
            SetActive(questPanel, true);
            SetActive(playerUI, false);
            
            questPanelIsOpen = true;
        } else if(questPanelIsOpen && Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(questPanel, false);
            SetActive(playerUI, true);
            
            questPanelIsOpen = false;
        }
    }

  

    void OpenInventory()
    {

        if (menuIsOpen || OpenObject.objectIsOpen)
            return;

        if (!inventoryIsOpen && Input.GetKeyDown(KeyCode.I) & !questPanelIsOpen )
        {

            SetActive(inventory, true);
            SetActive(playerUI, false);

            inventoryIsOpen = true;

        }
        else if (inventoryIsOpen && (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)))
        {
            SetActive(inventory, false);
            SetActive(playerUI, true);

            inventoryIsOpen = false;
        }
    }

    void OpenMenu()
    {
        if (OpenObject.objectIsOpen)
            return;

        if(Input.GetKeyDown(KeyCode.Escape) && !inventoryIsOpen && !questPanelIsOpen && !menuIsOpen)
        {
            menu.SetActive(true);
            menuIsOpen = true;
            playerUI.SetActive(false);
            SetTime(0);
        } 
        else if( Input.GetKeyDown(KeyCode.Escape) && menuIsOpen)
        {
            menu.SetActive(false);
            menuIsOpen = false;
            playerUI.SetActive(true);
            SetTime(1);
        }
    }
    public void DisableMenu()
    {
        menu.SetActive(false);
        menuIsOpen = false;
        playerUI.SetActive(true);
        SetTime(1);
    }

    void SetActive(GameObject o, bool how)
    {
        o.SetActive(how);
    }
    public void SetTime(float time)
    {
        Time.timeScale = time;
    }
}
