using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Primitives.Core;
using Primitives.Input;
using Primitives.Grid;

public class LockControl : InputBehaviour
{
    public GridObject stack;
    public GridObject ghostPiece;

    public override void OnInput(Vector2 input)
    {
    }

    public override void OnTrigger()
    {
        Vector3Int position = ghostPiece.grid.GetPositionOf(ghostPiece);
        if (stack.grid.IsColliding(position, ghostPiece) == true)
            return;

        stack.MergeVolume(ghostPiece.volume, position);
        Debug.Log("Next Piece");
    }
}
