using UnityEngine;

public class RunState : IState
{
    private PlayerStateMachine player;

    public RunState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter() => Debug.Log("Entered Run State");

    public void LogicUpdate()
    {
        if (!Input.GetKey(KeyCode.RightArrow))
        {
            player.ChangeState(new IdleState(player));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(new AttackState(player));
        }

        // Di chuyển nhân vật
        player.transform.Translate(Vector3.right * Time.deltaTime * 5);
    }

    public void Exit() => Debug.Log("Exit Run State");
}
