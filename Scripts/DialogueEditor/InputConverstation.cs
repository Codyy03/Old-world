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

            if (Input.GetKeyDown(KeyCode.UpArrow))
                ConversationManager.Instance.SelectNextOption();

            if (Input.GetKeyDown(KeyCode.DownArrow))
                ConversationManager.Instance.SelectPreviousOption();
         
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
                ConversationManager.Instance.PressSelectedOption();


        }


    }
}
