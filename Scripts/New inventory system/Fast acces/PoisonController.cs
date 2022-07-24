using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonController : MonoBehaviour
{
    [SerializeField] Sprite nullSprite, poisonSprite;

    public float maxUses;

    public float howManyUses;
    
    public float timeOfAction = 2f;

    Image poisonImage;
    private void Awake()
    {
        poisonImage = GetComponent<Image>();
    }

    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (howManyUses == maxUses)
            poisonImage.fillAmount = maxUses;
    }

    public void ChangeUsesValue()
    {  
        if (howManyUses >= 1)
           poisonImage.fillAmount = howManyUses / maxUses;
        else gameObject.GetComponent<Image>().sprite = nullSprite;

        if (howManyUses >= 1)
            poisonImage.sprite = poisonSprite;

    }
  

}
