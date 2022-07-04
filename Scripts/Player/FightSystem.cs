using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class FightSystem : MonoBehaviour
{
    [SerializeField] PoisonController poisonController;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float range;
    [SerializeField] AudioClip swordSound,swordHit,blockSound;

    public bool isAttack;
    public float damage;
    public float armor;
    public bool isDefende;

    private bool poisonIsOnEnemy;

    AudioManager audioManager;
    PlayerAnimations playerAnimations;
    PlayerController playerController;
    InventorySystem inventoryManager;
    GameManager gameManager;
    void Start()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
     
        playerController = GetComponent<PlayerController>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void FixedUpdate()
    {
       
    }

    void Update()
    {
        if (OpenShop.shopIsOpen || Loot.lootIsOpen)
            return;
        if (gameManager.inventoryIsOpen || gameManager.menuIsOpen)
            return;
        if (!playerController.isGrounded())
            return;
        if (ConversationManager.Instance.IsConversationActive)
            return;

        Attack();
        Defend();
    }

    void SwordAttack(float value)
    {
        if (gameManager.inventoryIsOpen)
            return;

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);    

        foreach(Collider2D enemy in hit)
        {
            audioManager.PlayClip(swordHit);
            enemy.GetComponent<EnemyHealth>().ChangeEnemyHealth(value);

            if(poisonController.howManyUses>=1)
            poisonController.ChangeUsesValue();
            
            if (poisonController.poisonCanBeUsed && !poisonIsOnEnemy)
            {
                poisonIsOnEnemy = true;
                enemy.GetComponent<SpriteRenderer>().color= new Color32(130, 248, 136, 255);
                poisonController.howManyUses -= 1;

                if(enemy!=null)
                StartCoroutine(WaitToEndPoison(enemy.gameObject));    
            }
        }
        audioManager.PlayClip(swordSound);
    }


   IEnumerator WaitToEndPoison(GameObject enemys)
    {
        yield return new WaitForSeconds(poisonController.timeOfAction);
        if (enemys != null)
        {
            poisonIsOnEnemy = false;
            enemys.GetComponent<EnemyHealth>().ChangeEnemyHealth(-7);
        }
        enemys.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
       
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttack && !playerController.roll)
        {
            isAttack = true;
           
            playerAnimations.ChangeAnimationState(playerAnimations.playerAttacks[Random.Range(0,3)]);
            SwordAttack(-damage);

        }

       
    }
    void Defend()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            isDefende = true;
            playerAnimations.ChangeAnimationState(playerAnimations.playerDefendeIdle);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isDefende = false;
            playerAnimations.ChangeAnimationState(playerAnimations.playerIdle);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }




    public void EndAttack()
    {
        isAttack = false;
     
    }

    public void PlayBlockSound()
    {
       audioManager.PlayClip(blockSound);
    }
  
}
