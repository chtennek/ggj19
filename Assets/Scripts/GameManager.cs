using System;
using UnityEngine;

using Primitives.Core;

public class GameManager : MonoBehaviour
{
    public GameValue piecesLeft;

    public void Awake()
    {
        piecesLeft.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(object source, EventArgs args)
    {
        if (piecesLeft.Value == 0) {
            Debug.Log("Game over");

            // Submit score

            // Switch to score scene
        }
    }
}
