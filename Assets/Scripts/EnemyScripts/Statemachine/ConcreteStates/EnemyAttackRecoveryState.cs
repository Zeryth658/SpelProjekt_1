using UnityEngine;

public class EnemyAttackRecoveryState : EnemyState
{
    private float timer;
    public EnemyAttackRecoveryState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }
    
    public override void EnterState()
    {
        timer = 0f; 
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        timer += Time.deltaTime;
        enemy.aimChecker();

        if (timer >= enemy.recoveryTime)
        {
            if (enemy.PlayerDetected())
            {
                enemyStateMachine.ChangeState(enemy.PreparingShotState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemy.IdleState);
            }
           
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
