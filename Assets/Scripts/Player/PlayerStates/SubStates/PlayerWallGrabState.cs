using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;
        Holdposition();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Holdposition();
            if (yInput > 0)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }

    }

    private void Holdposition()
    {
        player.transform.position = holdPosition;

        Movement?.SetVelocityX(0f);
        Movement?.SetVelocityY(0f);
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void DoChecks()
    {
        base.DoChecks();

    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

}
