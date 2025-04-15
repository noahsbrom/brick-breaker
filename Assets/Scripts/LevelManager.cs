using System.Data;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject brickPrefab;
    public Boundaries boundaries;
    // private int _currentLevel = 1;
    private float _brickGap = 0.25f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        SetBricks();
    }

    private void SetBricks()
    {
        int rows = 2;
        int cols = 8;
        float brickWidth = brickPrefab.transform.localScale.x;
        float brickHeight = brickPrefab.transform.localScale.y;
        float totalBricksWdith = (cols * brickWidth) + ((cols - 1) * _brickGap);
        float boundaryGap = (boundaries.rightBoundary - boundaries.leftBoundary - totalBricksWdith) / 2;

        float x, y;
        for (int row = 0; row < rows; row++)
        {   
            y = boundaries.topBoundary - (boundaryGap + (brickHeight / 2) + (row * (brickHeight + _brickGap)));
            for (int col = 0; col < cols; col++)
            {
                x = boundaries.leftBoundary + boundaryGap + (brickWidth / 2) + (col * (brickWidth + _brickGap));
                SetBrick(x, y);
            }
        }
    }

    private void SetBrick(float x, float y)
    {
        Instantiate(brickPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
