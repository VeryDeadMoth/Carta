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
        player.animator.SetTrigger("Idle");
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision2D collision)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Flip player sprite
            player.spriteRenderer.flipX = player.targetPos.x < player.transform.position.x;
            if (player.gameObject.GetComponent<OutOfBox>() != null)
            {
                if (!player.gameObject.GetComponent<OutOfBox>().IsPositionValid(player.targetPos))
                {
                    player.targetPos = player.transform.position;

                }
            }
            Debug.Log("Mouse Input -> new direction : " + player.targetPos);
            player.SwitchState(player.movingState);
            
        }
    }
}
