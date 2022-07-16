using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.SO
{   
    [CreateAssetMenu]
    public class AttackSO : ScriptableObject
    {
        [TitleGroup("Data")]
        public float AttackCooldown, AttackDamage, AttackTravelSpeed, AttackRange, AttackLifetime;
        [TitleGroup("Data"),AssetsOnly,PreviewField]
        public Attack AttackPrefab;

        [TitleGroup("Data")] public bool AttackBackward;

        public Attack Spawn(Transform start,Transform target)
        {
            Attack obj = Instantiate(AttackPrefab);
            obj.transform.position = start.position;
            obj.Damage = AttackDamage;
            obj.AttackLifetime = AttackLifetime;
            obj.AttackTravelSpeed = AttackTravelSpeed;

            return obj;
        }

        public Attack Spawn(Transform start, Vector3 dir)
        {
            Attack obj = Instantiate(AttackPrefab);
            obj.transform.position = start.position;
            obj.Damage = AttackDamage;
            obj.AttackLifetime = AttackLifetime;
            obj.AttackTravelSpeed = AttackTravelSpeed;
            obj.AttackInDirection(dir);
            return obj;
        }
        
        
    }
}