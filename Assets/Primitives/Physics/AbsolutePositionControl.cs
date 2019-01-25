using System.Collections;
using System.Collections.Generic;

using Primitives.Core;

namespace Primitives
{
    namespace Physics
    {
        using UnityEngine;

        public class AbsolutePositionControl : MonoBehaviour, ITriggerable2D
        {
            public string triggerName = "Position";
            public string TriggerName { get { return triggerName; } }

            [Header("Parameters")]
            public GridLayout.CellSwizzle swizzle = GridLayout.CellSwizzle.XYZ;
            public Transform matrix;

            public void OnTrigger(Vector2 v)
            {
                MoveTo(v);
            }

            public void MoveTo(Vector2 input)
            {
                Vector3 position = Grid.Swizzle(swizzle, input);
                position = matrix.rotation * position;
                position = Vector3.Scale(position, matrix.lossyScale);
                position += matrix.position;

                transform.position = position;
            }
        }
    }
}
