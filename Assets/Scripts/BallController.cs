using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb;
    public readonly float speed = 10f;
    private int _lives = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(.5f, 1f));
        Vector2 direction = new Vector2(0, 1);
        rb.linearVelocity = speed * direction;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "BottomWall")
        {
            _lives--;
        }
    }

}
