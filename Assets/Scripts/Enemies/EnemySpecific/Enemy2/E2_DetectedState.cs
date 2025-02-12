using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DetectedState : DetectedState
{

    private Enemy2 enemy;

    public E2_DetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DetectedState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {

        base.Enter();
    }
    public override void Exit()
    {

        base.Exit();
    }
    public override void LogicUpdate()
    {

        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown)
            {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.rangedAttackState);
        }

        // else if (performLongRangeAction)
        // {
        //     stateMachine.ChangeState(enemy.chargeState);
        // }
        else if (!isPlayerInMaxAggroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        //TODO:Transition to attack state
    }
    public override void PhysicsUpdate()
    {

        base.PhysicsUpdate();

    }


}
