using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private Enemy1 enemy;
    public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void DoChecks()
    {
        base.DoChecks();
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
        //Debug.Log("ChargeState");

        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            //Debug.Log("performCloseRangeAction" + performCloseRangeAction);
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (!isDetectingLedge || !isDetectingWall)
        {
            //Debug.Log("isDetectingWall" + isDetectingWall);
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            //Debug.Log("isChargeTimeOver" + isChargeTimeOver);

            if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(enemy.detectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
