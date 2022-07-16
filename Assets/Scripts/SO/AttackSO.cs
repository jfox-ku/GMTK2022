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

        public Attack Spawn(Transform start,Transform target)
        {
            var obj = Instantiate<Attack>(AttackPrefab);
            obj.transform.position = start.position;
            obj.Tweener.SetSpeed(AttackTravelSpeed);
            obj.Tweener.UpdateTarget(target);
            obj.Damage = AttackDamage;

            return obj;
        }
        
    }
}