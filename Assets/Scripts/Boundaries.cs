using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera cam;
    public GameObject topWall, bottomWall, leftWall, rightWall;
    public float topBoundary, bottomBoundary, leftBoundary, rightBoundary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SetBoundaries();
    }

    /// <summary>
    /// set edge collider for each wall based on the camera height and width
    /// </summary>
    private void SetBoundaries() 
    {
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;
        
        topBoundary = height/2;
        bottomBoundary = -height/2;
        leftBoundary = -width/2;
        rightBoundary = width/2;
        
        EdgeCollider2D topCollider = topWall.AddComponent<EdgeCollider2D>();
        topCollider.points = new Vector2[] 
        {
            new Vector2(leftBoundary, topBoundary),
            new Vector2(rightBoundary, topBoundary)
        };

        EdgeCollider2D bottomCollider = bottomWall.AddComponent<EdgeCollider2D>();
        bottomCollider.points = new Vector2[] 
        {
            new Vector2(leftBoundary, bottomBoundary),
            new Vector2(rightBoundary, bottomBoundary)
        };

        EdgeCollider2D leftCollider = leftWall.AddComponent<EdgeCollider2D>();
        leftCollider.points = new Vector2[] 
        {
            new Vector2(leftBoundary, bottomBoundary),
            new Vector2(leftBoundary, topBoundary)
        };

        EdgeCollider2D rightCollider = rightWall.AddComponent<EdgeCollider2D>();
        rightCollider.points = new Vector2[] 
        {
            new Vector2(rightBoundary, bottomBoundary),
            new Vector2(rightBoundary, topBoundary)
        };
    }
}
