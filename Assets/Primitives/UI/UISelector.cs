using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Primitives.Core;

namespace Primitives
{
    namespace UI
    {
        public class UISelector : MonoBehaviour, ITriggerable2D
        {
            public string triggerName = "Position";
            public string TriggerName { get { return triggerName; } }

            [Header("")]
            public Selectable selected;

            public void OnTrigger(Vector2 v)
            {
                MoveTo(v);
            }

            public void MoveTo(Vector2 offset)
            {
                // [TODO]
            }
        }
    }
}
