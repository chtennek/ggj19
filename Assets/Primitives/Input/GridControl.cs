using System;
using System.Collections.Generic;

using Primitives.Core;
using Primitives.Grid;

namespace Primitives
{
    namespace Input
    {
        using UnityEngine;

        public class GridControl : InputBehaviour
        {
            [Header("Parameters")]
            public GridLayout.CellSwizzle swizzle = GridLayout.CellSwizzle.XYZ;
            public GridObject gridObject;
            public float delayedAutoRepeat = .2f;

            private Vector2 lastMove;
            private float lastMoveTimestamp;

            public override void OnAxis2D(Vector2 input)
            {
                input = input.Quantized();
                if (input == lastMove && Time.time < lastMoveTimestamp + delayedAutoRepeat)
                    return;

                if (input != lastMove)
                {
                    lastMove = input;
                    lastMoveTimestamp = Time.time;
                }

                Vector3Int offset = Vector3Int.RoundToInt(Grid.Swizzle(swizzle, input));
                Move(offset);
            }

            public void Move(Vector3Int offset)
            {
                if (offset == Vector3Int.zero)
                    return;

                gridObject.Translate(offset);
            }

            public override void OnTrigger()
            {
            }

            public override void OnAxis2DDown(Vector2 input)
            {
            }
        }
    }
}
