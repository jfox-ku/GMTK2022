using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using UnityEngine;
[CreateAssetMenu]
public class PickUpDataSO : ScriptableObject
{
    public GameObject Prefab;

    public GameObject Spawn()
    {
        return Instantiate(Prefab);
    }
    
    public float AttackDamagePercentDelta = 0f;
    public float AttackTravelSpeedPercentDelta = 0f;
    public float AttackSizePercentDelta = 0f;
    public float AttackCooldownPercentDelta = 0f;
    public float AttackLifetimePercentDelta = 0f;

    public void Apply(AttackSO attackData)
    {
        attackData._attackDamageMultiplier += AttackDamagePercentDelta;
        attackData._attackTravelSpeedMultiplier += AttackTravelSpeedPercentDelta;
        attackData._attackSizeMultiplier += AttackSizePercentDelta;
        attackData._attackCooldownMultiplier += AttackCooldownPercentDelta;
        attackData._attackLifetimeMultiplier += AttackLifetimePercentDelta;

        attackData._attackCooldownMultiplier = Mathf.Clamp(attackData._attackCooldownMultiplier,0.05f,100f);
    }
}