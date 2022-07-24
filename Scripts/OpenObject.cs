using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour
{
    public static bool objectIsOpen;
    [SerializeField] GameObject objectToOpen,optionalNotification;

    [SerializeField] bool stopTime;

    bool canBeOpen, isOpen;
   
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeOpen && Input.GetKeyDown(KeyCode.E) &&!isOpen)
        {
            objectToOpen.SetActive(true);
            isOpen = true;
            objectIsOpen = true;
            if (stopTime)
            {
                gameManager.inventoryIsOpen = true;
                
            }
        }
        else if(isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            objectToOpen.SetActive(false);
            objectIsOpen = false;
            isOpen = false;
            if (stopTime)
            {
                gameManager.inventoryIsOpen = false;

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          
            canBeOpen = true;
            if (optionalNotification != null)
                optionalNotification.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectIsOpen = false;
        objectToOpen.SetActive(false);
        isOpen = false;
        canBeOpen = false;
        if (optionalNotification != null)
            optionalNotification.SetActive(false);
    }
}
