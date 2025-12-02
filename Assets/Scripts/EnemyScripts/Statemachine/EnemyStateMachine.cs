using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; set; }

    public void Initialize(EnemyState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }

    public virtual void ChangeState(EnemyState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    
}
