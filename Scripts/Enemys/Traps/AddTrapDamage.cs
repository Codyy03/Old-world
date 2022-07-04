using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTrapDamage : MonoBehaviour
{
    [SerializeField] AudioClip trapSound;

    [SerializeField] float damage;


    FightSystem figthSystem;
    PlayerHealth playerHealth;
    AudioManager audioManager;
    CalculateDistance calculateDistance;
    bool damageWasDone, canAddDamage;
    
    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        figthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        calculateDistance = GetComponent<CalculateDistance>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !damageWasDone && canAddDamage)
        {
          //  damageWasDone = true;
           
            playerHealth.ChangeHealth(-damage + figthSystem.armor * 0.1f);
            damageWasDone = true;
            StartCoroutine(WaitToAddNextDamage());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !damageWasDone && canAddDamage)
        {
            //damageWasDone = true;
           
            playerHealth.ChangeHealth(-damage + figthSystem.armor * 0.1f);
            damageWasDone = true;

        }
    }

    IEnumerator WaitToAddNextDamage()
    {
        yield return new WaitForSeconds(0.6f);
        damageWasDone = false;

    }
    public void PlayTrapSound()
    {
        if(calculateDistance.distance <= 10)
        audioManager.PlayClip(trapSound);
    }

    public void EnabledDamage()
    {
        canAddDamage = true;
    }
    public void DisableDamage()
    {
        canAddDamage = false;
    }


}
