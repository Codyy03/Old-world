using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallLaunch : MonoBehaviour
{

     [SerializeField] GameObject fireBall;
     Transform lauchPoint;
     bool canLaunch ;

    
     Animator animator;
  //  [SerializeField] AudioClip expodeSound;
  //  AudioManager audioManager;

    void Start()
    {
        canLaunch = true;
        animator = GetComponent<Animator>();
        //  audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
   
        lauchPoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(canLaunch)
        {
            Launch();
            animator.Play("fire Breat");
        }
    }

    public void Launch()
    {
       // if(calculateDistance.distance <= 10)
       // audioManager.PlayClip(expodeSound);
            Instantiate(fireBall, lauchPoint.transform.position, lauchPoint.transform.rotation);
            canLaunch = false;
            StartCoroutine(WaitToLaunch());
        
    }

    IEnumerator WaitToLaunch()
    {
        yield return new WaitForSeconds(4f);
        canLaunch = true;
    }
   
}
