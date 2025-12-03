using UnityEngine;

public class EnemySpottedPlayerState : EnemyState
{
    private float detectionTimer;
    public EnemySpottedPlayerState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }
    
    public override void EnterState()
    {
        detectionTimer = 0f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        detectionTimer  += Time.deltaTime;
        if (detectionTimer >= enemy.spottingDelay)
        {
            enemy.WeaponRotation.isAiming = true;
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
