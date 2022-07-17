using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyTypeIndex = 0;
    public Action EnemyDestroyedEvent;

    [SerializeField]
    private Enemies AllEnemies;
    public EnemyDataSO EnemyData;
    public Destroyable Dest;
    public Rigidbody RB;
    public GameObject EnemyModelObject;
    public Tweener EnemyTweener;

    private void Start()
    {
        Dest.Destroyed += Destroyed;
    }

    [Button]
    public void SetEnemyType(int i)
    {
        enemyTypeIndex = i;
        EnemyData = AllEnemies.GetByIndex(enemyTypeIndex);
        if(EnemyModelObject!=null) DestroyImmediate(EnemyModelObject);
        EnemyModelObject = Instantiate(EnemyData.Prefab,transform);
        Dest.health = EnemyData.Health;
        EnemyTweener.SetSpeed(EnemyData.ChaseSpeed);
        EnemyTweener.FaceTarget = EnemyData.FaceTarget;
        
    }

    public void SetEnemyTarget(Transform target)
    {
        EnemyTweener.UpdateTarget(target);
    }

    private void Destroyed()
    {
        Dest.Destroyed -= Destroyed;
        EnemyDestroyedEvent?.Invoke();
        ParticleManager.Instance.PlayChipDestoyEffect(transform, Vector3.up, Vector3.one);
    }
}
