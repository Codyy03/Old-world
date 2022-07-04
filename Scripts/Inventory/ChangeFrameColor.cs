using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ChangeFrameColor : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
            transform.GetChild(0).GetComponent<Image>().color = Color.red;
        
        
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
            transform.GetChild(0).GetComponent<Image>().color = Color.white;
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
