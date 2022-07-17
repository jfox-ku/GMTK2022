using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Extensions
    {
        public static float GetDistance(this Transform t, Vector3 pos)
        {
            return Vector3.Distance(t.position, pos);
        }

        public static void ChangeY(this Vector3 v,float t)
        {
           v[1] = t;
        }

        public static IEnumerator StickRoutine(this Transform source, Transform target, Vector3 offset, float duration)
        {
            var startTime = Time.time;
            while (Time.time < startTime + duration && source!=null && target!=null)
            {
                source.position = target.position + offset;
                yield return null;
            }
        }
        
    }
}