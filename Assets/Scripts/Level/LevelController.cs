using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level _level;

    public bool PlayerIsInXBounds(float playerXPosition)
    {
        return _level.GetLeftBounds() <= playerXPosition  && playerXPosition <= _level.GetRightBounds();
    }
}
