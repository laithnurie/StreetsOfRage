using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjectsHolder : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetLevelController(LevelController levelController)
    {
        gameController.SetLevelController(levelController);
    }
}
