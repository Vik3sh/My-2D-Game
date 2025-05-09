using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFallingPlatform : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D[] colliders;
    [SerializeField] private float speed=.75f;
    [SerializeField] private float travelDistance;
    private  Vector3[] wayPoints;
    private int wayPointIndex;
    public bool canMove;
    [Header("Platform Fall Details")]
    [SerializeField] private float impactSpeed = 3;
    [SerializeField] private float impactDuration = .1f;
    [SerializeField] private float fallDelay = .5f;
    [Space] private bool impactHappened = false; 
    private float impactTimer;

    private IEnumerator Start()
    {
        setupWaypoints();
        float randomDelay = Random.Range(0, .6f);
        yield return new WaitForSeconds(randomDelay);

        canMove = true;
    }

    private void ActivatePlatform()
    {
        canMove = true;

    }

    private void Awake()
    {
        anim= GetComponent<Animator>(); 
        rb= GetComponent<Rigidbody2D>();
        colliders=GetComponents<BoxCollider2D>();
    }

    private void setupWaypoints()
    {
        wayPoints= new Vector3[2];

        float yOffset = travelDistance / 2;
        wayPoints[0] = transform.position + new Vector3(0, yOffset, 0);
        wayPoints[1]= transform.position + new Vector3(0,-yOffset, 0);  
    }

    private void Update()
    {
        HandleImpact();
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        if (canMove == false)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex], speed*Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex]) < .1f){
            wayPointIndex++;

            if(wayPointIndex >= wayPoints.Length)
            {
                wayPointIndex = 0;
            }
        }
    }

    private void HandleImpact()
    {
        if (impactTimer <0)
        {
            return;
        }

        impactTimer-=Time.deltaTime;
        transform.position = 
            Vector2.MoveTowards(transform.position, transform.position + (Vector3.down * 10), impactSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (impactHappened)
        {
            return;
        }
        player player=collision.gameObject.GetComponent<player>();  
        if (player != null)
        {
            Invoke(nameof(switchOffPlatform), fallDelay);
            impactTimer = impactDuration;
            impactHappened = true;
        }
    }

    private void switchOffPlatform()
    {
        anim.SetTrigger("deactivate");
        canMove = false;

        rb.isKinematic = false;
        rb.gravityScale = 3.5f;
        rb.drag = .5f;

        foreach(BoxCollider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
