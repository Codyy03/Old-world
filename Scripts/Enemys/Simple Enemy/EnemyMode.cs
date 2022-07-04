using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DisableAttackingMode();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DisableAttackingMode()
    {
        gameObject.GetComponent<EnemyController>().enabled = false;
        gameObject.GetComponent<EnemyAnimations>().enabled = false;
        gameObject.GetComponent<EnemySwordAttack>().enabled = false;
        gameObject.GetComponent<EnemyHealth>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public void EneableAttackingMode()
    {
        gameObject.GetComponent<EnemyController>().enabled = true;
        gameObject.GetComponent<EnemyAnimations>().enabled = true;
        gameObject.GetComponent<EnemySwordAttack>().enabled = true;
        gameObject.GetComponent<EnemyHealth>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
      
    }
}
