using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player plyr = collision.gameObject.GetComponent < player>();
        if(plyr != null )
        {
            plyr.Knockback(transform.position.x);
        }
    }
}
