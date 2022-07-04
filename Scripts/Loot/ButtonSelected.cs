using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonSelected : MonoBehaviour
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
           
        }
        else this.transform.GetChild(1).gameObject.SetActive(false);

     
    }
}
