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

    [Button]
    public void SetEnemyType(int i)
    {
        enemyTypeIndex = i;
        EnemyData = AllEnemies.GetByIndex(enemyTypeIndex);
        if(EnemyModelObject!=null) DestroyImmediate(EnemyModelObject);
        EnemyModelObject = Instantiate(EnemyData.Prefab,transform);
        Dest.health = EnemyData.Health;
        
    }
    
    
    
}
