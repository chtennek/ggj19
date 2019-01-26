using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Primitives.Core;
using Primitives.Input;
using Primitives.Grid;

public class FlipControl : InputBehaviour
{
    public GridObject ghostPiece;

    public override void OnTrigger()
    {
        ghostPiece.Scale(new Vector3Int(-1, 1, 1));
    }

    public override void OnAxis2D(Vector2 input)
    {
    }

    public override void OnAxis2DDown(Vector2 input)
    {
    }
}
