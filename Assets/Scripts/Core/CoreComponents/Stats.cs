using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 统计数据
public class Stats : CoreComponent
{
    // 最大生命值
    [SerializeField] private float maxHealth;
    // 当前生命值
    private float currentHealth;
    // 生命值为零事件
    public event Action OnHealthZero;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }
    // 根据伤害值减少当前生命值
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
            Debug.Log("Health is zero!!");
        }
    }
    // 根据恢复值增加当前生命值
    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}