using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Primitives.Core;
using Primitives.Grid;

public class PieceQueue : MonoBehaviour
{
    public GameObject bag;
    public int iterations = 15;

    private List<GridObject> queue;
    private int currentIndex;

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        List<GridObject> bagPieces = new List<GridObject>(bag.GetComponents<GridObject>());
        queue = new List<GridObject>();

        for (int i = 0; i < iterations; i++)
        {
            bagPieces.Shuffle();
            foreach (GridObject piece in bagPieces)
                queue.Add(piece);
        }

        currentIndex = 0;
    }

    public GridObject Peek(int offset = 0) {
        int index = currentIndex + offset;
        if (index < 0 || index >= queue.Count)
            return null;
        return queue[index];
    }

    public GridObject Dequeue() {
        currentIndex++;
        if (currentIndex >= queue.Count)
            return null;
        return queue[currentIndex - 1];
    }
}
