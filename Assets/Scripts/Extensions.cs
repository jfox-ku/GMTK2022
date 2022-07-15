using UnityEngine;

namespace DefaultNamespace
{
    public static class Extensions
    {
        public static float GetDistance(this Transform t, Vector3 pos)
        {
            return Vector3.Distance(t.position, pos);
        }
    }
}