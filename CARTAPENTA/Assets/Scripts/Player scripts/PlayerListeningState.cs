using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListeningState : PlayerBaseState
{
    //subscribe to a delegate if possible ? 
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered State : Listening");
        player.targetPos = player.transform.position;
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision2D collision)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
    }
}
