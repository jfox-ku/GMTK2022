using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.SO
{   
    [CreateAssetMenu,Serializable]
    public class Enemies : ScriptableObject
    {
        public List<EnemyDataSO> AllEnemies;

        public EnemyDataSO GetByIndex(int i) => AllEnemies[i];
    }
}