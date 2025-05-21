using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : Enemy
{
    [Header("Plant Details")]
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private float bulletSpeed = 7;
    [SerializeField] private Transform gunPoint;
    private float lastTimeAttacked;  

    protected override void Update()
    {
        base.Update();
            bool canAttack=Time.time > lastTimeAttacked+ attackCooldown;
            //we attacked when time. time or game time is 5 . so last time attak is 5
            //now time. time is 7 so to check if we can attack or not we do if(time.time >5+attackCooldown);



        if (isPlayerDetected && canAttack)
        {
            Attack();
        }
    }

    private void createBullet()
    {
        EnemyBullet newBullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

        Vector2 bulletVelocity = new Vector2(bulletSpeed * facinDir, 0);
        newBullet.setVelocity(bulletVelocity);

        Destroy(newBullet.gameObject, 10);
    }
    private void Attack()
    {
        lastTimeAttacked = Time.time;
        anim.SetTrigger("Attack");
    }


    protected override void handleAminmator()
    {
        //keep it empty unless you need to update paarameteres

    }
}
