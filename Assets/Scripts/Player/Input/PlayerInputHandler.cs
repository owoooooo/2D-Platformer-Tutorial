using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;


    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }

    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool[] AttackInputs { get; private set; }


    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float DashInputStartTime;
    private void Start()
    {

        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];

        cam = Camera.main;

    }
    private void Update()
    {
        //Debug.LogError(GetHashCode());
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }
    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }
        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.primary] = false;
        }

    }
    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.secondary] = true;
        }
        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.secondary] = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Player move Input HashCode is " + this.GetHashCode());
        RawMovementInput = context.ReadValue<Vector2>();
        // Debug.Log(RawmovementInput);
        // if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        // {
        //     NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        // }
        // else
        // {
        //     NormInputX = 0;
        // }
        // if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        // {
        //     NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        // }
        // else
        // {
        //     NormInputY = 0;
        // }
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);


    }
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        ///Debug.Log("Jump Input");
        if (context.started)
        {
            //Debug.Log("Player Input HashCode is " + this.GetHashCode());
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;

        }
        if (context.canceled)
        {
            JumpInputStop = true;

        }

    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Dash Input");
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            DashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            //Debug.Log("Dash Stop");
            DashInputStop = true;
        }
    }
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();
        // if (playerInput.currentControlScheme == " Keyboard")
        // {
        //     RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        // }

        if (playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - RawDashDirectionInput.x, cam.pixelHeight - RawDashDirectionInput.y, -100f)) - transform.position;
        }
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);

    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= DashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}
public enum CombatInputs
{
    primary,
    secondary
}