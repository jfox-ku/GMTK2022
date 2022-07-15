using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public AttackSO AttackData;
    public Transform Dice;
    public Transform CurrentTarget;

    public void Start()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        var startTime = Time.time;
        while (Time.time < startTime + AttackData.AttackCooldown)
        {

            
            yield return null;
        }
        
        
    }
}
