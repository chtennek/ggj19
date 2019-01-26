namespace Primitives
{
    namespace Grid
    {
        using System.Collections;
        using System.Collections.Generic;

        using UnityEngine;

        public class GridObject : MonoBehaviour
        {
            public List<Vector3Int> volume = new List<Vector3Int> { Vector3Int.zero };
            public GameGrid grid;

            public virtual void RemapVolume(List<Vector3Int> vol) {}

            public void RegisterObject(GameGrid g) {
                if (g.IsColliding(this) == true)
                    return;
                
                if (grid != null)
                    grid.DeregisterObject(this);

                grid = g;
                g.RegisterObject(this);
            }

            public void DeregisterObject(GameGrid g) {
                g.DeregisterObject(this);
            }
        }
    }
}