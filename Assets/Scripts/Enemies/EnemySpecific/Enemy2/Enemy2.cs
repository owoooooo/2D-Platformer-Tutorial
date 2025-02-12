using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState { get; private set; }
    public E2_MoveState moveState { get; private set; }
    public E2_DetectedState detectedState { get; private set; }
    //public E2_ChargeState chargeState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_DetectedState detectedStateData;
    // [SerializeField]
    // private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    public D_RangedAttackState rangedAttackStateData;
    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;


    public override void Awake()
    {
        base.Awake();
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        detectedState = new E2_DetectedState(this, stateMachine, "detected", detectedStateData, this);
        //chargeState = new E2_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookforplayer", lookForPlayerStateData, this);
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeattack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
        dodgeState = new E2_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangedAttackState = new E2_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

    }
    void Start()
    {
        stateMachine.Initialize(moveState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

    }
    // public override void Damage(AttackDetails attackDetails)
    // {
    //     base.Damage(attackDetails);

    //     if (isDead)
    //     {
    //         stateMachine.ChangeState(deadState);
    //     }
    //     else if (isStunned && stateMachine.currentState != stunState)
    //     {
    //         stateMachine.ChangeState(stunState);
    //     }
    //     else if (CheckPlayerInMinAgroRange())
    //     {
    //         stateMachine.ChangeState(rangedAttackState);
    //     }

    //     else if (!CheckPlayerInMinAgroRange())
    //     {
    //         lookForPlayerState.SetTurnImmediately(true);
    //         stateMachine.ChangeState(lookForPlayerState);
    //     }

    // }
}
