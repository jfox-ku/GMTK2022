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
    private int AliveCount;
    public Action SpawnerCompleteEvent;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(SpawnDelay);
        currentSpawnedCount = 0;
        StartCoroutine(Spawn());

    }

    public void EnemyDestoyed()
    {
        AliveCount--;
        if (AliveCount == 0 && currentSpawnedCount == SpawnCount)
        {
            SpawnerCompleteEvent?.Invoke();
        }
    }

    public IEnumerator Spawn()
    {
        AliveCount = 0;
        var startTime = Time.time;
        while (currentSpawnedCount < SpawnCount)
        {
            if (startTime + SpawnCooldown < Time.time)
            {
                startTime = Time.time;
                currentSpawnedCount++;
                AliveCount++;
                var enemy = Instantiate(EnemyBase,transform);
                enemy.transform.localPosition = Vector3.zero;
                enemy.SetEnemyType(SpawnIndex);
                enemy.SetEnemyTarget(PlayerController.PlayerDice);
                enemy.EnemyDestroyedEvent += EnemyDestoyed;
            }
            
            yield return null;
        }
    }
}
