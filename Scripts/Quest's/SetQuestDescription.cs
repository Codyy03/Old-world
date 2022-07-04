using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetQuestDescription : MonoBehaviour
{
    [SerializeField] Quest quest;
    [TextArea]
    [SerializeField] string afterTalk;
    [TextArea]
    [SerializeField] string exitConversation;
    void Start()
    {
        
    }

    // Update is called once per fr
    void Update()
    {
        
    }

    public void AfterTalk()
    {
        quest .description+= afterTalk;
    }
    public void ExitTalk()
    {
        quest.description += exitConversation;
    }

}
