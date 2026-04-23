using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleShuffler : MonoBehaviour
{
    public List<Transform> puzzlePieces; // Assign 6 puzzle pieces in the inspector

    void Start()
    {
        ShufflePositions();
    }

    void ShufflePositions()
    {
        // Store the original positions
        List<Vector3> positions = new List<Vector3>();
        foreach (Transform piece in puzzlePieces)
        {
            positions.Add(piece.position);
        }

        // Shuffle the positions
        for (int i = 0; i < positions.Count; i++)
        {
            int randomIndex = Random.Range(0, positions.Count);
            Vector3 temp = positions[i];
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        // Assign shuffled positions back to the puzzle pieces
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzlePieces[i].position = positions[i];
        }
    }
}

