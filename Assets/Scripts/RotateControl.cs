using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Primitives.Core;
using Primitives.Input;
using Primitives.Grid;

public class RotateControl : PieceInputBehaviour
{
    public Vector3Int rotation;

    public override void OnTrigger()
    {
        piece.Rotate(rotation);
        control.OnPieceModified();
    }

    public override void OnAxis2D(Vector2 input)
    {
    }

    public override void OnAxis2DDown(Vector2 input)
    {
    }
}
