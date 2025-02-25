using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDate", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{

    [Header("Move state")]
    public float movementVelocity = 10f;
    [Header("Jump state")]
    public float JumpVelocity = 15f;
    public int amountOfJumps = 1;
    [Header("Wall Jump state")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("wall Slide state")]
    public float wallSlimdeVelocity = 0.1f;
    [Header("wall Climb state")]
    public float wallClimbVelocity = 3f;
    [Header("Ledge Climb State")]
    public Vector2 startoffset;
    public Vector2 stopoffset;
    [Header("Dash state")]
    public float dashCoolDown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Crouch States")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 0.5f;
    public float standColliderHeight = 1.6f;




    // [Header("Check Variables")]
    // public float groundCheckRadius = 0.3f;
    // public float wallCheckDistance = 0.5f;
    // public LayerMask whatIsGround;


}
