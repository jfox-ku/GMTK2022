using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.SO
{   
    [CreateAssetMenu]
    public class AttackSO : ScriptableObject
    {
        [TitleGroup("Data")]
        public float AttackCooldown, AttackDamage, AttackTravelSpeed;
        [TitleGroup("Data"),AssetsOnly,PreviewField]
        public Attack AttackPrefab;

    }
}