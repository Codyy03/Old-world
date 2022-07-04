using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordAttack : MonoBehaviour
{
    [SerializeField] AudioClip attack;
    public Transform attackPoint;
    public float range;
    public LayerMask layer;
    public float damage;

    Vector2 attackPointPosition;
    FightSystem fightSystem;
    AudioManager audioManager;
    EnemyController enemyController;
    void Start()
    {
        fightSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        enemyController = GetComponent<EnemyController>();
        attackPointPosition = attackPoint.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.isLeft)
            SetAttackPointDierctionIfIsLeft();

        if (!enemyController.isLeft)
            SetAttackPointDierction();

    }

    void SetAttackPointDierction()
    {
        if (enemyController.angle > 0)
            attackPoint.localPosition = attackPointPosition;
        else
            attackPoint.localPosition = new Vector2(attackPointPosition.x * -1, attackPointPosition.y);
    }
    void SetAttackPointDierctionIfIsLeft()
    {

        if (enemyController.angle < 0)
            attackPoint.localPosition = attackPointPosition;
        else
            attackPoint.localPosition = new Vector2(attackPointPosition.x * -1, attackPointPosition.y);
    }
    public void SwordAttack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, range, layer);

      
        foreach (Collider2D player in hit)
        {

            player.GetComponent<PlayerHealth>().ChangeHealth(damage+ fightSystem.armor * 0.1f);
            player.GetComponent<PlayerAnimations>().Block();

        }
        audioManager.PlayClip(attack);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

}
