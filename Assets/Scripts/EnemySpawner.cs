using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SO;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemies AllEnemies;
    [AssetsOnly]
    public Enemy EnemyBase;
    public float SpawnDelay;
    public int SpawnIndex;
    public float SpawnCooldown;
    public int SpawnCount;
   

    private int currentSpawnedCount;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(SpawnDelay);
        currentSpawnedCount = 0;
        StartCoroutine(Spawn());

    }

    public IEnumerator Spawn()
    {
        var startTime = Time.time;
        while (currentSpawnedCount < SpawnCount)
        {
            if (startTime + SpawnCooldown < Time.time)
            {
                startTime = Time.time;
                currentSpawnedCount++;
                var enemy = Instantiate(EnemyBase,transform);
                enemy.transform.localPosition = Vector3.zero;
                enemy.SetEnemyType(SpawnIndex);
                enemy.SetEnemyTarget(PlayerController.PlayerDice);
            }
            
            yield return null;
        }
    }
}
