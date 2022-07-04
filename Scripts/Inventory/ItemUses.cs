using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUses : MonoBehaviour
{
    [SerializeField] SetPotionToSlot set1, set2;
    [SerializeField] SetSwordToSlot setSword;
    [SerializeField] SetArmorToSlot setArmor;

    public int[] usesID= new int[4];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        usesID[0] = set1.saveID;
        usesID[1] = set2.saveID;
        usesID[2] = setSword.saveID;
        usesID[3] = setArmor.saveID;
    }
}
