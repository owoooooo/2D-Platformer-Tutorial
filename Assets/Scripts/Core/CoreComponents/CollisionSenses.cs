using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//碰撞处理
public class CollisionSenses : CoreComponent
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    #region check Transforms
    //地面检查坐标
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);

        private set => groundCheck = value;
    }
    //墙壁检查坐标
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    //横向检查是否接触平台
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    //纵向检查是否接触平台
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    // public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }
    //天花板检查坐标
    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }
    // 地面图层
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    // 地面检查范围
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    // 墙壁检查范围
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    // [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] float groundCheckRadius;
    [SerializeField] float wallCheckDistance;

    #endregion
    #region Check Function
    // 检查天花
    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
    }
    // 检查是否在地面
    public bool Grounded
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }
    // 检查是否接触墙壁
    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    // 检查身后是否接触墙壁
    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    // 横向检查是否接触平台
    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    // 纵向检查是否接触平台
    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }
    // public bool Ledge
    // {
    //     get => Physics2D.Raycast(LedgeCheck.position, Vector2.down, wallCheckDistance, whatIsGround);
    // }

    #endregion
}
