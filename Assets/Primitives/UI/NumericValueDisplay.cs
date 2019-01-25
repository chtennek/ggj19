using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Primitives.Core;

namespace Primitives
{
    namespace UI
    {
        public class NumericValueDisplay : ValueDisplay
        {
            [Header("UI")]
            public Text display;

            public bool integerDisplayOnly = true;
            public int zeroPadding = 0;

            protected override void Reset()
            {
                base.Reset();
                display = GetComponent<Text>();
            }

            protected override void Start()
            {
                enabled &= (display != null);
                base.Start();
            }

            public string GetDisplayString(float displayValue)
            {
                if (integerDisplayOnly)
                {
                    if (displayValue < value.Value)
                        displayValue = Mathf.CeilToInt(displayValue);
                    else
                        displayValue = Mathf.FloorToInt(displayValue);
                }
                string text = displayValue.ToString().PadLeft(zeroPadding, '0');
                return text;
            }

            protected override void UpdateDisplay()
            {
                display.text = GetDisplayString(displayValue);
            }
        }
    }
}