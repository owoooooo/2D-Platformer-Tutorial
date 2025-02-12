using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Core : MonoBehaviour
{
    // public Movement Movement
    // {
    //     get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);

    //     private set => movement = value;
    // }
    // public CollisionSenses CollisionSenses
    // {
    //     get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
    //     private set => collisionSenses = value;
    // }
    // public Combat Combat
    // {
    //     get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
    //     private set => combat = value;
    // }
    // public Stats Stats
    // {
    //     get => GenericNotImplementedError<Stats>.TryGet(stats, transform.parent.name);
    //     private set => stats = value;
    // }

    // private Movement movement;
    // private CollisionSenses collisionSenses;
    // private Combat combat;
    // private Stats stats;
    // private List<ILogicUpdate> CoreComponents = new List<ILogicUpdate>();
    private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

    private void Awake()
    {

        // Movement = GetComponentInChildren<Movement>();
        // CollisionSenses = GetComponentInChildren<CollisionSenses>();
        // Combat = GetComponentInChildren<Combat>();
        // Stats = GetComponentInChildren<Stats>();
        // if (!Movement || !CollisionSenses)
        // {
        //     Debug.LogError("Missing Core Component");
        // }


        var comps = GetComponentsInChildren<CoreComponent>();

        foreach (var component in comps)
        {
            AddComponent(component);
        }

        foreach (var component in CoreComponents)
        {
            component.Init(this);
        }
    }
    // public void LogicUpdate()
    // {
    //     foreach (var component in CoreComponents)
    //     {
    //         component.LogicUpdate();
    //     }
    // }

    // public void AddComponent(ILogicUpdate component)
    // {
    //     if (!CoreComponents.Contains(component))
    //     {
    //         CoreComponents.Add(component);
    //     }
    // }
    public void LogicUpdate()
    {
        foreach (CoreComponent component in CoreComponents)
        {
            component.LogicUpdate();
        }
    }
    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponents.Contains(component))
        {
            CoreComponents.Add(component);
        }
    }
    // 根据组件类型获得核心组件
    // public T GetCoreComponent<T>() where T : CoreComponent
    // {
    //     var comp = CoreComponents.OfType<T>().FirstOrDefault();
    //     if (comp)
    //         return comp;
    //     comp = GetComponentInChildren<T>();
    //     if (comp)
    //         return comp;
    //     Debug.LogWarning($" {typeof(T)} not found on {transform.parent.name}");
    //     return null;
    // }
    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponents.OfType<T>().FirstOrDefault();    // 返回 T 类型集合中的第一个组件，若是长度为 0 则返回 null

        if (comp == null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }

        return comp;
    }
    // 根据组件类型获得核心组件
    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}