using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<EnemySpawner> Spawners;
    public int spawnersLeft;
    
    private void Start()
    {
        StartLevel();
    }

    public void SpawnerComplete()
    {
        spawnersLeft--;
        if (spawnersLeft == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void StartLevel()
    {
        spawnersLeft = Spawners.Count;
        for (int i = 0; i < Spawners.Count; i++)
        {
            var s = Spawners[i];
            s.SpawnerCompleteEvent += SpawnerComplete;
        }
    }
}
