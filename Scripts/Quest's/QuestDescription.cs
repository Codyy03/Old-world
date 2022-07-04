using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class QuestDescription : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Quest quest;

    [TextArea(30,35)] [SerializeField] string[] extraDescription;

    TextMeshProUGUI questName, questDescription;
    public void OnPointerEnter(PointerEventData eventData)
    {
        questName = GameObject.Find("Quest name").GetComponent<TextMeshProUGUI>();
        questDescription= GameObject.Find("Quest description").GetComponent<TextMeshProUGUI>();
        questName.text = quest.name;

        if(!quest.afterTalkWithSomebody && !quest.beforeExitQuestTalk &&!quest.exitQuest)
        questDescription.text = quest.description;

        if (quest.afterTalkWithSomebody && !quest.beforeExitQuestTalk && !quest.exitQuest)
            questDescription.text = quest.description+extraDescription[0];

        if (quest.afterTalkWithSomebody && quest.beforeExitQuestTalk && !quest.exitQuest)
            questDescription.text = quest.description + extraDescription[0] + extraDescription[1];

        if (quest.afterTalkWithSomebody && quest.beforeExitQuestTalk && quest.exitQuest)
            questDescription.text = quest.description + extraDescription[0] + extraDescription[1] + extraDescription[2];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        questName.text = null;
        questDescription.text = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
