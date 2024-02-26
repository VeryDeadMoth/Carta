using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered State : Idle");
        player.targetPos = player.transform.position;
        //TO DO : ADD ANIMATION HERE
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision2D collision)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse Input -> new direction : " + player.targetPos);
            player.SwitchState(player.movingState);
            
        }
    }
}
