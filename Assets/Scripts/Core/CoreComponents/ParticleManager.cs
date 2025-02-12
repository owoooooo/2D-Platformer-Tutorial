
using UnityEngine;
// 特效粒子管理器
public class ParticleManager : CoreComponent
{
    //将作为生成粒子的父级
    private Transform particleContainer;
    protected override void Awake()
    {
        base.Awake();
        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }
    // 实例化指定位置和旋转的粒子
    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation, particleContainer);
    }
    // 实例化粒子
    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }
    // 实例化随机旋转的粒子
    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
    {
        // Generate a random rotation along the z-axis 沿 z 轴生成随机旋转
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        // Spawn the particle and return 生成粒子并返回
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }

}
