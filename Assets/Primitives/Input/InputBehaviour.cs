using UnityEngine;
using System.Collections;

namespace Primitives
{
    namespace Input
    {
        public abstract class InputBehaviour : MonoBehaviour
        {
            public string actionName = "Position";

            public abstract void OnTrigger();
            public abstract void OnInput(Vector2 input);
        }
    }
}