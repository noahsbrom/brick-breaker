using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public SpriteRenderer sr;
    private int _hitsRemaining = 3;
    private Dictionary<int, Color> _colorMap;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _colorMap = new Dictionary<int, Color>
        {
            {3, Color.black},
            {2, Color.green},
            {1, Color.white}
        };
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        sr.color = _colorMap[_hitsRemaining];
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _hitsRemaining--;
        if (0 >= _hitsRemaining)
        {
            Destroy(this.gameObject);
            LevelManager.instance.HandleBrickDeleted();
        }
        else 
        {
            sr.color = _colorMap[_hitsRemaining];
        }
    }
}
