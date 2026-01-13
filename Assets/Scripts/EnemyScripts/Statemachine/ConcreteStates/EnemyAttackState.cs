using UnityEngine;

public class EnemyAttackState : EnemyState
{

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }
    
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        
       
        if (enemy.PlayerDetected())
        {
            enemy.Animator.SetBool("Shooting", true);
            enemyStateMachine.ChangeState(enemy.AttackRecoveryState);
        }
        else
        {
            enemyStateMachine.ChangeState(enemy.IdleState);
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
