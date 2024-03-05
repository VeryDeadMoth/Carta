using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovingState : PlayerBaseState
{

    float movementSpeed = 8;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered State : Moving");
        //TO DO : ADD ANIMATION HERE
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision2D collision)
    {
        //If player collides with NPC, switch to listening
        if (collision.transform.CompareTag("NPC"))
        {
            player.SwitchState(player.listeningState);
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //When left click is pressed, change the player's target position to current mouse position
        if(Input.GetMouseButtonDown(0))
        {
            player.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("Mouse Input -> new direction : " + player.targetPos);
        }

        //If player reaches target position (or is within its radius), return to idle
        if(Mathf.Pow(player.transform.position.x-player.targetPos.x,2)+ Mathf.Pow(player.transform.position.y - player.targetPos.y, 2)<= Mathf.Pow(player.idleRadius,2))
        {
            player.SwitchState(player.idleState);
        }

        //Movement 
        //Note : using transform for movement instead of rigidbody cancels physics, but it might not have that much of an impact here
        player.transform.position = Vector2.MoveTowards(player.transform.position, player.targetPos, movementSpeed * Time.deltaTime);

    }
}
