using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static Action LevelComplete;
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
            LevelManager.Instance.IncreaseLevel();
            LevelComplete?.Invoke();
            DOVirtual.DelayedCall(4f, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });

        }
    }
    
    [Button]
    public void ReadSpawners()
    {
        Spawners.Clear();
        var sp = gameObject.GetComponentsInChildren<EnemySpawner>();
        Spawners.AddRange(sp);
        
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
