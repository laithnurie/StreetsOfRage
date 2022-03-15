using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioClip bossMusic;

    public bool PlayerIsInXBounds(float playerXPosition, float playerColliderWidth)
    {
        return level.GetLeftBounds() + playerColliderWidth <= playerXPosition  && playerXPosition <= level.GetRightBounds();
    }

    private void Start()
    {
        GameController.i.PlayMusic();
    }

    public AudioClip GetBackgroundMusic()
    {
        // some logic here to determine the correct audio clip, e.g. boss appears!
        return levelMusic;
    }
}
