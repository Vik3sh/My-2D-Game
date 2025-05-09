using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class player : MonoBehaviour
{
    private Animator anim;
    private  Rigidbody2D rb;

    private bool canBeControlled = false;
    private CapsuleCollider2D cd;
    [Header("Movement details")]
    [SerializeField]private float movespeed;
    [SerializeField] private float jumpForce;
    private float defaultGravityScale;
    private bool canDoubleJump;

    [Header("Double Jump details")]
    [SerializeField] private float doubleJumpForce;

    [Header("bUFFER and  cayote  jump")]
    [SerializeField] private float bufferJumpWindow = .25f;
     public float bufferJumpActivated=-1;
    [SerializeField] private float coyoteJumpWindow = .25f;
    private float coyoteJumpActivated;



    [Header("Wall interactions")]
    [SerializeReference] private float wallJumpDuration=.6f;
    [SerializeField] private Vector2 wallJumpForce;
    private bool isWallJumping;


    [Header("Collision info")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isWallDetected;

    [Header("knockback")]
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackPower;
    private bool isKnocked;
    private bool canBeKnocked;


    private bool isGrounded;
    private bool isAirborne;
    private float xInput;
    private float yInput;   

    public bool isRunning;

    private int facingDir = 1;

    private bool facingRight = true;

    [Header("vfx")]
    [SerializeField] private GameObject deathVFX;


    

    public void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        cd=GetComponent<CapsuleCollider2D>();
        anim=GetComponentInChildren<Animator>();

        
    }

    private void Start()
    {
        defaultGravityScale=rb.gravityScale;
        RespawnFinished(false); 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        
            {
                Knockback(transform.position.x);
            }
        updateAirBorneStatus();


        if(canBeControlled==false)
        {
            HandleAnimation();
            HandleCollision();
            return;
        }
        if(isKnocked)
        {
            return;
        }
        HandleInput();
        HandleWallSlide();
        HandleMovement();
        HandleFlip();
        HandleCollision();
        HandleAnimation();

    }

    public void RespawnFinished(bool finished)
    {
        
        if (finished)
        {
            rb.gravityScale=defaultGravityScale;
            cd.enabled = true;
            canBeControlled = true;
        }
        else
        {
            rb.gravityScale = 0;
            canBeControlled = false;    
            cd.enabled= false;
        }
    }

    public void Knockback(float sourceDamageXPosition)
    {
        float knockBackDr = 1;
        if (transform.position.x < sourceDamageXPosition)
        {
            knockBackDr = -1;
        }
        if (isKnocked)
        {
            return;
        }
        StartCoroutine(KnockBackRoutine());
        
        rb.velocity = new Vector2(knockbackPower.x * knockBackDr, knockbackPower.y);
    }

    public void Die()
    {
        GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
        
    }
    public void Push(Vector2 direction, float duration=0)
    {
        StartCoroutine(PushCoroutine(direction, duration));
    }
    private IEnumerator PushCoroutine(Vector2 direction, float duration )
    {
        canBeControlled = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction, ForceMode2D.Impulse);   
        yield return new WaitForSeconds(duration);
        canBeControlled = true;
    }
    private IEnumerator KnockBackRoutine()
    {
        canBeKnocked = false;

        isKnocked = true;
        anim.SetBool("knockBack", true);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
        anim.SetBool("knockBack",false);
    }

    private void updateAirBorneStatus()
    {
        if (isAirborne && isGrounded)
        {
            HandleLanding();
        }

        if (!isGrounded && !isAirborne)
        {
            BecomeAirborne();
        }
    }

    private void BecomeAirborne()
    {
        isAirborne = true;

        if (rb.velocity.y < 0)
        {
           
            ActivateCoyoteJump();

        }
    }

    private void HandleLanding()
    {
        isAirborne = false;
        canDoubleJump = true;

        AttemptBufferJump();

    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
            RequestBufferJump();    
        }
    }

    private void RequestBufferJump()
    {
        if (isAirborne)
        {
            bufferJumpActivated = Time.time;
        }
    }
    private void AttemptBufferJump()
    {
        if(Time.time<bufferJumpActivated+ bufferJumpWindow)
        {
            bufferJumpActivated = Time.time-1;
            Jump();

        }
    }
    #region Buffer  and coyote Jump
    private void ActivateCoyoteJump()=> coyoteJumpActivated= Time.time;
    private void CancelCoyoteJump()=> coyoteJumpActivated = Time.time-1;
    private void JumpButton()
    {
        bool   coyoteJumpAvalilabe=Time.time<coyoteJumpActivated+ coyoteJumpWindow;
        if (isGrounded || coyoteJumpAvalilabe)
        {
            
            Jump();
        }
        else if (isWallDetected && !isGrounded)
        {
            WallJump();
        }
        else if (canDoubleJump)
        {
            DoubleJump();
        }

        CancelCoyoteJump();
    }
    #endregion
    
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }


    private void WallJump()
    {
        canDoubleJump = true;
        rb.velocity = new Vector2(wallJumpForce.x * -facingDir, wallJumpForce.y);
        flip();

        StopAllCoroutines(); 
        StartCoroutine(WallJumpRoutine());
    }
    private IEnumerator WallJumpRoutine()
    {
        isWallJumping = true;  
        yield return new WaitForSeconds(wallJumpDuration);
        isWallJumping = false;
    }

    private void HandleWallSlide()
    {
        bool canWallSlide = isWallDetected && rb.velocity.y < 0;
        float yModifier = yInput < 0 ? 1 : .05f;

        if (canWallSlide == false)
            return;



        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * yModifier);
    }


  
    private void DoubleJump()
    {
        isWallJumping = false;
        canDoubleJump = false;
        rb.velocity=new Vector2(rb.velocity.x,doubleJumpForce);
    }
    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    }

    private void HandleAnimation()
    {
        isRunning = rb.velocity.x != 0;

        anim.SetFloat("xVelocity",rb.velocity.x);
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallDetected", isWallDetected);
    }

    private void HandleMovement()
    {
        if (isWallDetected)
        {
            return;
        }
        if (isWallJumping)
        {
            return;
        }
        rb.velocity = new Vector2(xInput * movespeed, rb.velocity.y);
    }

    private void HandleFlip()
    {
        
        if(xInput<0 && facingRight  || xInput>0 && !facingRight  )
        {
            flip();
        }

    }


    private void flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
        facingRight=!facingRight;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y-groundCheckDistance ));

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance*facingDir), transform.position.y));
    }
}
