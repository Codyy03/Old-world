using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static bool isDead;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] GameObject deadCanvas;
    [HideInInspector] public float health;
    public int maxHealth;

    PlayerAnimations playerAnimations;
    FightSystem fightSystem;
    AudioManager audioManager;
    void Start()
    {
        health = maxHealth;
        playerAnimations = GetComponent<PlayerAnimations>();
        fightSystem = GetComponent<FightSystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            ChangeHealth(+10);

        if (health <= 0)
        {
            deadCanvas.SetActive(true);
            Time.timeScale = 0;
            isDead = true;
        } else
        {
            deadCanvas.SetActive(false);    
            isDead = false;
        }
      
        
    }
    public void ChangeHealth(float value)
    {
        if (fightSystem.isDefende)
            return;

        if(value<0)
        {
            playerAnimations.ChangeAnimationState(playerAnimations.playerHurt);
            audioManager.PlayClip(hurtSound);
            value += fightSystem.armor * 0.4f;
        }

        health = Mathf.Clamp(health + value, 0, maxHealth);
        Player_UI.instance.ChangeHealth(health / maxHealth);

        
    }
}
