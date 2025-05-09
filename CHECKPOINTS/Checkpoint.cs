using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   
    private bool active;
    [SerializeField] private bool canBeReactivated;
    private Animator anim=>GetComponent<Animator>();
    private void Start()
    {
        canBeReactivated=GameManager.Instance.canReactivate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (active)
        {
            return;
        }
        player plyr = collision.GetComponent<player>();

        if (plyr != null)
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        active = true;
        anim.SetTrigger("activate");
        GameManager.Instance.UpdateRespawnPosition(transform);
    }
}
