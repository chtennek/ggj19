using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Primitives.Grid;

public class TilemapGridObject : GridObject
{
    public bool readFromTilemapOnAwake;
    public Tilemap tilemap;
    public Tile tile;

    public override void MergeVolume(IEnumerable<Vector3Int> vol, Vector3Int offset)
    {
        base.MergeVolume(vol, offset);

        foreach (Vector3Int p in vol)
            tilemap.SetTile(p + offset, tile);
    }

    public override void RemapVolume(IEnumerable<Vector3Int> vol)
    {
        base.RemapVolume(vol);

        tilemap.ClearAllTiles();
        foreach (Vector3Int p in vol)
            tilemap.SetTile(p, tile);
    }
}
