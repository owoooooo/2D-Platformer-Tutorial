using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    private bool isHolding;

    private bool dashInputStop;
    private float lastDashTime;
    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAIPos;



    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        Debug.Log("Enter DashState");
        base.Enter();
        CanDash = false;
        player.InputHandler.UseDashInput();
        isHolding = true;
        dashDirection = Vector2.right * Movement.FacingDirection;
        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;


        player.DashDirectionIndicator.gameObject.SetActive(true);

    }
    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit DashState");

        if (Movement?.CurrentVelocity.y > 0)
        {
            Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }

    }
    public override void LogicUpdate()
    {
        //Debug.Log("Logic DashState");
        base.LogicUpdate();
        if (!isExitingState)
        {
            player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

            if (isHolding)
            {
                //Debug.Log("isHolding");
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;
                if (dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();

                }
                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                //player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 225f);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);
                if (dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;

                    Movement?.CheckIfFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.drag;
                    Movement?.SetVelocity(playerData.dashVelocity, dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                Movement?.SetVelocity(playerData.dashVelocity, dashDirection);
                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
                checkIfShouldPlaceAfterImage();
            }

        }

    }
    private void checkIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAIPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }
    private void PlaceAfterImage()
    {

        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;

    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCoolDown;
    }
    public void ResetCanDash() => CanDash = true;


}
