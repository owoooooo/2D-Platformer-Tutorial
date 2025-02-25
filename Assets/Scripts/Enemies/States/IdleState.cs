using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    protected D_IdleState stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAggroRange;
    protected float idleTime = 2f;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    // protected bool isDetectingWall;
    // protected bool isDetectingLedge;

    public override void Enter()
    {

        base.Enter();
        Movement?.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();

    }
    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            Movement?.Flip();
        }

    }
    public override void LogicUpdate()
    {

        base.LogicUpdate();
        Movement.SetVelocityX(0f);
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }

    }
    public override void PhysicsUpdate()
    {

        base.PhysicsUpdate();

    }
    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }


}
