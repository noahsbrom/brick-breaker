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

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // ReadValue will return -1 or 1 depending on input direction
        move.action.performed += ctx => _targetVelocityX = ctx.ReadValue<float>() * _speed;
        move.action.canceled += _ => _targetVelocityX = 0;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            Mathf.Lerp(rb.linearVelocityX, _targetVelocityX, _acceleration * Time.fixedDeltaTime), 0
        );
    }
}
