using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class TraderPart : MonoBehaviour
{
    [SerializeField] Quest quest;
  
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
      
    }
   public void AfterTalkWithTrader()
    {
        quest.beforeExitQuestTalk = true;
    }
}
