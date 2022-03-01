using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private MainObjectsHolder mainObjectsHolder;
    [SerializeField] private GameObject currentSceneSpawnPoint;
    [SerializeField] private LevelController levelController;

    private void Awake()
    {
        var objectsHolder = FindObjectsOfType<MainObjectsHolder>();
        if (objectsHolder.Length != 0) return;
        var spawnPos = Vector3.zero;

        if (currentSceneSpawnPoint != null)
            spawnPos = currentSceneSpawnPoint.transform.position;

        var newObjectsHolder = Instantiate(mainObjectsHolder, spawnPos, Quaternion.identity);
        newObjectsHolder.SetLevelController(levelController);
    }
}