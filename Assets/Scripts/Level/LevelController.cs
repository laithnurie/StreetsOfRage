using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level level;

    public bool PlayerIsInXBounds(float playerXPosition, float playerColliderWidth)
    {
        return level.GetLeftBounds() + playerColliderWidth <= playerXPosition  && playerXPosition <= level.GetRightBounds();
    }
}
