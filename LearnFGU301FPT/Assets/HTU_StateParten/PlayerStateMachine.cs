using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerStateMachine : MonoBehaviour
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();       // Rời trạng thái cũ
        currentState = newState;
        currentState.Enter();       // Bắt đầu trạng thái mới
    }

    private void Update()
    {
        currentState?.LogicUpdate();
    }
}
