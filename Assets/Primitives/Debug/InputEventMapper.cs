using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Primitives.Core;

namespace Primitives
{
    namespace Debug
    {
        [System.Serializable]
        public class InputButtonEvent
        {
            public string buttonName;

            public UnityEvent onButtonDown;
            public UnityEvent onButton;
            public UnityEvent onButtonUp;
        }

        public class InputEventMapper : MonoBehaviour
        {
            public InputHandler input;
            public InputButtonEvent[] buttonEvents;

            void Update()
            {

                foreach (InputButtonEvent e in buttonEvents)
                {
                    if (input.GetButtonDown(e.buttonName))
                        e.onButtonDown.Invoke();

                    if (input.GetButton(e.buttonName))
                        e.onButton.Invoke();

                    if (input.GetButtonUp(e.buttonName))
                        e.onButtonUp.Invoke();
                }
            }
        }
    }
}
