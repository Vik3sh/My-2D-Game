using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaqAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float coolDown = 1;
    [SerializeField] private Transform[] wayPoint;
     private Vector3[] wayPointPosition;
    public int wayPointIndex = 1;
    private bool canMove = true;
    public int moveDirection = 1;



    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>(); 
       
    }
    private void Start()
    {
        UpdateWayPointInfo();
        transform.position = wayPointPosition[0];
    }

    private void UpdateWayPointInfo()
    {
        wayPointPosition = new Vector3[wayPoint.Length];
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPointPosition[i] = wayPoint[i].position;
        }
    }

    private IEnumerator StopMovement(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
        sr.flipX = !sr.flipX;
    }
    private void Update()
    {

        anim.SetBool("active", canMove);
        if (canMove == false)
        {

            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, wayPointPosition[wayPointIndex],moveSpeed*Time.deltaTime);    
        if (Vector2.Distance(transform.position, wayPointPosition[wayPointIndex]) <  .1f)
        {
                if(wayPointIndex== wayPointPosition.Length - 1 || wayPointIndex==0)
            {
                moveDirection = moveDirection * -1;
                StartCoroutine(StopMovement(coolDown));
            }
            wayPointIndex = wayPointIndex+ moveDirection;

        }
    }

}
