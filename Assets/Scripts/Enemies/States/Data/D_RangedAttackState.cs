using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " newRangedAttackStateData", menuName = "Data/State Data/RangedAttack State")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10f;
    public float projectileSpeed = 13f;
    public float projectileTravelDistance = 1f;
}
