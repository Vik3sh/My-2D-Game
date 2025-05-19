using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player plyr = collision.gameObject.GetComponent<player>();
        if (plyr != null)
        {
            plyr.Die();
            GameManager.Instance.respawnLayer();
        }

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Die();
            
        }
    }
}
