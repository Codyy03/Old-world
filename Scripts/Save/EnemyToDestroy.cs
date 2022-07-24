using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class EnemyToDestroy : MonoBehaviour
{
    public string[] names = new string[200];
    public GameObject[] enemy = new GameObject[200];
    int i = -1;


    string saveSaparator = "##";
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WhichEnemyDestory(GameObject gameObjectName)
    {
        //dodaje do tablicy nazwe przeciwnika który zosta³ pokonany
        i++;
        names[i] = gameObjectName.name;

        

    }
    public void DestroyEnemyOnLoad()
    {
        // na starcie dodaje do tablicy gameObject przeciwnika który zosta³ pokonany i go niszczy 

        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i] = GameObject.Find(names[i]);
            if (enemy[i] != null)
            Destroy(enemy[i]);
        }
    }


    public void SaveEnemy()
    {
        // zapisuje nazwe przeciwnika który zosta³ pokonany
        string[] constents = new string[200]; 

        for (int i = 0; i < names.Length; i++)
        {
            constents[i] = names[i];
        }
       
        string saveString = string.Join(saveSaparator, constents);

        File.WriteAllText(Application.dataPath + "/saveEnemy.txt", saveString);

    }


  public  void LoadEnemy()
    {
        // wczytuje nazwe przeciwnika który zosta³ pokonany
        string saveString = File.ReadAllText(Application.dataPath + "/saveEnemy.txt");

        string[] contents = saveString.Split(new[] { saveSaparator }, System.StringSplitOptions.None);

        for (int i = 0; i < names.Length; i++)
        {

           
            names[i]=contents[i];
           
        }
        DestroyEnemyOnLoad();





    }
}
