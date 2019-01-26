using UnityEngine;
using System.Collections;

using Primitives.Input;
using Primitives.Grid;

public abstract class PieceInputBehaviour : InputBehaviour
{
    public TilemapGridObject piece;
    public PieceControl control;

    public void Reset()
    {
        if (piece == null)
            piece = GetComponent<TilemapGridObject>();
        if (control == null)
            control = GetComponent<PieceControl>();
    }
}
