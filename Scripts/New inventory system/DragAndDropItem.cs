using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] AudioClip dragSound;
    RectTransform rect, frame;
    CanvasGroup canvasGroup;

    AudioManager audioManager;
    ShowItemDescription showDescription;
    public void OnBeginDrag(PointerEventData eventData)
    {
       
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha=0.6f;
        audioManager.PlayClip(dragSound);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta;
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
       
        canvasGroup.alpha = 1f;
        if (eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = frame.localPosition;
        canvasGroup.blocksRaycasts = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        frame = transform.parent.GetChild(0).GetComponent<RectTransform>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        showDescription = GetComponent<ShowItemDescription>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
