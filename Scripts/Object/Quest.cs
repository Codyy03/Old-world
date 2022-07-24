using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questName;

    [TextArea (10,10)]
    public string description;

    public GameObject[] NPCInQuest;

    public bool  afterTalkWithSomebody,beforeExitQuestTalk,bonus,exitQuest;

    public int reword;

    public int questAccepted;


}
