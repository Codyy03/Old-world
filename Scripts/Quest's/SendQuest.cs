using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendQuest : MonoBehaviour
{
    [SerializeField] GameObject scroolArea;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendQuestToCanvas(GameObject questPrefab)
    {
        Instantiate(questPrefab, scroolArea.transform.GetChild(0));
    }
}
