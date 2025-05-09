using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    protected Animator anim;
    //[SerializeField] private Vector2 pushDirection;
    [SerializeField] private float duration = .5f;
    [SerializeField] private float pushPower;
    private void Awake()
    {
        anim= GetComponent<Animator>(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player plyr= collision.gameObject.GetComponent<player>();   
        if( plyr != null)
        {
            plyr.Push(transform.up*pushPower,duration);
            anim.SetTrigger("activate");
        }
    }
}
