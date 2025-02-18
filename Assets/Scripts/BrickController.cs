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

    public void HandleCollision()
    {
        _hitsRemaining--;
        if (0 == _hitsRemaining)
        {
            Debug.Log("BRICK FINISHED");
        }
        else 
        {
            sr.color = _colorMap[_hitsRemaining];
        }

    }
}
