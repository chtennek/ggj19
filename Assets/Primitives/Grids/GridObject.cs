namespace Primitives
{
    namespace Grid
    {
        using System.Collections;
        using System.Collections.Generic;

        using UnityEngine;

        public class GridObject : MonoBehaviour
        {
            public List<Vector3Int> initialVolume = new List<Vector3Int> { Vector3Int.zero };
            public HashSet<Vector3Int> volume = new HashSet<Vector3Int>();
            public GameGrid grid;

            public void Awake()
            {
                RemapVolume(initialVolume);
            }

            public virtual void RemapVolume(IEnumerable<Vector3Int> vol) {
                volume.Clear();
                foreach (Vector3Int v in vol)
                    volume.Add(v);
            }
            public virtual void MergeVolume(IEnumerable<Vector3Int> vol) {
                volume.UnionWith(vol);
            }

            public void Translate(GridObject o, Vector3Int offset)
            {
                if (offset == Vector3Int.zero || grid == null)
                    return;

                Vector3Int newPosition = grid.GetPositionOf(this) + offset;
                if (grid.IsColliding(newPosition, this))
                    return;

                GameGrid g = grid;
                g.DeregisterObject(this);
                g.RegisterObject(this, newPosition);
                transform.position = g.ToWorldSpace(newPosition);
            }

            public void Rotate(GridObject o, Vector3Int rotation)
            {
                // [TODO] (1, 0, 0), (0, -1, 0), (0, 0, 2)
            }

            public void Scale(GridObject o, Vector3Int scale)
            {
                // [TODO] (1, -1, 1), (1, 2, 1), (0, 0, 0)
            }
        }
    }
}