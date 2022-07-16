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
        StartCoroutine(Attack());
    }

    public float AttackRange;
    
    public void FindTarget()
    {
        //var casts = Physics.SphereCastAll(Vector3.one, 5f);
    }


    IEnumerator Attack()
    {
        var startTime = Time.time;
        while (Time.time < startTime + AttackData.AttackCooldown)
        {
            if (CurrentTarget != null)
            {
                AttackData.Spawn(Dice.transform,CurrentTarget);
            }
            
            yield return null;
        }
        
        
    }
}
