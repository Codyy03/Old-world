using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public string playerIdle = "HeroKnight_Idle";
    public string playerRun = "HeroKnight_Run";
    public string playerJump = "HeroKnight_Jump";
    public string playerRoll = "";
    public string playerFall;
    public string playerHurt;
    public string playerDefendeIdle;
    public List<string> playerAttacks;
  

    string currnetAnimation;
    Animator animator;
    FightSystem fightSystem;
    void Start()
    {
        animator = GetComponent<Animator>();
        fightSystem = GetComponent<FightSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Block()
    {
        animator.SetTrigger("Block");

    }


    public void ChangeAnimationState(string animation)
    {
        if (currnetAnimation == animation) return;

        else 
        {           
            animator.Play(animation);
           
            currnetAnimation = animation;
        }

        
    }
}
