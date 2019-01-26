using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Primitives.Core;
using Primitives.Input;
using Primitives.Grid;

public class RotateControl : InputBehaviour
{
    public GridObject ghostPiece;
    public Vector3Int rotation;

    public override void OnTrigger()
    {
        ghostPiece.Rotate(rotation);
    }

    public override void OnAxis2D(Vector2 input)
    {
    }

    public override void OnAxis2DDown(Vector2 input)
    {
    }
}
