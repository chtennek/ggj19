using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Primitives.Core;
using Primitives.Input;
using Primitives.Grid;

public class TrashControl : PieceInputBehaviour
{
    public PieceControl ghost;

    public override void OnTrigger()
    {
        ghost.GetNextPiece();
    }

    public override void OnAxis2D(Vector2 input)
    {
    }

    public override void OnAxis2DDown(Vector2 input)
    {
    }
}
