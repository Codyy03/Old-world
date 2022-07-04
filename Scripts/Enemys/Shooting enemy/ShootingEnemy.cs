using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [HideInInspector] public float angle;
    [SerializeField] float distanceToShoot,idleDistance;
    [SerializeField] float speed, timeToNextAttack;
  //  [SerializeField] bool isLeft;

    Vector2 lookDirection;
    bool isAttacking;
    Transform player;
    float distance;
    
    
    Vector2 directory;
    SpriteRenderer spriteRenderer;
    EnemyAnimations enemyAnimator;
    DistanceAttack distanceAttack;
    EnemyHealth enemyHealth;
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponent<EnemyAnimations>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        distanceAttack = GetComponent<DistanceAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (enemyHealth.isDead)
            return;

        Movement();
    }
    // Update is called once per fr
    void Update()
    {
       
    }

    void Movement()
    {
        distance = Vector3.Distance(transform.position, player.position);


        if (distance >= distanceToShoot && !isAttacking && distance < idleDistance)
        {

            StartCoroutine(WaitToAttack());
            isAttacking = true;
            distanceAttack.UseDistanceAttack();
            animator.SetTrigger("Attack");
            //enemyAnimator.ChangeAnimationState(enemyAnimator.attacks[Random.Range(0, enemyAnimator.attacks.Length)]);


        }
        else if (distance >= idleDistance)
        {
            enemyAnimator.ChangeAnimationState(enemyAnimator.idle);

            audioSource.Stop();

        }


        //sprawdza pozycje gracza i przeciwnika
        lookDirection = Vector2.MoveTowards(transform.position, player.transform.position, speed * -1 * Time.deltaTime);

        //oblicza kierunek 
        directory = player.position - transform.position;
        
        angle = Mathf.Atan2(directory.x, directory.y) * Mathf.Rad2Deg;

        if (distance < distanceToShoot && !isAttacking)
        {
            rb.MovePosition(lookDirection);
            enemyAnimator.ChangeAnimationState(enemyAnimator.walk);

            if (!audioSource.isPlaying)
                audioSource.Play();
        }

     
          

      

        if (directory.x > 0)
        SetDirectionLeft();

        if (directory.x < 0)
            SetDirectionRight();
    }
    void SetDirectionRight()
    {
        if (angle > 0 )
        {
            if (!isAttacking)
                spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
        else
        {
            if (isAttacking)
                spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
 
        }
    }
    void SetDirectionLeft()
    {
        if (angle < 0 )
        {
            // if (!isLeft)
            if (!isAttacking)
                spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;

        }
        else 
        {
            if (isAttacking)
                spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;

        }
    }

    IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(timeToNextAttack); isAttacking = false;

    }

}
