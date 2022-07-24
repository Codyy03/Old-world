using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class InputConverstation : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ConversationManager.Instance!= null && ConversationManager.Instance.IsConversationActive)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
                ConversationManager.Instance.SelectNextOption();

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                ConversationManager.Instance.SelectPreviousOption();
         
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                ConversationManager.Instance.PressSelectedOption();


        }


    }
}
