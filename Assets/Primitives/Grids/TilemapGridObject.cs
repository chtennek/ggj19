using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Primitives.Grid;

namespace Primitives
{
    namespace Grid
    {
        public class TilemapGridObject : GridObject
        {
            public bool readFromTilemapOnAwake;
            public Tilemap tilemap;
            public Tile tile;

            public void Reset()
            {
                if (grid == null)
                    grid = GetComponent<GameGrid>();
                if (tilemap == null)
                    tilemap = GetComponent<Tilemap>();
            }

            public void Awake()
            {
                if (readFromTilemapOnAwake)
                {
                    ReadFromTilemap();
                }
            }

            private void ReadFromTilemap()
            {
                initialVolume.Clear();
                foreach (Vector3Int p in tilemap.cellBounds.allPositionsWithin)
                {
                    if (tilemap.HasTile(p))
                        initialVolume.Add(p);
                }
            }

            public void RefreshTilemap() {
                foreach (Vector3Int p in volume)
                    tilemap.SetTile(p, tile);
            }

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
    }
}
