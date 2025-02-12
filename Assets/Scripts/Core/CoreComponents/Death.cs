using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 实体的生命值达到零时，调用
public class Death : CoreComponent
{
    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private ParticleManager particleManager;
    private Stats Stats => stats ? stats : core.GetCoreComponent<Stats>();
    private Stats stats;
    // 死亡粒子
    [SerializeField] private GameObject[] deathParticles;
    public override void Init(Core core)
    {
        base.Init(core);

        Stats.OnHealthZero += Die;
    }

    private void OnEnable()
    {
        //Stats.OnHealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }

    // 死亡
    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }
}