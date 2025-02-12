using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E1_IdleState : IdleState
{
    private Enemy1 enemy;
    public E1_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        //Debug.Log("IdleState");

        base.LogicUpdate();
        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.detectedState);
            //Debug.Log("isPlayerInMinAggroRange" + isPlayerInMinAggroRange);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
            //Debug.Log("isIdleTimeOver" + isIdleTimeOver);
        }


    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
