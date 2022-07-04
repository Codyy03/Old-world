using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFastAccess : MonoBehaviour
{
    [SerializeField] SetPotionToSlot set1, set2;
    [SerializeField] SetSwordToSlot setSword;
    [SerializeField] SetArmorToSlot setArmor;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("fastAccessID", set1.saveID);
        PlayerPrefs.SetInt("fastAccessID2", set2.saveID);
        PlayerPrefs.SetInt("setSword", setSword.saveID);
        PlayerPrefs.SetInt("setArmor", setArmor.saveID);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        
        set1.saveID = PlayerPrefs.GetInt("fastAccessID");
        set2.saveID = PlayerPrefs.GetInt("fastAccessID2");
        setSword.saveID = PlayerPrefs.GetInt("setSword");
        setArmor.saveID = PlayerPrefs.GetInt("setArmor");

        set1.LoadFastAccess();
        set2.LoadFastAccess();
        setSword.LoadFastAccess();
        setArmor.LoadFastAccess();
    }
}
