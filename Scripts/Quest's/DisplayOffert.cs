using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class DisplayOffert : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler

{
    [SerializeField] Quest quest;
    [SerializeField] bool IsQuest;
    [SerializeField] TextMeshProUGUI offertDescription,nameQuest;

    [SerializeField] GameObject questPrefab, questCanvas;

    [SerializeField] AudioClip bookSound;

    [TextArea]
    [SerializeField] string offert;

    public int saveValue;

    AudioManager audioManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(IsQuest)
        {
        audioManager.PlayClip(bookSound);
        saveValue = 1;
        quest.questAccepted = saveValue;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        offertDescription.text = offert;
        nameQuest.text = quest.questName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        offertDescription.text = null;
        nameQuest.text = null;
    }

    // Start is called before the first frame up
    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        LoadOffert();
    }

    public void LoadOffert()
    {
        if (saveValue == 1)
        {
            Instantiate(questPrefab, questCanvas.transform.GetChild(0));
            offertDescription.text = null;
            nameQuest.text = null;
            Destroy(gameObject);
        }
    }
}


