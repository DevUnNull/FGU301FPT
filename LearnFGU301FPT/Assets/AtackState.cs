using System.Collections;
using UnityEngine;

public class AttackState : IState
{
    private PlayerStateMachine player;
    private float attackDuration = 1.0f; // thời gian tấn công
    private float timer = 0f;

    public AttackState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Entered Attack State");
        timer = 0f;

        // Ở đây bạn có thể trigger Animator attack
        // player.Animator.SetTrigger("Attack");
    }

    public void LogicUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= attackDuration)
        {
            player.ChangeState(new IdleState(player));
        }
    }

    public void Exit()
    {
        Debug.Log("Exited Attack State");
    }
}
