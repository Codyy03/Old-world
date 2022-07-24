using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymmonExplode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip expodeSound;
    public float damage;
    PlayerHealth playerHealth;
    AudioManager audioManager;
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.ChangeHealth(-damage);
        }
        audioManager.PlayClip(expodeSound);
        Destroy(gameObject);
    }
}
