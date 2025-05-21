using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniahPoint : MonoBehaviour
{
    private Animator anim=>GetComponent<Animator>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player plyr= collision.GetComponent<player>();  
        if( plyr != null )
        {
            anim.SetTrigger("activate");
            GameManager.Instance.LeveFinished();
        }
    }
}
