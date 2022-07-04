using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] AudioClip hurtSound;
    public int maxHealth;

    private float health;
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
            ChangeHealth(-10);

    }
    public void ChangeHealth(float value)
    {
        if (fightSystem.isDefende)
            return;

        if(value<0)
        {
            playerAnimations.ChangeAnimationState(playerAnimations.playerHurt);
            audioManager.PlayClip(hurtSound);
        }

        health = Mathf.Clamp(health + value, 0, maxHealth);
        Player_UI.instance.ChangeHealth(health / maxHealth);

    }
}
