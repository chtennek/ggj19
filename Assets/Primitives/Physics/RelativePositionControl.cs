using System.Collections;
using System.Collections.Generic;

using Primitives.Core;

namespace Primitives
{
    namespace Physics
    {
        using UnityEngine;

        public class RelativePositionControl : MonoBehaviour, ITriggerable2D
        {
            public string triggerName = "Position";
            public string TriggerName { get { return triggerName; } }

            [Header("Parameters")]
            public GridLayout.CellSwizzle swizzle = GridLayout.CellSwizzle.XYZ;
            public bool restrictToOrthogonal = true;
            public bool allowZeroDistance = false;
            public float minDistance = 1f;
            public float maxDistance = 1f;

            public void OnTrigger(Vector2 v)
            {
                MoveTo(v);
            }

            public void MoveTo(Vector2 offset)
            {
                if (restrictToOrthogonal)
                    offset = offset.LargestAxis();

                if (allowZeroDistance == false || offset != Vector2.zero)
                    offset = offset.normalized * Mathf.Clamp(offset.magnitude, minDistance, maxDistance);

                offset = Grid.Swizzle(swizzle, offset);
                transform.localPosition = offset;
            }
        }
    }
}
