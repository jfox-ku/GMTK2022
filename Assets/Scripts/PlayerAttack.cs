using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<AttackSO> AttackDatum;
    public AttackSO AttackData => AttackDatum[Mathf.Clamp(attackIndex, 0, 6)];
    public Transform Dice;
    public Transform CurrentTarget;

    public Coroutine AttackRoutine;
    [SerializeField] private int attackIndex;
    public float AimDistance = 5f;


    public static Action<float> AttackCooldownLeftPercent;

    private void Awake()
    {
        DiceNumberController.TopNumberChanged += SetAttackIndex;
    }

    [Button]
    public void Start()
    {
        ResetMultipliers();
    }

    private void SetAttackIndex(int topNum)
    {
        if (AttackRoutine != null) StopCoroutine(AttackRoutine);
        attackIndex = topNum - 1;
        AttackRoutine = StartCoroutine(Attack());
    }

    public void ResetMultipliers()
    {
        foreach (var attackData in AttackDatum)
        {
            attackData.ResetMultipliers();
        }
    }

    public void SetAttackCooldownMultiplier(float f)
    {
        foreach (var attackData in AttackDatum)
        {
            attackData._attackCooldownMultiplier = f;
        }
    }

    public void SetAttackLifetimeMultiplier(float f)
    {
        foreach (var attackData in AttackDatum)
        {
            attackData._attackLifetimeMultiplier = f;
        }
    }

    public void SetAttackTravelSpeedMultiplier(float f)
    {
        foreach (var attackData in AttackDatum)
        {
            attackData._attackTravelSpeedMultiplier = f;
        }
    }

    public void SetAttackDamageMultiplier(float f)
    {
        foreach (var attackData in AttackDatum)
        {
            attackData._attackDamageMultiplier = f;
        }
    }


    public void OnDestroy()
    {
        DiceNumberController.TopNumberChanged -= SetAttackIndex;
    }

    private void OnDrawGizmos()
    {
        var touchPos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(touchPos + Vector3.forward * AimDistance);
                
        var iDir = worldPos - transform.position;
        iDir[1] = 0f;
        iDir = iDir.normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position+iDir);
    }

    IEnumerator Attack()
    {
        var lastAttackTime = Time.time;
        while (true)
        {
            if (Time.time >= lastAttackTime + AttackData.AttackCooldown)
            {
                var dice = Dice.transform;
                var touchPos = Input.mousePosition;
                var worldPos = Camera.main.ScreenToWorldPoint(touchPos + Vector3.forward * AimDistance);
                
                var iDir = worldPos - transform.position;
                iDir[1] = 0f;
                iDir = iDir.normalized;
                Debug.Log($"Touch: {touchPos} WorldTouch: {worldPos}\nPlayerPos: {transform.position} FinalDir{iDir}");
                
                // var diceDir = AttackData.AttackBackward ? -dice.forward : Dice.forward;
                // diceDir[1] = 0f;
                AttackData.Spawn(dice, iDir.normalized);
                lastAttackTime = Time.time;
                AttackCooldownLeftPercent?.Invoke(1f);
            }
            else
            {
                var timeLeft = lastAttackTime + AttackData.AttackCooldown - Time.time;
                AttackCooldownLeftPercent?.Invoke(timeLeft / AttackData.AttackCooldown);
            }

            yield return null;
        }
    }
}