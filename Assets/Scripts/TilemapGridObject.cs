using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Primitives.Grid;

public class TilemapGridObject : GridObject
{
    public Tilemap tilemap;
    public Tile tile;

    void Start()
    {
        if (tilemap != null) {
            RemapVolume(volume);
        }
    }

    public virtual void RemapVolume(List<Vector3Int> vol) {
        tilemap.ClearAllTiles();

        foreach (Vector3Int p in vol) {
            tilemap.SetTile(p, tile);
        }
    }
}
