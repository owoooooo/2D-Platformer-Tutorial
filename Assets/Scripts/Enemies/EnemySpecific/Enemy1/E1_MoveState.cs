using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    public E1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        //Debug.Log("isDetectingLedge:" + isDetectingLedge);
        //Debug.Log("Move");
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {

            enemy.idleState.SetFlipAfterIdle(true);

            stateMachine.ChangeState(enemy.idleState);
            //Debug.Log("isDetectingLedgeinMove:" + isDetectingLedge);
        }
        else if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.detectedState);
            //Debug.Log("isPlayerInMinAggroRange:" + isPlayerInMinAggroRange);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
