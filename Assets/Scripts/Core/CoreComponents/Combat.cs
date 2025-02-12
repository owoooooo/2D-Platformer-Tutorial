using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 战斗相关
public class Combat : CoreComponent, IDamageable, IKnockBackable
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;
    //private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private ParticleManager ParticleManager { get => particleManager ??= core.GetCoreComponent<ParticleManager>(); }
    private ParticleManager particleManager;
    // 伤害粒子
    [SerializeField] private GameObject damageParticles;

    // 最大击退时间
    [SerializeField] private float maxKnockbackTime = 0.2f;
    // 是否激活击退
    private bool isKnockbackActive;
    // 击退开始时间
    private float knockbackStartTime;
    public override void LogicUpdate()
    {
        CheckKnockback();
    }
    //伤害更新
    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + "Damaged!");
        Stats?.DecreaseHealth(amount);
        ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
    }
    //击退
    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;

        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }
    //检测击退
    private void CheckKnockback()
    {

        if (isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Grounded) || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }

}
