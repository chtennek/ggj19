using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Primitives.Core;
using Primitives.Input;

public class GameFlowManager : InputBehaviour
{
    // game
    public GameValue score;
    public GameValue piecesLeft;
    public GameObject[] destroyOnQueueEmpty;

    // submit
    public Text nameInput;
    public Text scoreDisplay;
    public static string playerName;
    public static int playerScore = 0;

    public string nextScene;

    public void Awake()
    {
        if (piecesLeft != null)
            piecesLeft.ValueChanged += OnValueChanged;
        if (scoreDisplay != null)
            scoreDisplay.text = playerScore.ToString();
    }

    public override void OnTrigger()
    {
        if (enabled && (piecesLeft == null || piecesLeft.Value == 0)) {
            if (score != null)
                playerScore = Mathf.RoundToInt(score.Value);
            if (nameInput != null) {
                playerName = nameInput.text;
                Debug.Log("Submitting score...");
            }
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnValueChanged(object source, EventArgs args)
    {
        if (piecesLeft != null && piecesLeft.Value == 0) {
            Debug.Log("Game over");
            foreach (GameObject g in destroyOnQueueEmpty) {
                Destroy(g);
            }


            // Submit score

            // Switch to score scene
        }
    }

    public override void OnAxis2D(Vector2 input)
    {
    }

    public override void OnAxis2DDown(Vector2 input)
    {
    }
}
