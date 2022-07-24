using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTrap : MonoBehaviour
{
    [SerializeField] AudioClip expodeSound;

    public float damage;
    PlayerHealth playerHealth;
   
    AudioManager audioManager;
    Animator aniamtor;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        aniamtor = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            aniamtor.SetTrigger("Explde");
            playerHealth.ChangeHealth(-damage);
            audioManager.PlayClip(expodeSound);
        }
    }
}
