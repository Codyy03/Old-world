using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] AudioClip expodeSound;
    public float damage;

    Vector2 directory;
    Transform player;
    
    PlayerHealth playerHealth;
    FightSystem fightSystem;
   
    PlayerAnimations playerAnimations;
    AudioManager audioManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
       
        fightSystem= GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>();
        playerAnimations = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimations>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        
        directory = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new Vector2(directory.x, 0).normalized * speed * Time.deltaTime);       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.ChangeHealth(-damage + fightSystem.armor * 0.1f);
            playerAnimations.Block();
        }
        audioManager.PlayClip(expodeSound);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
