using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primitives
{
    namespace Core
    {
        public static class UnityExtensions
        {
            public static void Shuffle<T>(this IList<T> list)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = Random.Range(0, n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }

            public static string Hierarchy(this MonoBehaviour behaviour)
            {
                string hierarchy = "";
                Transform target = behaviour.transform;

                while (target != null)
                {
                    hierarchy = "::" + target.name + hierarchy;
                    target = target.parent;
                }
                return hierarchy;
            }

            public static bool Contains(this LayerMask mask, int layer)
            {
                return (mask.value & (1 << layer)) > 0;
            }

            public static Color Multiply(this Color color, float value)
            {
                return value * (Vector4)color;
            }


            public static Vector3 LargestAxis(this Vector3 vector)
            {
                if (Mathf.Abs(vector.x) >= Mathf.Abs(vector.y) && Mathf.Abs(vector.x) >= Mathf.Abs(vector.z))
                    return vector.x * Vector3.right;

                if (Mathf.Abs(vector.y) >= Mathf.Abs(vector.z))
                    return vector.y * Vector3.up;

                return vector.z * Vector3.forward;
            }

            public static Vector2 LargestAxis(this Vector2 vector)
            {
                return ((Vector3)vector).LargestAxis();
            }

            public static Vector3 Quantized(this Vector3 vector)
            {
                Vector3 output = vector;
                if (output.x != 0) output.x = Mathf.Sign(output.x);
                if (output.y != 0) output.y = Mathf.Sign(output.y);
                if (output.z != 0) output.z = Mathf.Sign(output.z);
                return output;
            }

            public static Vector2 Quantized(this Vector2 vector)
            {
                return ((Vector3)vector).Quantized();
            }

            public static float Sum(this Vector3 vector)
            {
                return vector.x + vector.y + vector.z;
            }

            public static float Sum(this Vector2 vector)
            {
                return vector.x + vector.y;
            }
        }
    }
}