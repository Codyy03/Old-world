using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float distanceToGround;
    [SerializeField] GameObject attackPoint;
    [SerializeField] AudioClip jumpSound;
    [HideInInspector] public Vector2 movement;

    public float speed,jumpPower,rollForce;
    public int direction=1;
    public bool roll;
    

    private float horizotalInput;
    private bool isJumping;


    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;
    private float walkStepDistance = 0.28f;
    RaycastHit2D hit;


    AudioManager audioManager;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    PlayerAnimations playerAnimation;
    FightSystem fightSystem;
    PlayerFootSteps playerFootsteps;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimations>();
        fightSystem = GetComponent<FightSystem>();
        audioSource = GetComponent<AudioSource>();
        playerFootsteps = GetComponent<PlayerFootSteps>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        playerFootsteps.volumeMin = walkVolumeMin;
        playerFootsteps.volumeMax = walkVolumeMax;
        playerFootsteps.stepDistance = walkStepDistance;
    }
    private void FixedUpdate()
    {


       

    }

    // Update is called once per frame
    void Update()
    {
        horizotalInput = Input.GetAxis("Horizontal");

        movement = new Vector2(horizotalInput, 0);
        hit = Physics2D.Raycast(transform.position + new Vector3(0, 1.4f, 0), Vector2.down, distanceToGround, groundLayer);

        if (fightSystem.isAttack || fightSystem.isDefende)
            return;
        if (roll)
            return;
        if (OpenShop.shopIsOpen)
            return;

         if (ConversationManager.Instance.IsConversationActive)
         {
            playerAnimation.ChangeAnimationState(playerAnimation.playerIdle);
            return;
         }
    
        
        Movement();

        Jump();

    }

    void Movement()
    {
       
        transform.Translate(movement * speed * Time.deltaTime);

        if(horizotalInput!=0 && isGrounded() && !isJumping)
        {
            playerFootsteps.stepDistance = walkStepDistance;
            playerFootsteps.volumeMax = walkVolumeMax;
            playerFootsteps.volumeMin = walkVolumeMin;

            playerAnimation.ChangeAnimationState(playerAnimation.playerRun);

        } else if( horizotalInput == 0 && isGrounded() && !isJumping )
         playerAnimation.ChangeAnimationState(playerAnimation.playerIdle);
           
        


        if (horizotalInput < 0)
        {
           
            SetDirection(new Vector2(-0.45f, 0.791f), true, -1);

        }
        else if (horizotalInput > 0)
        {
            
            SetDirection(new Vector2(0.45f, 0.791f), false, 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded())
        {
            roll = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.right * direction * rollForce ,ForceMode2D.Impulse);
            playerAnimation.ChangeAnimationState(playerAnimation.playerRoll);
        }


    }

    void SetDirection(Vector2 position, bool flip, int d)
    {
        direction = d;
        attackPoint.transform.localPosition = position;
        spriteRenderer.flipX = flip;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position + new Vector3(0, 1f, 0), Vector2.down);
    }


    void Jump()
    {
       
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            isJumping = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.up* jumpPower,ForceMode2D.Impulse);
            playerAnimation.ChangeAnimationState(playerAnimation.playerJump);
            audioManager.PlayClip(jumpSound);
        }


    }
    void EndRoll()
    {
        roll = false;
      
        isJumping = false;
    }

    void EndJump()
    {
        isJumping = false;
    }
  public bool isGrounded()
    {
      
        if (hit.collider != null)
        {

            return true; 
        }
        else return false;
    }

}
