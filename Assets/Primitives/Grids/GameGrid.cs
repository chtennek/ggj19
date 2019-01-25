namespace Primitives
{
    namespace Grid
    {
        using System.Collections;
        using System.Collections.Generic;

        using UnityEngine;

        public class GameGrid : MonoBehaviour
        {
            public Vector3 gridScale = Vector3.one;
            public Vector3 gridOffset = Vector3.zero;

            public LayerMask layerMask = ~1;
            private Dictionary<GridObject, Vector3Int> objPositions;
            private Dictionary<Vector3Int, HashSet<GridObject>> collisionMap; // Unused: performance issues?

            private void Awake()
            {
                collisionMap = new Dictionary<Vector3Int, HashSet<GridObject>>();
                foreach (GridObject o in FindObjectsOfType<GridObject>()) {
                    if ((layerMask & o.gameObject.layer) > 0)
                        RegisterObject(o);
                }
            }

            private void SetGridPoint(Vector3Int position, GridObject o, bool filled) {
                if (collisionMap.ContainsKey(position) == false)
                    collisionMap[position] = new HashSet<GridObject>();

                if (filled)
                    collisionMap[position].Add(o);
                else
                    collisionMap[position].Remove(o);
            }

            public void Translate(GridObject o, Vector3Int offset) {
                // [TODO]
            }

            public void Rotate(GridObject o, Vector3Int rotation) {
                // [TODO] (1, 0, 0), (0, -1, 0), (0, 0, 2)
            }

            public void Scale(GridObject o, Vector3Int scale) {
                // [TODO] (1, -1, 1), (1, 2, 1), (0, 0, 0)
            }

            public void RemapVolume(GridObject o, List<Vector3Int> volume) {
                List<Vector3Int> v = o.volume;
                o.volume = volume;
                if (IsColliding(o) == true) {
                    o.volume = v;
                    return;
                }

                o.volume = v;
                DeregisterObject(o);
                o.volume = volume;
                RegisterObject(o);
            }

            public bool IsColliding(GridObject o) { return IsColliding(ToGridSpace(o), o); }
            public bool IsColliding(Vector3 position, GridObject o = null) {
                // [TODO] remember, can't collide with itself
                return false;
            }

            public bool RegisterObject(GridObject o, bool force = false) {
                Vector3Int position = ToGridSpace(o);
                if (force == false && IsColliding(position, o) == true)
                    return false;

                objPositions[o] = position;
                foreach (Vector3Int offset in o.volume)
                    SetGridPoint(position, o, true);
                return true;
            }

            public void DeregisterObject(GridObject o)
            {
                Vector3Int position = ToGridSpace(o);

                objPositions.Remove(o);
                foreach (Vector3Int offset in o.volume)
                    SetGridPoint(position, o, true);
            }

            public Vector3Int ToGridSpace(GridObject o) { return ToGridSpace(o.transform.position); }
            public Vector3Int ToGridSpace(Vector3 position)
            {
                Vector3 inverseGridSize = new Vector3(1 / gridScale.x, 1 / gridScale.y, 1 / gridScale.z);
                return Vector3Int.RoundToInt(Vector3.Scale(inverseGridSize, position - gridOffset));
            }

            public Vector3 ToWorldSpace(Vector3Int coordinates)
            {
                return Vector3.Scale(coordinates, gridScale) + gridOffset;
            }
        }
    }
}