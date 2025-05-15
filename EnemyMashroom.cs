using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMashroom : Enemy
{

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

        if(isDead)
        {
            return;
        }   
        handleMovement();
        HandleCollision();
        if(!isGroundInFrontDetected || isWallDetected)
        {
            if(isGrounded== false)
            {
                return;
            }
            flip();
            idleTimer = idleDuration;
            rb.velocity=Vector2.zero;   
        }   
    }
  
  

    private void handleMovement()
    {
        if (idleTimer > 0)
        {
            return;
        }
       
        
            rb.velocity = new Vector2(moveSpeed * facinDir, rb.velocity.y);
        
        
    }
    public override void Die()
    {
        base.Die();
        cd.enabled = false;
    }
}
