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
        public abstract class ValueDisplay : MonoBehaviour
        {
            public GameValue value;

            [Header("Tweening")]
            public int updatesPerSecond = 60;
            public float minDelta = 0.1f;
            public float maxDelta = Mathf.Infinity;
            public float lerpValue = 1f;

            public int tickSoundsPerSecond = 10;
            public string tickSound;

            protected float displayValue; // current mid-lerp display value
            private IEnumerator coroutine;

            protected virtual void Reset() {
                value = GetComponent<GameValue>();
            }

            protected virtual void Start()
            {
                if (value == null)
                    enabled = false;

                if (enabled) {
                    value.ValueChanged += OnValueChanged;
                    SyncDisplay();
                }
            }

            private void OnValueChanged(object source, EventArgs args)
            {
                StopCoroutine(coroutine);
                coroutine = Coroutine_UpdateDisplay();
                StartCoroutine(coroutine);
            }

            // Stop lerping and force the value display to the actual value
            public void SyncDisplay()
            {
                StopCoroutine(coroutine);
                displayValue = value.Value;
                UpdateDisplay();
            }

            protected abstract void UpdateDisplay();

            private void UpdateDisplayValue()
            {
                float lerpedValue = Mathf.Lerp(displayValue, value.Value, lerpValue);
                float minDeltaValue = Mathf.MoveTowards(displayValue, value.Value, minDelta);
                float maxDeltaValue = Mathf.MoveTowards(displayValue, value.Value, maxDelta);
                if (lerpedValue <= value.Value)
                    displayValue = Mathf.Clamp(displayValue, minDeltaValue, maxDeltaValue);
                else
                    displayValue = Mathf.Clamp(displayValue, maxDeltaValue, minDeltaValue);
            }

            private IEnumerator Coroutine_UpdateDisplay()
            {
                while (Mathf.Approximately(displayValue, value.Value) == false)
                {
                    UpdateDisplayValue();
                    UpdateDisplay();
                    yield return new WaitForSeconds(1f / updatesPerSecond);
                } 
            }
        }
    }
}