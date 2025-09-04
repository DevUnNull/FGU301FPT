using UnityEngine;

public class IdleState : IState
{
    private PlayerStateMachine player;

    public IdleState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter() => Debug.Log("Entered Idle State");

    public void LogicUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.ChangeState(new RunState(player));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(new AttackState(player));
        }
    }

    public void Exit() => Debug.Log("Exit Idle State");
}
