using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyRino :Enemy
{
    [Header("Rino Details")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedUpRate=.6f;
    private float defaultSpeed;
    [SerializeField] private Vector2 impactPower;
   
    private bool canCharge=true;
    

    protected override void Start()
    {
        base.Start();
        defaultSpeed = moveSpeed;
    }
    protected override void Update()
    {
        base.Update();
       

        
        handleCharge();
    }

    private void handleCharge()
    {
        if (isDead)
        {
            return;
        }
        if (canMove == false)
        {
            return;

        }
        HandleSpeedUp();
        rb.velocity = new Vector2(moveSpeed * facinDir, rb.velocity.y);

        if (isWallDetected)
        {
            wallHit();
        }

        if (!isGroundInFrontDetected)
        {
            TurnAround();
        }


    }

    private void HandleSpeedUp()
    {
        moveSpeed += speedUpRate * Time.deltaTime;

        if (moveSpeed > maxSpeed)
        {
            maxSpeed = moveSpeed;
        }
    }

    private void TurnAround()
    {
        SpeedReset();
        canMove = false;
        rb.velocity = Vector2.zero;
        flip();
        moveSpeed = defaultSpeed;
    }

    private void wallHit()
    {
        canMove = false;
        SpeedReset();
        anim.SetBool("hitWall", true);
        rb.velocity = new Vector2(impactPower.x * -facinDir, impactPower.y);
    }

    private void SpeedReset()
    {
        moveSpeed = default;
        moveSpeed = defaultSpeed;
    }

    private void chargeIsOver()
    {
        
        anim.SetBool("hitWall", false);
        Invoke(nameof(flip), 1);
    }

    
   
    protected override void HandleCollision()
    {
        base.HandleCollision();
        if(isPlayerDetected && isGrounded )
        {
            canMove = true;
           
        }
    }

   
}
