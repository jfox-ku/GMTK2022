using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    public GameObject HitEffect, BulletExpEffect, ChipDestroyEffect, PlayerDamagedParticle;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void PlayHitEffect(Transform target, Vector3 offset)
    {
        var effect = Instantiate(HitEffect, transform);
        effect.transform.position = target.transform.position + offset;
    }

    public void BulletExpireEffect(Transform target, Vector3 offset)
    {
        var effect = Instantiate(BulletExpEffect, transform);
        effect.transform.position = target.transform.position + offset;
    }

    public void PlayChipDestoyEffect(Transform target, Vector3 offset, Vector3 scale)
    {
        var effect = Instantiate(ChipDestroyEffect, transform);
        effect.transform.position = target.transform.position + offset;
        effect.transform.localScale = scale;
    }

    public void PlayPlayerDamagedParticle(Transform target, Vector3 offset)
    {
        var effect = Instantiate(PlayerDamagedParticle, transform);
        effect.transform.position = target.transform.position + offset;
        StartCoroutine(effect.transform.StickRoutine(target, offset, 1f));
    }
    
    
}