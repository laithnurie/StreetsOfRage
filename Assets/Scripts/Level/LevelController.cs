using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level _level;

    public bool PlayerIsInXBounds(float playerXPosition, float playerColliderWidth)
    {
        return _level.GetLeftBounds() + playerColliderWidth <= playerXPosition  && playerXPosition <= _level.GetRightBounds();
    }
}
