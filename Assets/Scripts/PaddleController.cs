using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.MemoryMappedFiles;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public InputActionReference move;
    public Boundaries boundaries;
    public Rigidbody2D rb;
    private readonly float _speed = 20f;
    private readonly float _acceleration = 5f;
    private float _targetVelocityX = 0f;
    private float _paddleWidth = 0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _paddleWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        // ReadValue will return -1 or 1 depending on input direction
        move.action.performed += ctx => _targetVelocityX = ctx.ReadValue<float>() * _speed;
        move.action.canceled += _ => _targetVelocityX = 0;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, _targetVelocityX, _acceleration * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Get the reflection direction of the ball after it has collided with the paddle. The
    /// reflection direction is determined by the position of contact on the paddle. For example, 
    /// if the ball hits the left half of the paddle, the ball will be reflected to the left, with a 
    /// sharper angle the farther from the center the contact occurs.
    /// </summary>
    /// <param name="relativeContactPointX">the contact point relative to the paddle position</param>
    /// <returns>reflection direction of the ball</returns>
    private Vector2 GetReflectionDirection(float relativeContactPointX)
    {
        // reflection angle restrictions in degrees
        float rightAngleLimit = 25f;
        float leftAngleLimit = 180f - rightAngleLimit;

        float _paddleWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        float conversionRatio = (rightAngleLimit - leftAngleLimit) / _paddleWidth;
        float maxX = _paddleWidth / 2;

        float newAngle = leftAngleLimit + ((relativeContactPointX + maxX) * conversionRatio);

        // rotate angle to be relative to y axis
        float radians = newAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ("Ball" == other.gameObject.name)
        {
            // calculate desired reflection angle based on where the ball hit the paddle
            float relativeContactPointX = other.GetContact(0).point.x - gameObject.transform.position.x;
            Vector2 direction = GetReflectionDirection(relativeContactPointX);

            // set new ball velocity
            BallController bc = other.gameObject.GetComponent<BallController>();
            bc.rb.linearVelocity = bc.speed * direction;
        }
    }
}
