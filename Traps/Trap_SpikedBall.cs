using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Trap_SpikedBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D spikeRb;
    [SerializeField] private float pushForce;

    private void Start()
    {
        Vector2 pushVector= new Vector2(pushForce,0);   
        spikeRb.AddForce(pushVector, ForceMode2D.Impulse);
    }
}
