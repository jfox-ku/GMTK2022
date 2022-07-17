using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.SO
{   
    [CreateAssetMenu]
    public class AttackSO : ScriptableObject
    {
        [TitleGroup("Data")]
        public float _attackCooldown, _attackDamage, _attackTravelSpeed, _attackLifetime, _attackSize;
        [TitleGroup("Data"),AssetsOnly,PreviewField]
        public Attack AttackPrefab;
        public float _attackCooldownMultiplier, _attackDamageMultiplier, _attackTravelSpeedMultiplier, _attackLifetimeMultiplier = 1f, _attackSizeMultiplier;

        [TitleGroup("Data")] public bool AttackBackward;

        public float AttackCooldown
        {
            get => _attackCooldown * _attackCooldownMultiplier;
            set => _attackCooldown = value;
        }

        public float AttackDamage
        {
            get => _attackDamage  * _attackDamageMultiplier;
            set => _attackDamage = value;
        }

        public float AttackTravelSpeed
        {
            get => _attackTravelSpeed  * _attackTravelSpeedMultiplier;
            set => _attackTravelSpeed = value;
        }

        public float AttackLifetime
        {
            get => _attackLifetime  * _attackLifetimeMultiplier;
            set => _attackLifetime = value;
        }

        public float AttackSize
        {
            get => _attackSize * _attackSizeMultiplier;
            set => _attackSize = value;
        }

        public void ResetMultipliers()
        {
            _attackCooldownMultiplier = 1f;
            _attackDamageMultiplier = 1f;
            _attackLifetimeMultiplier = 1f;
            _attackTravelSpeedMultiplier = 1f;
            _attackSizeMultiplier = 1f;
        }

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
            var t = obj.transform;
            t.position = start.position;
            t.localScale = Vector3.one * AttackSize;
            obj.Damage = AttackDamage;
            obj.AttackLifetime = AttackLifetime;
            obj.AttackTravelSpeed = AttackTravelSpeed;
            obj.AttackInDirection(dir);
            return obj;
        }
        
        
    }
}