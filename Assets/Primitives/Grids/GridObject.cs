namespace Primitives
{
    namespace Grid
    {
        using System.Collections;
        using System.Collections.Generic;

        using UnityEngine;

        public class GridObject : MonoBehaviour
        {
            public string label;
            public List<Vector3Int> initialVolume = new List<Vector3Int> { Vector3Int.zero };
            public HashSet<Vector3Int> volume = new HashSet<Vector3Int>();
            public GameGrid grid;

            public void Start()
            {
                RemapVolume(initialVolume);
            }

            public virtual void RemapVolume(IEnumerable<Vector3Int> vol)
            {
                // [TODO] collision check?

                grid.DeregisterObject(this);
                volume.Clear();
                foreach (Vector3Int v in vol)
                    volume.Add(v);
                grid.RegisterObject(this);
            }

            public void MergeVolume(IEnumerable<Vector3Int> vol) { MergeVolume(vol, Vector3Int.zero); }
            public virtual void MergeVolume(IEnumerable<Vector3Int> vol, Vector3Int offset)
            {
                // [TODO] collision check?

                grid.DeregisterObject(this);
                foreach (Vector3Int v in vol)
                {
                    Debug.Log(v + offset);
                    volume.Add(v + offset);
                }
                grid.RegisterObject(this);
            }

            public void Translate(GridObject o, Vector3Int offset)
            {
                if (offset == Vector3Int.zero || grid == null)
                    return;

                Vector3Int newPosition = grid.GetPositionOf(this) + offset;
                if (grid.IsColliding(newPosition, this))
                    return;

                grid.DeregisterObject(this);
                grid.RegisterObject(this, newPosition);
                transform.position = grid.ToWorldSpace(newPosition);
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