using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private LevelController _levelController;
    
    public static GameController i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    public void SetLevelController(LevelController levelController)
    {
        _levelController = levelController;
    }
    public LevelController LevelController => _levelController;
}