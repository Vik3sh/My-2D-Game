using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnail : Enemy
{
    [Header("Snail Details")]
    [SerializeField] private EnemySnailBody bodyPrefab;
    [SerializeField] private float maxSpeed = 10;
    private bool hasBody = true;
    protected override void Update()
    {
        base.Update();


        if (isDead)
        {
            return;
        }
        handleMovement();

        if (!isGroundInFrontDetected && hasBody|| isWallDetected)
        {
            if (isGrounded == false)
            {
                return;
            }
            flip();
            idleTimer = idleDuration;
            rb.velocity = Vector2.zero;
        }
    }

    public override void Die()
    {
        if (hasBody)
        {
            canMove = false;
            hasBody = false;
            anim.SetTrigger("Hit");

            rb.velocity = Vector2.zero;
            idleDuration = 0;

        }
        else if(canMove== false && hasBody == false)
        {
            anim.SetTrigger("Hit");
            canMove = true;
            moveSpeed = maxSpeed;
        }
        else
        {
            base.Die();
        }
            

    }

    private void handleMovement()
    {
        if (idleTimer > 0)
        {
            return;
        }

        if(canMove == false)
        {
            return;
        }

        rb.velocity = new Vector2(moveSpeed * facinDir, rb.velocity.y);


    }

    private void CreateBody()
    {
        EnemySnailBody newBody = Instantiate(bodyPrefab, transform.position, Quaternion.identity);



        if(Random.Range(0, 100) < 50)
        {
            deathRotaitionSpeed= deathRotationDirection * -1;
        }
         

        newBody.setUpBody(deathImpactSpeed, deathRotaitionSpeed*deathRotationDirection,facinDir);


       
        Destroy(gameObject,50);
    }

    protected override void flip()
    {
        base.flip();
        if (hasBody == false)
        {
            anim.SetTrigger("wallHit"); 
        }
    }
}
    

