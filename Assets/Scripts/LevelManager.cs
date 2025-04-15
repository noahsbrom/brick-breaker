using System;
using System.Data;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // singleton
    public static LevelManager instance;

    public GameObject brickPrefab;
    public Boundaries boundaries;
    // private int _currentLevel = 1;
    private int _brickCount = 0;
    private float _brickGap = 0.20f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        SetBricks();
    }

    /// <summary>
    /// Initialize the brick layout for the level
    /// </summary>
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

    /// <summary>
    /// Set a brick in the scene at a particular x,y coordinate
    /// </summary>
    /// <param name="x">x coord of the brick to set</param>
    /// <param name="y">y coord of the brick to set</param>
    private void SetBrick(float x, float y)
    {
        Instantiate(brickPrefab, new Vector3(x, y, 0), Quaternion.identity);
        _brickCount++;
    }

    /// <summary>
    /// Decrement the count of total bricks once a brick has been deleted
    /// </summary>
    public void HandleBrickDeleted() 
    {
        _brickCount--;
        if (_brickCount <= 0)
        {
            HandleLevelCompleted();
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    private void HandleLevelCompleted()
    {
        // TODO
        Debug.Log("GAME OVER!!!!");
    }
}
