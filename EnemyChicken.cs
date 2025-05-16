using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyChicken : Enemy {
    [Header("Chicken Details")]
    [SerializeField] private bool playerDetected;
    [SerializeField] private float detectionRange;
    [SerializeField] private float aggroDuration;
    private float aggroTimer;
    private bool canFlip = true;    

    BoxCollider2D cd;
    protected override void Awake()
    {
        base.Awake();
        cd = GetComponent<BoxCollider2D>();
    }
    protected override void Update()
    {
        base.Update();
        anim.SetFloat("xVelocity", rb.velocity.x);

        if (isDead)
        {
            return;
        }
        if(playerDetected)
        {
            canMove = true;
            aggroTimer -= Time.deltaTime;   
        }

        if (aggroTimer <= 0)
        {
            canMove = false;
            aggroTimer = aggroDuration;
        }
        HandleCollision();
        handleMovement();
        if (!isGroundInFrontDetected || isWallDetected)
        {
            if (isGrounded == false)
            {
                return;
            }
            flip();
            canMove=false;
            
            rb.velocity = Vector2.zero;
        }
    }



    private void handleMovement()
    {
        if (!canMove)
        {
            return;
        }

       HandleFlip(player.transform.position.x);

       rb.velocity = new Vector2(moveSpeed * facinDir, rb.velocity.y);


    }

    protected override void HandleFlip(float xValue)
    {
        
        if (xValue < transform.position.x && facingRight || xValue > transform.position.x && !facingRight)
        {
            if (canFlip)
            {
                canFlip = false;
                Invoke(nameof(flip), 0.3f);
            }
        }

    }

    protected override void flip()
    {
        base.flip();
        canFlip = true;
    }
    public override void Die()
    {
        base.Die();
        cd.enabled = false;
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.Raycast(transform.position, Vector2.right *  facinDir, detectionRange, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x + (detectionRange *facinDir), transform.position.y));   
    }
}
