using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Animator anim=>GetComponent<Animator>();
    private void OnTriggerExit2D(Collider2D collision)
    {
        player plyr=collision.GetComponent<player>();   
        if( plyr != null )
        {
            anim.SetTrigger("activate");
        }
    }
}
