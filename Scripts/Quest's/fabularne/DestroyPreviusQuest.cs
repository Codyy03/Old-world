using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPreviusQuest : MonoBehaviour
{
    [SerializeField] Quest[] fabularQuests;
    [SerializeField] GameObject[] questPrefabs;
    [SerializeField] GameObject questBackgorund;
    // Start is called before the first frame update
    bool questWasSend; 
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        SendQuest();
    }

    void SendQuest()
    {
        for (int i = 0; i < fabularQuests.Length; i++)
        {
            if (!questWasSend && fabularQuests[i].questAccepted == 1 && !fabularQuests[i].exitQuest)
            {
                Instantiate(questPrefabs[i], questBackgorund.transform.GetChild(0));
                questWasSend = true;
              
                return;
            }
        }
      
    }

    public void DestroyOldQuest()
    {
        GameObject fabularQuest;
        fabularQuest = questBackgorund.transform.GetChild(0).gameObject;

        Destroy(fabularQuest.transform.GetChild(1).gameObject);

        questWasSend = false;


    }
}
