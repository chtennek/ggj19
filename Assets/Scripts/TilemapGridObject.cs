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

    void Start()
    {
        if (tilemap != null) {
            if (readFromTilemapOnAwake)
                volume = volume; // [TODO] TilemapToVolume
            else
                RemapVolume(volume);
        }
    }


    public override void RemapVolume(List<Vector3Int> vol) {
        base.RemapVolume(vol);

        tilemap.ClearAllTiles();
        foreach (Vector3Int p in vol) {
            tilemap.SetTile(p, tile);
        }
    }
}
