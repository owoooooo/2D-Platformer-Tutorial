using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    protected int xInput;
    protected int yInput;
    protected bool isTouchingCeiling;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
        //
        //get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();
        //get
        /// {
        //     if (collisionSenses)
        //     {
        //         return collisionSenses;
        //     }
        //     collisionSenses = core.GetCoreComponent<CollisionSenses>();
        //     return collisionSenses;
        // }
    }
    private CollisionSenses collisionSenses;
    private bool jumpInput;
    private bool grabInput;
    private bool dashInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Grounded;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            // isTouchingLedge = core.CollisionSenses.Ledge;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
    }
    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        //Debug.Log(xInput);
        jumpInput = player.InputHandler.JumpInput;
        //Debug.Log("Logic Input HashCode is " + player.InputHandler.GetHashCode());
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            //player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
            //Debug.Log("Jump");
        }
        else if (!isGrounded)
        {
            //player.JumpState.DecreaseAmountOfJumpsLeft(); ?
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {

            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {

            stateMachine.ChangeState(player.DashState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
