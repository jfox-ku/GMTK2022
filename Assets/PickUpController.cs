using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public PickUpDataSO Data;
    public static Action<Vector3> ParticlePickedUpPositionEvent;

    public GameObject SpawnedObject;
    public bool Respawn = false;
    public float SpawnDelay, RespawnDelay;
    public int MaxSpawnCount = 3;
    private int currentSpawnCount = 0;

    private IEnumerator Start()
    {
        currentSpawnCount = 0;
        if (SpawnedObject != null) DestroyImmediate(SpawnedObject);
        yield return new WaitForSeconds(SpawnDelay);
        Spawn();
    }

    [Button]
    public void Spawn()
    {
        if (currentSpawnCount >= MaxSpawnCount) return;
        if (SpawnedObject != null) DestroyImmediate(SpawnedObject);

        SpawnedObject = Data.Spawn();
        currentSpawnCount++;
        SpawnedObject.transform.parent = transform;
        SpawnedObject.transform.localPosition = Vector3.zero;
    }

    public void Apply(PlayerController p)
    {
        ParticlePickedUpPositionEvent?.Invoke(transform.position);
        if (Respawn) DOVirtual.DelayedCall(RespawnDelay, Spawn);
        Data.Apply(p.Attacker.AttackData);
        Destroy(SpawnedObject);
    }
}