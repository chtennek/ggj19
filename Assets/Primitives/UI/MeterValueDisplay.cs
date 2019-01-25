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
        public class MeterValueDisplay : ValueDisplay
        {
            public Image display;

            public bool overrideColor = false;
            public Gradient displayColor;

            protected override void Reset()
            {
                base.Reset();
                display = GetComponent<Image>();
            }

            protected override void Start()
            {
                enabled &= (display != null);
                base.Start();
            }

            protected override void UpdateDisplay()
            {
                display.fillAmount = 0;
                if (Mathf.Approximately(value.maxValue, 0) == false)
                    display.fillAmount = displayValue / value.maxValue;
                                      
                if (overrideColor == true)
                    display.color = displayColor.Evaluate(value.ValuePercent);
            }
        }
    }
}