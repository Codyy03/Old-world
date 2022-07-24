using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;
public class DeadBoss : MonoBehaviour
{
    [SerializeField] GameObject healthCanvas, projectile;
    [SerializeField] AudioClip healingSound, attackSound, spawnSound;
    public Quest quest;
    bool isAttacking, conversationWasStarted;
    float direction;
    Transform player;

    ShowNotification notification;
    NPCConversation conversation;
    EnemySwordAttack enemySwordAttack;
    EnemyAnimations enemyAnimations;
    EnemyHealth enemyHealth;
    SpriteRenderer sprite;
    AudioManager audioManager;
    DeadBossWalls deadBossWalls;
    // Start is called before the first frame update
    void Start()
    {
        if (quest.questAccepted == 1)
            Destroy(gameObject);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notification = GetComponent<ShowNotification>();
        conversation = GetComponent<NPCConversation>();
        enemySwordAttack = GetComponent<EnemySwordAttack>();
        enemyAnimations = GetComponent<EnemyAnimations>();
        sprite = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        deadBossWalls = GameObject.Find("walls").GetComponent<DeadBossWalls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.isDead)
            return;

        direction = transform.position.x - player.transform.position.x;

        if (direction < 0)
            sprite.flipX = false;
        else if(direction > 0)
            sprite.flipX = true;

       
        if (notification.distance <= notification.distanceToShow && !conversationWasStarted && deadBossWalls.wallDown)
        {
            ConversationManager.Instance.StartConversation(conversation);
            conversationWasStarted = true;
        }

        if (!isAttacking)
            enemyAnimations.ChangeAnimationState(enemyAnimations.idle);

        if (enemyHealth.isDead)
            healthCanvas.SetActive(false);
    }

  public void Attack()
    {
        if (enemyHealth.isDead)
            return;

        int drawAttack = 0;

        healthCanvas.SetActive(true);
        drawAttack = Random.Range(0, enemyAnimations.attacks.Length);

        isAttacking = true;
        if (enemyHealth.health == enemyHealth.maxHealth && drawAttack == 1)
            enemyHealth.health -= 20;

        switch (drawAttack)
        {
            case 0:  enemyAnimations.ChangeAnimationState(enemyAnimations.attacks[0]); break;
            case 1:  enemyAnimations.ChangeAnimationState(enemyAnimations.attacks[1]); enemyHealth.ChangeEnemyHealth(+20); audioManager.PlayClip(healingSound); break;
            case 2:  enemyAnimations.ChangeAnimationState(enemyAnimations.attacks[2]); SpawnProjectile(); break;
        }
        StartCoroutine(WaitToAttack());
    }
    IEnumerator WaitToAttack()
    {
    
        yield return new WaitForSeconds(1.5f);
        Attack();
        
    }

    void SpawnProjectile()
    {
        audioManager.PlayClip(spawnSound);
        for (int i = -3; i<=3; i+=3)
        {
            Instantiate(projectile, new Vector2(player.transform.position.x + i, player.transform.position.y + 9f), transform.rotation);
        }
    }
    void EndAttack()
    {
        isAttacking = false;
    }
    void SimpleAttack()
    {
        audioManager.PlayClip(attackSound);
        enemySwordAttack.SwordAttack();
    }
    public void EneableAttackComponents()
    {
        enemyHealth.enabled = true;
        enemySwordAttack.enabled = true;
    }
}
