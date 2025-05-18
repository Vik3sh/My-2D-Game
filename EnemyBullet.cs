using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private string playerLayerName = "player";
    [SerializeField] private string groundLayerName= "Ground";   
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    public void setVelocity(Vector2 velocity)
    {
        rb.velocity= velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(playerLayerName))
        {
            collision.GetComponent <player>().Knockback(transform.position.x);
            Destroy(gameObject);
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            Destroy(gameObject);
        }
    }
}
