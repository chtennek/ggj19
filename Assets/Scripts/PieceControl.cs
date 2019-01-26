﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Primitives.Core;
using Primitives.Input;
using Primitives.Grid;

public class PieceControl : InputBehaviour
{
    public GridObject stack;
    public PieceQueue queue;

    public TilemapGridObject ghostPiece;
    public Tile validTile;
    public Tile invalidTile;

    public GridControl ghostControl;

    private Vector3Int lastInput;

    public IEnumerator Start()
    {
        yield return null;
        GetNextPiece();
    }

    public void Update()
    {
        Vector3Int p = ghostPiece.grid.ToGridSpace(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        p.z = 0;

        if (lastInput != p && ghostPiece.grid.IsColliding(p, ghostPiece) == false)
        {
            lastInput = p;
            ghostPiece.MoveTo(p);
            OnPieceModified();
        }
    }

    public override void OnTrigger()
    {
        Vector3Int position = ghostPiece.grid.GetPositionOf(ghostPiece);
        if (IsGhostPieceLegal() == false)
            return;

        stack.MergeVolume(ghostPiece.volume, position);
        GetNextPiece();
    }

    public void OnPieceModified()
    {
        if (IsGhostPieceLegal() == true)
            ghostPiece.tile = validTile;
        else
            ghostPiece.tile = invalidTile;

        ghostPiece.RefreshTilemap();
    }

    public bool IsGhostPieceLegal()
    {
        bool legal = true;
        Vector3Int position = ghostPiece.grid.GetPositionOf(ghostPiece);
        if (stack.grid.IsColliding(position, ghostPiece) == true)
            legal = false;

        if (stack.grid.IsColliding(position + Vector3Int.down, ghostPiece) == false)
            legal = false;

        return legal;
    }

    public void GetNextPiece()
    {
        GridObject piece = queue.Dequeue();
        if (piece == null)
            ghostPiece.RemapVolume(new HashSet<Vector3Int>());
        else
            ghostPiece.RemapVolume(piece.volume);
    }

    public override void OnAxis2D(Vector2 input)
    {
    }

    public override void OnAxis2DDown(Vector2 input)
    {
    }
}
