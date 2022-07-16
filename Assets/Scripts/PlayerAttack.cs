using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<AttackSO> AttackDatum;
    public AttackSO AttackData => AttackDatum[Mathf.Clamp(attackIndex ,0,6)];
    public Transform Dice;
    public Transform CurrentTarget;

    public Coroutine AttackRoutine;
    [SerializeField]
    private int attackIndex;
    
    
    public static Action<float> AttackCooldownLeftPercent;

    private void Awake()
    {
        DiceNumberController.TopNumberChanged += SetAttackIndex;
    }

    [Button]
    public void Start()
    {
        FindTarget();
        AttackRoutine = StartCoroutine(Attack());
    }

    private void SetAttackIndex(int topNum)
    {
        if(AttackRoutine!=null) StopCoroutine(AttackRoutine);
        attackIndex = topNum - 1;
        AttackRoutine = StartCoroutine(Attack());
    }

    public void OnDestroy()
    {
        DiceNumberController.TopNumberChanged -= SetAttackIndex;
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

    public Vector3 ClosestEnemyDirection()
    {
        var position = Dice.transform.position;
        var isHit = Physics.SphereCast(position, 50f, Vector3.forward,out var hit, 20f);
        if (isHit) return (hit.point - position).normalized;
        else return Dice.transform.forward;

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Dice.transform.position,ClosestEnemyDirection());
            var position = Dice.transform.position;
            // var isHit = Physics.SphereCast(position, 50f, Vector3.forward,out var hit, 20f);
            // if(isHit) Gizmos.DrawWireSphere(Dice.position + transform.forward * hit.distance,50f);
            //
        }
    }

    IEnumerator Attack()
    {
        var lastAttackTime = Time.time -  AttackData.AttackCooldown - 1f;
        while (true)
        {
            if (Time.time >= lastAttackTime + AttackData.AttackCooldown)
            {
                if (CurrentTarget != null)
                {
                    var dice = Dice.transform;
                    AttackData.Spawn(dice,AttackData.AttackBackward? -dice.forward : Dice.forward);
                    lastAttackTime = Time.time;
                }

            }
            else
            {
                var timeLeft = lastAttackTime + AttackData.AttackCooldown - Time.time;
                AttackCooldownLeftPercent?.Invoke(timeLeft/AttackData.AttackCooldown);
            }
          
            yield return null;
        }
        
        
    }
}
