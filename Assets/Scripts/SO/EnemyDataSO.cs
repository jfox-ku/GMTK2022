using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu,Serializable]
public class EnemyDataSO : ScriptableObject
{
    public float Health = 100f;
    public float ChaseSpeed = 0.2f;
    [AssetsOnly]
    public GameObject Prefab;
}
