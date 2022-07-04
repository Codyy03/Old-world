using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int direction;
    [SerializeField] float speed;
    [SerializeField] AudioClip expodeSound;

    public float damage;

    Rigidbody2D rb;
    PlayerHealth playerHealth;
    FightSystem fightSystem;

    PlayerAnimations playerAnimations;
    AudioManager audioManager;
    CalculateDistance calculateDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
        calculateDistance = GetComponent<CalculateDistance>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        fightSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<FightSystem>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        playerAnimations = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimations>();

        rb.velocity = transform.right * direction * speed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
      
       // rb.AddForce(new Vector2(1 * -direction, Time.deltaTime * 55) * speed * Time.fixedDeltaTime);
    }
    void Update()
    {
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.ChangeHealth(-damage + fightSystem.armor * 0.1f);
            playerAnimations.Block();
        }

        if(calculateDistance.distance < 10)
        audioManager.PlayClip(expodeSound);

        Destroy(gameObject);
    }
}
