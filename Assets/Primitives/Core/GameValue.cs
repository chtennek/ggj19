using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primitives {
    namespace Core {
        public class GameValue : MonoBehaviour
        {
            public event EventHandler ValueChanged;

            public string label;
            public float minValue = -Mathf.Infinity;
            public float maxValue = Mathf.Infinity;
            public bool clampMin = true;
            public bool clampMax = true;
            public bool roundToInt = true;

            [SerializeField]
            private float _value = 0; // Initial value
            public float Value
            {
                get { return _value; }
                set
                {
                    _value = Mathf.Clamp(value, minValue, maxValue);
                    if (roundToInt)
                        _value = Mathf.RoundToInt(_value); // [TODO] reclamp
                    OnValueChanged();
                }
            }
            public float ValuePercent
            {
                get { return (Mathf.Approximately(Value, 0)) ? 0 : Value / maxValue; }
                set
                {
                    Value = value * maxValue;
                }
            }

            protected void OnValueChanged() {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
            }
        }
    }
}