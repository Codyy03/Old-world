using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEnemy : MonoBehaviour
{
   

    EnemyToDestroy enemyToDestroy;
     // Start is called before the first frame update
    void Start()
    {
        enemyToDestroy = GameObject.Find("Save Manager").GetComponent<EnemyToDestroy>();
       
    }

    void Update()
    {
        
    }

   public void Save()
    {
      
        enemyToDestroy.WhichEnemyDestory(gameObject);
        enemyToDestroy.SaveEnemy();
 
    }

   


}
