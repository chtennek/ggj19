using UnityEngine;
using System.Collections;

namespace Primitives
{
    namespace Input
    {
        public abstract class InputBehaviour : MonoBehaviour
        {
            public string actionName = "Action";

            public abstract void OnTrigger();
            public abstract void OnAxis2D(Vector2 input);
            public abstract void OnAxis2DDown(Vector2 input);
        }
    }
}