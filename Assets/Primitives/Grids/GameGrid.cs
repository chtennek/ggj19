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

            public string initializeWithTag = "Untagged";
            private Dictionary<GridObject, Vector3Int> objPositions;
            private Dictionary<Vector3Int, HashSet<GridObject>> collisionMap; // Performance?

            private void Awake()
            {
                objPositions = new Dictionary<GridObject, Vector3Int>();
                collisionMap = new Dictionary<Vector3Int, HashSet<GridObject>>();

                foreach (GridObject o in FindObjectsOfType<GridObject>())
                {
                    if (initializeWithTag == o.tag)
                        RegisterObject(o, ToGridSpace(o));
                }
            }

            private void SetGridPoint(Vector3Int position, GridObject o, bool filled)
            {
                if (collisionMap.ContainsKey(position) == false)
                    collisionMap[position] = new HashSet<GridObject>();

                if (filled)
                    collisionMap[position].Add(o);
                else
                    collisionMap[position].Remove(o);
            }

            public HashSet<GridObject> GetCollisionsAt(Vector3Int p)
            {
                if (collisionMap.ContainsKey(p) == false)
                    return new HashSet<GridObject>();
                return collisionMap[p];
            }

            public Vector3Int GetPositionOf(GridObject o)
            {
                return objPositions[o];
            }

            public void RemapVolume(GridObject o, IEnumerable<Vector3Int> volume)
            {
                Vector3Int p = objPositions[o];
                HashSet<Vector3Int> v1 = o.volume;
                HashSet<Vector3Int> v2 = new HashSet<Vector3Int>(volume);
                o.volume = v2;
                if (IsColliding(o) == true)
                {
                    o.volume = v1;
                    return;
                }

                o.volume = v1;
                DeregisterObject(o);
                o.volume = v2;
                RegisterObject(o, p);
            }

            public bool IsColliding(GridObject o) { return IsColliding(ToGridSpace(o), o); }
            public bool IsColliding(Vector3Int position, GridObject o = null)
            {
                foreach (Vector3Int offset in o.volume)
                {
                    HashSet<GridObject> objectsInCell = GetCollisionsAt(position + offset);
                    int check = objectsInCell.Contains(o) ? 1 : 0; // Don't count ourselves if we're on the grid
                    if (objectsInCell.Count > check)
                        return true;
                }
                return false;
            }

            public bool RegisterObject(GridObject o, Vector3Int position, bool force = false)
            {
                if (force == false && IsColliding(position, o) == true)
                    return false;

                o.grid = this;
                objPositions[o] = position;
                foreach (Vector3Int offset in o.volume)
                    SetGridPoint(position + offset, o, true);
                return true;
            }

            public void DeregisterObject(GridObject o)
            {
                Vector3Int position = objPositions[o];

                o.grid = null;
                objPositions.Remove(o);
                foreach (Vector3Int offset in o.volume)
                    SetGridPoint(position + offset, o, true);
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