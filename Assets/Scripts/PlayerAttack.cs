using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public AttackSO AttackData;
    public Transform Dice;
    public Transform CurrentTarget;
    [Button]
    public void Start()
    {
        FindTarget();
        StartCoroutine(Attack());
    }
    
    [Button]
    public void FindTarget()
    {
        var casts = Physics.SphereCastAll(Vector3.one, 5f,Vector3.forward,AttackData.AttackRange, LayerMask.GetMask("Enemy"));
        foreach (var hit in casts)
        {
            if (hit.collider.TryGetComponent<Destroyable>(out var dest))
            {
                CurrentTarget = dest.transform;
                return;
            }   
        }
        
    }
    
    IEnumerator Attack()
    {
        var lastAttackTime = Time.time -  AttackData.AttackCooldown;
        while (true)
        {
            if (Time.time > lastAttackTime + AttackData.AttackCooldown)
            {
                if (CurrentTarget != null)
                {
                    var dice = Dice.transform;
                    AttackData.Spawn(dice,dice.forward);
                    lastAttackTime = Time.time;
                }

            }
          
            yield return null;
        }
        
        
    }
}
