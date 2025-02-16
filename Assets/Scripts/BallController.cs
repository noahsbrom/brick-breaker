using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallCotroller : MonoBehaviour
{
    public Rigidbody2D rb;
    private readonly float _speed = 10f;
    private int _lives = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(.5f, 1f));
        rb.linearVelocity = _speed * direction;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objectHit = collision.gameObject.name;
        if ("LeftWall" == objectHit || "RightWall" == objectHit)
        {
            rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
        }
        else if ("TopWall" == objectHit || "Paddle" == objectHit)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
        }
        else if ("Brick" == objectHit)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
            BrickController bc = collision.GetComponent<BrickController>();
            bc.HandleCollision();
        }
        else 
        {
            _lives--;
        }
    }

}
