using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Damage;
    public float AttackLifetime;
    public float AttackTravelSpeed;

    private Coroutine MoveRoutineRef;

    public void AttackInDirection(Vector3 dir)
    {
        MoveRoutineRef = StartCoroutine(MoveRoutine(dir));
    }

    public IEnumerator MoveRoutine(Vector3 dir)
    {
        var startTime = Time.time;
        while (Time.time < startTime + AttackLifetime)
        {
            transform.Translate(dir * AttackTravelSpeed);
            yield return null;
        }
        
        DestroyAttack();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Destroyable"))
        {
            if (collision.gameObject.TryGetComponent<Destroyable>(out var dest))
            {
                dest.TakeDamage(Damage);
                DestroyAttack();
            }
        }
    }

    public void DestroyAttack()
    {
        if(MoveRoutineRef!=null) StopCoroutine(MoveRoutineRef);
        Destroy(this.gameObject);
    }
}
