using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyTypeIndex = 0;
    
    [SerializeField]
    private Enemies AllEnemies;
    public EnemyDataSO EnemyData;
    public Destroyable Dest;
    public Rigidbody RB;
    public GameObject EnemyModelObject;

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
        
    }

    private void Destroyed()
    {
        Dest.Destroyed -= Destroyed;
        ParticleManager.Instance.PlayChipDestoyEffect(transform, Vector3.up, Vector3.one);
    }
}
