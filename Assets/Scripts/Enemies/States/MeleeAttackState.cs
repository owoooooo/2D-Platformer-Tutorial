using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    protected D_MeleeAttackState stateData;
    // protected AttackDetails attackDetails;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        // attackDetails.damageAmount = stateData.attackDamage;
        // attackPosition.position = entity.transform.position;
    }

    public override void Exit()
    {

        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {

        base.PhysicsUpdate();

    }
    public override void FinishAttack()
    {
        base.FinishAttack();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedobjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
        foreach (Collider2D collider in detectedobjects)
        {
            //Debug.Log("TriggerAttack");
            //collider.transform.SendMessage("Damage", attackDetails);
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                // damageable.Damage(attackDetails.damageAmount);
                damageable.Damage(stateData.attackDamage);
            }
            IKnockBackable knockbackable = collider.GetComponent<IKnockBackable>();
            if (knockbackable != null)
            {
                knockbackable.Knockback(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection);
            }

        }

    }

}
