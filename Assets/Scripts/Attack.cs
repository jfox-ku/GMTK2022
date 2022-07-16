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
            transform.Translate(dir.normalized * AttackTravelSpeed);
            yield return null;
        }
        
        DestroyAttack(true);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Destroyable"))
        {
            if (collision.rigidbody.TryGetComponent<Destroyable>(out var dest))
            {
                dest.TakeDamage(Damage);
                ParticleManager.Instance.PlayHitEffect(this.transform,Vector3.zero);
                DestroyAttack();
            }
        }
    }

    public void DestroyAttack(bool expire=false)
    {
        if(MoveRoutineRef!=null) StopCoroutine(MoveRoutineRef);
        if(expire) ParticleManager.Instance.BulletExpireEffect(transform,Vector3.zero);
        Destroy(this.gameObject);
    }
}
