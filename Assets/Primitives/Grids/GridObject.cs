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
                if (grid != null)
                    grid.DeregisterObject(this);

                volume.Clear();
                foreach (Vector3Int v in vol)
                    volume.Add(v);

                if (grid != null)
                    grid.RegisterObject(this);
            }

            public void MergeVolume(IEnumerable<Vector3Int> vol) { MergeVolume(vol, Vector3Int.zero); }
            public virtual void MergeVolume(IEnumerable<Vector3Int> vol, Vector3Int offset)
            {
                // [TODO] collision check?

                if (grid != null)
                    grid.DeregisterObject(this);

                foreach (Vector3Int v in vol)
                {
                    volume.Add(v + offset);
                }

                if (grid != null)
                    grid.RegisterObject(this);
            }

            public void Translate(Vector3Int offset)
            {
                if (offset == Vector3Int.zero)
                    return;
                Vector3Int newPosition = grid.GetPositionOf(this) + offset;
                MoveTo(newPosition);
            }

            public void MoveTo(Vector3Int position)
            {
                if (grid == null)
                    return;

                grid.DeregisterObject(this);
                grid.RegisterObject(this, position);
                transform.position = grid.ToWorldSpace(position);
            }

            public void Rotate(Vector3Int rotation)
            {
                if (grid == null)
                    return;
                
                while (rotation.z != 0) {
                    if (rotation.z > 0) {
                        HashSet<Vector3Int> newVolume = new HashSet<Vector3Int>();
                        foreach (Vector3Int p in volume) {
                            newVolume.Add(new Vector3Int(p.y, -p.x, p.z)); // CW
                        }
                        RemapVolume(newVolume);
                        rotation.z -= 1;
                    }
                    if (rotation.z < 0)
                    {
                        HashSet<Vector3Int> newVolume = new HashSet<Vector3Int>();
                        foreach (Vector3Int p in volume)
                        {
                            newVolume.Add(new Vector3Int(-p.y, p.x, p.z)); // CCW
                        }
                        RemapVolume(newVolume);
                        rotation.z += 1;
                    }
                }

                // [TODO] (1, 0, 0), (0, -1, 0), (0, 0, 2)
            }

            public void Scale(Vector3Int scale)
            {
                HashSet<Vector3Int> newVolume = new HashSet<Vector3Int>();
                foreach (Vector3Int p in volume)
                {
                    newVolume.Add(Vector3Int.Scale(scale, p)); // CCW
                }
                RemapVolume(newVolume);
            }
        }
    }
}