using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyChicken : Enemy
{
    [Header("Chicken Details")]
    [SerializeField] private float detectionRange;
    [SerializeField] private float aggroDuration;


    private float aggroTimer;
    private bool canFlip = true;
    private bool playerDetected;


    
    protected override void Update()
    {
        base.Update();

        if(playerDetected == true)
        {
            Debug.Log("Player detected");   
        }
        

        if (isDead)
        {
            return;
        }
        if (isPlayerDetected)
        {
            canMove = true;
            aggroTimer -= Time.deltaTime;
        }

        if (aggroTimer <= 0)
        {
            canMove = false;
            aggroTimer = aggroDuration;
        }
        
        handleMovement();
        if (!isGroundInFrontDetected || isWallDetected)
        {
            if (isGrounded == false)
            {
                return;
            }
            flip();
            canMove = false;

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
 
   

   
}
