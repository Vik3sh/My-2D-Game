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
    [SerializeField] private float detectionRange;
    private bool playerDetected;
    private bool canCharge=true;

    protected override void Start()
    {
        base.Start();
        defaultSpeed = moveSpeed;
    }
    protected override void Update()
    {
        base.Update();
        anim.SetFloat("xVelocity", rb.velocity.x);

        HandleCollision();
        handleCharge();
    }

    private void handleCharge()
    {
        if (!canMove)
        {
            return;

        }
        moveSpeed += speedUpRate * Time.deltaTime;

        if (moveSpeed > maxSpeed)
        {
            maxSpeed = moveSpeed;
        }
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

    private void TurnAround()
    {
        moveSpeed = defaultSpeed;
        canMove = false;
        rb.velocity = Vector2.zero;
        flip();
        moveSpeed = defaultSpeed;
    }

    private void wallHit()
    {
        canMove = false;
        moveSpeed = default;
        moveSpeed = defaultSpeed;
        anim.SetBool("hitWall", true);
        rb.velocity = new Vector2(impactPower.x * -facinDir, impactPower.y);
    }

    private void chargeIsOver()
    {
        
        anim.SetBool("hitWall", false);
        Invoke(nameof(flip), 1);
    }


    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facinDir, detectionRange, whatIsPlayer);

        if(playerDetected && isGrounded )
        {
            canMove = true;
           
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (detectionRange * facinDir), transform.position.y));
    }
}
