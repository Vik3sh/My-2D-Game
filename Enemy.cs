using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Transform player;
    protected Collider2D[] colliders;


    


    [Header("Genereal Info")]   
     
    [SerializeField] protected float moveSpeed =2f;
    protected bool canMove = false;
    [SerializeField] protected float idleDuration =1.5f;
    [SerializeField]protected float idleTimer;



    [Header("Death Details")]
    [SerializeField] private float deathImpactSpeed=5;
    [SerializeField] private float deathRotaitionSpeed=150;
    private int deathRotationDirection = 1;
    protected bool isDead;


    [Header("Basic collision")]
    [SerializeField] protected float groundCheckDistance = 1.1f;
    [SerializeField] protected float wallCheckDistance = .7f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float playerDetectionRange=15;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform groundCheck;
    protected bool isPlayerDetected;
    protected bool isGrounded;
    protected bool isGroundInFrontDetected;
    protected bool isWallDetected;

    protected int facinDir = -1;
    protected bool facingRight = false;  


    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();  
        colliders=GetComponentsInChildren<Collider2D>();
    }

    protected virtual void Start()
    {
       InvokeRepeating("UpdatePlayersRef", 0, 1);
        sr = GetComponent<SpriteRenderer>();

        if (sr.flipX==true && !facingRight)
        {
            sr.flipX = false;   
            flip();
        }
        
    }
    private void UpdatePlayersRef()
    {
        if (player==null)
        {
            player = GameManager.Instance.player.transform;
        }   
        
    }
    protected virtual void Update()
    {
        handleAminmator();
        HandleCollision();
        idleTimer -= Time.deltaTime;

        if (isDead)
        {
            HandleDeath();
        }
    }


    protected virtual void handleAminmator()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        
    }
    public virtual void Die()
    {
        
        foreach(var collider in colliders)
        {
            collider.enabled = false;
        }
        anim.SetTrigger("Hit");
        rb.velocity=new Vector2(rb.velocity.x, deathImpactSpeed); 

        isDead = true;
        if(Random.Range(0, 100)<50)
        {
            deathRotationDirection = deathRotationDirection * -1;
        }
    }
    private void HandleDeath()
    {
        transform.Rotate(0, 0, (deathRotationDirection * deathRotaitionSpeed) * Time.deltaTime);
    }
    

    
    protected virtual void HandleFlip(float xValue)
    {

        if (xValue < transform.position.x && facingRight || xValue > transform.position.x && !facingRight)
        {
            flip();
            
        }

    }
    protected virtual void HandleCollision()
    {
        isGrounded= Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isGroundInFrontDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facinDir, wallCheckDistance, whatIsGround);
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * facinDir, playerDetectionRange, whatIsPlayer);
    }


    protected virtual void flip()
    {
        facinDir = facinDir * -1;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    [ContextMenu("Change flip direction")]
    public void flipDefaultFacingDir()
    {
        sr.flipX = !sr.flipX;   
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance * facinDir), transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (playerDetectionRange * facinDir), transform.position.y));
    }
}
