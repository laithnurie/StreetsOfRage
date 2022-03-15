using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private LevelController _levelController;
    [SerializeField] private GameObject mainCamera;
    
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
    
    public void PlayMusic()
    {
        if (_levelController == null) return;
        var audioSource = mainCamera.GetComponent<AudioSource>();
        audioSource.clip = _levelController.GetBackgroundMusic();
        audioSource.Play();

    }
}