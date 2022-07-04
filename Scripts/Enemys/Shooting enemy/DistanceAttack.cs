using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    [SerializeField] AudioClip attackSound;
  
    [SerializeField] GameObject projectilePrefab;
    public Transform attackPoint;

    Vector2 attackPointPosition;
    
    ShootingEnemy shootingEnemy;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    { 
      
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        shootingEnemy = GetComponent<ShootingEnemy>();
        attackPointPosition = attackPoint.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingEnemy.angle > 0)
            attackPoint.localPosition = attackPointPosition;
        else
            attackPoint.localPosition = new Vector2(attackPointPosition.x * -1, attackPointPosition.y);
    }

    public void UseDistanceAttack()
    {
        Instantiate(projectilePrefab, attackPoint.transform.position, attackPoint.transform.rotation);
        
        audioManager.PlayClip(attackSound);
    }
}
