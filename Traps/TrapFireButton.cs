using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFireButton : MonoBehaviour
{
    private Animator anim;
    private TrapFire trapFire;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        trapFire=GetComponentInParent<TrapFire>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player player=collision.gameObject.GetComponent<player>(); 
        if(player != null )
        {
            anim.SetTrigger("activate");
            trapFire.SwitchOffFire();
        }
    }
}
