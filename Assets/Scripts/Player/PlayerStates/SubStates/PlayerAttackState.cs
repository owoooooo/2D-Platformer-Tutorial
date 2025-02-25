using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private int xInput;
    private float velocityToSet;
    private bool setVelocity;
    private bool shouldChecpFlip;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        setVelocity = false;

        weapon.EnterWeapon();
    }
    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        if (shouldChecpFlip)
        {
            Movement?.CheckIfFlip(xInput);
        }
        if (setVelocity)
        {
            Movement?.SetVelocityX(velocityToSet * Movement.FacingDirection);
        }


    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        this.weapon.InitializeWeapon(this, core);
    }
    public void SetPlayVelocity(float velocity)
    {
        Movement.SetVelocityX(velocity * Movement.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }
    #region Animation Triggers
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
    #endregion

    public void SetFlipCheck(bool value)
    {
        shouldChecpFlip = value;
    }

}
