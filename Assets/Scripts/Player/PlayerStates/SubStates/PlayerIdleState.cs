using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(0f);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // JumpInput = player.InputHandler.JumpInput;

        // if (JumpInput)
        // {
        //     player.InputHandler.UseJumpInput();
        //     stateMachine.ChangeState(player.JumpState);
        //     //Debug.Log("Jump");
        // }

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                //Debug.Log("Logic Input HashCode is " + player.InputHandler.GetHashCode());
                stateMachine.ChangeState(player.MoveState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
        }


    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
