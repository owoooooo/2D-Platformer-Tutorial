using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
// 处理刚体移动
public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    // 面向方向
    public int FacingDirection { get; private set; }
    // 能否设置速度
    public bool CanSetVelocity { get; set; }

    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();
        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
        CanSetVelocity = true;
    }
    public override void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Function
    // 设置速度为零
    public void SetVelocityZero()
    {
        workspace = Vector2.zero;
        SetFinalVelocity();
    }
    // 根据速度，角度，方向设置矢量
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }
    // 根据速度，向量设置矢量
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        //workspace = -direction * velocity;
        SetFinalVelocity();
    }
    // 设置最终速度
    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

    // 根据速度设置X轴移动
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }
    // 根据速度设置Y轴移动
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();

    }
    #endregion
    // 根据X轴输入检查是否要翻转
    public void CheckIfFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    // 翻转
    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);

    }
}
