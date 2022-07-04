using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    bool wasOpen;
    Animator animator;
    Loot loot;
    void Start()
    {
        loot = GetComponent<Loot>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(loot.isOpen && !wasOpen)
        {
            wasOpen = true;
            animator.SetTrigger("Open");

        }
    }
}
