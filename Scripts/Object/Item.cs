using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items",fileName ="Object")]
public class Item :ScriptableObject
{
    public int ID;
    public Sprite image;
    public string objectName;

    [TextArea]
    public string descripotion;

    public int value;
    public float health;
    public float armor;
    public float damage;

}
