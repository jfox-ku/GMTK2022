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


    public static Action<float> AttackCooldownLeftPercent;

    private void Awake()
    {
        DiceNumberController.TopNumberChanged += SetAttackIndex;
    }

    [Button]
    public void Start()
    {
        ResetMultipliers();
        FindTarget();
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

    [Button]
    public void FindTarget()
    {
        var casts = Physics.SphereCastAll(Vector3.one, 5f, Vector3.forward, 10f, LayerMask.GetMask("Enemy"));
        foreach (var hit in casts)
        {
            if (hit.collider.TryGetComponent<Destroyable>(out var dest))
            {
                CurrentTarget = dest.transform;
                return;
            }
        }
    }

    public Vector3 ClosestEnemyDirection()
    {
        var position = Dice.transform.position;
        var isHit = Physics.SphereCast(position, 50f, Vector3.forward, out var hit, 20f);
        if (isHit) return (hit.point - position).normalized;
        else return Dice.transform.forward;
    }


    IEnumerator Attack()
    {
        var lastAttackTime = Time.time;
        while (true)
        {
            if (Time.time >= lastAttackTime + AttackData.AttackCooldown)
            {
                var dice = Dice.transform;
                var diceDir = AttackData.AttackBackward ? -dice.forward : Dice.forward;
                diceDir[1] = 0f;
                AttackData.Spawn(dice, diceDir);
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