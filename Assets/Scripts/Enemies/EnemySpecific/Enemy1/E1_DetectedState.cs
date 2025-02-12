using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DetectedState : DetectedState
{

    private Enemy1 enemy;

    public E1_DetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DetectedState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        //Debug.Log("Detected");

        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
            //Debug.Log("performCloseRangeAction:" + performCloseRangeAction);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
            //Debug.Log("performLongRangeAction:" + performLongRangeAction);
        }
        else if (!isPlayerInMaxAggroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
            //Debug.Log("isPlayerInMaxAggroRange:" + isPlayerInMaxAggroRange);
        }
        else if (!isDetectingLedge)
        {
            Movement?.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
        //TODO:Transition to attack state
    }
    public override void PhysicsUpdate()
    {

        base.PhysicsUpdate();

    }


}
