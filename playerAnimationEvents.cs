using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationEvents : MonoBehaviour
{
    private   player player;
    private void Awake()
    {
         player=GetComponentInParent<player>();
    }

    public void finishRespawn()=>player.RespawnFinished(true);
}
