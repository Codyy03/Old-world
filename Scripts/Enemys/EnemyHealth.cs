using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Image heathBar;
   
    [SerializeField] GameObject lootPrefab;

    public bool isDead;
    public float maxHealth;
    private float health;

    EnemyAnimations enemyAnimation;
    
    void Start()
    {
        health = maxHealth;
        enemyAnimation = GetComponent<EnemyAnimations>();
    }

   
    void Update()
    {

    }

    public void ChangeEnemyHealth(float value)
    {
        health = Mathf.Clamp(health+value, 0, maxHealth);
        heathBar.fillAmount = health / maxHealth;

        enemyAnimation.ChangeAnimationState(enemyAnimation.takeHit);
        
        if (health <= 0)
        {
            enemyAnimation.ChangeAnimationState(enemyAnimation.dead);
            isDead=true;
            InstantiateLoot(lootPrefab);
        }
           
    }

    void InstantiateLoot(GameObject prefab)
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
