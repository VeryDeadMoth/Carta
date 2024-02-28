using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    PlayerBaseState currentState;
    public PlayerMovingState movingState = new PlayerMovingState();
    public PlayerListeningState listeningState = new PlayerListeningState();
    public PlayerIdleState idleState = new PlayerIdleState();

    public Vector2 targetPos;
    public float idleRadius; 

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        currentState = idleState;
        currentState.EnterState(this);
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
}
