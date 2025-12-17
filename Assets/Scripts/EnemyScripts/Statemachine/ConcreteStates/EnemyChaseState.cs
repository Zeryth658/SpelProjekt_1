using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
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
        float distance = Vector2.Distance(enemy.transform.position, enemy.Shooter.target.position);
        enemy.AimChecker();
        if (distance > enemy.shootingRange)
        {
            if (enemy.PlayerDetected())
            {
                enemy.Animator.SetBool("IsMoving", true);
                enemy.Movement.MovementUpdate();
            }
            else
            {
                enemyStateMachine.ChangeState(enemy.IdleState);
            }
            
        }
        else
        {
            enemyStateMachine.ChangeState(enemy.PreparingShotState);
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
