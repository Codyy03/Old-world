using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public string idle, walk, dead, takeHit;
    public string[] attacks;


    Animator animator;
    string currentAnimation;


    
    void Start()
    {
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeAnimationState(string animationName)
    {
        if (currentAnimation == animationName) return;
        else
        {
            animator.Play(animationName);
            currentAnimation = animationName;
        }
       
    }
}
