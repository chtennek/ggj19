using System.Collections;
using System.Collections.Generic;

using Primitives.Core;

namespace Primitives
{
    namespace Physics
    {
        using UnityEngine;

        public class QuantizedControl : MonoBehaviour, ITriggerable2D
        {
            public string triggerName = "Position";
            public string TriggerName { get { return triggerName; } }

            [Header("Parameters")]
            public GridLayout.CellSwizzle swizzle = GridLayout.CellSwizzle.XYZ;
            public Vector3 gridOffset;
            public Vector3 gridScale = Vector3.one;
            public float delayedAutoRepeat = .1f;

            private Vector2 lastMove;
            private float lastMoveTimestamp;

            public void OnTrigger(Vector2 v)
            {
                Move(v);
            }

            public void Move(Vector2 offset)
            {
                offset = offset.Quantized();
                if (offset == lastMove && Time.time < lastMoveTimestamp + delayedAutoRepeat)
                    return;

                if (offset != lastMove)
                {
                    lastMove = offset;
                    lastMoveTimestamp = Time.time;
                }

                transform.localPosition += Grid.Swizzle(swizzle, offset);
            }
        }
    }
}
