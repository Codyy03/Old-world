using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public float angle;
    [SerializeField] float speed;
    [SerializeField] float distanceToRun, distanceToAttack,timeToNextAttack;
    [SerializeField] LayerMask layer;
    [SerializeField] float yValueInRaycast;
    public bool isLeft;


    Vector2 lookDirection;
    Vector2 directory;
    Transform player;
    
    float distance;
    bool isAttacking;


    RaycastHit2D hit;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    EnemySwordAttack swordAttack;
    EnemyAnimations enemyAnimation;
    EnemyHealth health;

    private void Awake()
    {
        
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<EnemyHealth>();
        enemyAnimation = GetComponent<EnemyAnimations>();
        swordAttack = GetComponent<EnemySwordAttack>();
        audioSource = GetComponent<AudioSource>();



    }
    private void FixedUpdate()
    {

        if (health.isDead )
            return;

        if (PlayerHealth.isDead)
        {
            audioSource.Stop();
            return;
        }
      

        Movement();
    }

    // Update is called once per frame
    void Update()
    {
       
        

    }

    void Movement()
    {
        // oblicza dystans do gracza
        distance = Vector2.Distance(player.position, transform.position);

        //sprawdza pozycje gracza i przeciwnika
        lookDirection = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);

        //oblicza kierunek 
        directory = player.position - transform.position;

        angle = Mathf.Atan2(directory.x, directory.y) * Mathf.Rad2Deg;


        // jezeli gracz jest wystarczaj¹co blisko przeciwnik rusza w kierunku gracza


        if (distance <= distanceToRun && !isAttacking && !CheckIfIsCloseToOtherEnemy())
        {
           
            enemyAnimation.ChangeAnimationState(enemyAnimation.walk);

            rb.MovePosition(lookDirection);

            if (!audioSource.isPlaying)
                audioSource.Play();

            if (distance <= distanceToAttack && !CheckIfIsCloseToOtherEnemy())
            {
                enemyAnimation.ChangeAnimationState(enemyAnimation.attacks[Random.Range(0, enemyAnimation.attacks.Length)]);
                StartCoroutine(WaitToAttack());
                isAttacking = true;
               
                swordAttack.SwordAttack();
            }


        }
        else if (distance > distanceToRun || CheckIfIsCloseToOtherEnemy())
        {
            EnemyWalk();
        }



        if (angle < 0 )
        {
            if(!isLeft)
            spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;

        }
        else
        {
            if (!isLeft)
            spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;

        }
    }
    void EnemyWalk()
    {
        enemyAnimation.ChangeAnimationState(enemyAnimation.idle);
        audioSource.Stop();
    }
    IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(timeToNextAttack); isAttacking = false;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 255, 255);
        Gizmos.DrawRay(transform.position + new Vector3(0.7f, yValueInRaycast, 0), Vector2.right*1);
    }

    public bool CheckIfIsCloseToOtherEnemy()
    {
        int direction = 0;
      
        if (angle < 0)
            direction = -1;
        else if (angle > 0)
            direction = 1;

        hit = Physics2D.Raycast(transform.position + new Vector3(0.7f * direction, yValueInRaycast, 0), Vector2.right * direction, 1, layer);
        bool isClose = false;

        if (hit.collider != null)
            isClose = true;
        else isClose = false;

        return isClose;

    }
}
