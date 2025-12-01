using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine _enemyStateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this._enemyStateMachine = enemyStateMachine;
    }
    
    public virtual void EnterState(){}
    public virtual void ExitState(){}
    public virtual void FrameUpdate(){}
    public virtual void PhysicsUpdate(){}

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
    }
  
    
}
