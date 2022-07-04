using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class PlayerFootSteps : MonoBehaviour
{
    [SerializeField] AudioClip[] footsteps;

    [HideInInspector] public float volumeMin, volumeMax;
    [HideInInspector] public float stepDistance;

    float accumulatedDistance;

    AudioSource audioSource;
    PlayerController palyerController;
    FightSystem fightSystem;
    void Start()
    {
        fightSystem = GetComponent<FightSystem>();
        palyerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per fr
    void Update()
    {
        if (fightSystem.isAttack || fightSystem.isDefende)
            return;

        if (ConversationManager.Instance.IsConversationActive)
            return;

        CheckToPlayFootstepSound();

    }

    void CheckToPlayFootstepSound()
    {
        if (!palyerController.isGrounded())
            return;

        if (palyerController.movement.x != 0 )
        {
            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistance)
            {
                audioSource.volume = Random.Range(volumeMin, volumeMax);
                audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
                audioSource.Play();
                accumulatedDistance = 0f;
            }
        }
        else accumulatedDistance = 0f;
    }
}
