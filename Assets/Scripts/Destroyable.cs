using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public bool isPlayer = false;
    public float health;
    public Action Destroyed;
    public static Action<float> PlayerPercentHealthLeft;

    private float maxHealth;
    private void Start()
    {
        maxHealth = health;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (isPlayer)
        {
            ParticleManager.Instance.PlayPlayerDamagedParticle(PlayerController.PlayerDice,Vector3.up);
            PlayerPercentHealthLeft?.Invoke(health/maxHealth);
        }
        if (!CheckAlive) Die();
        
    }

    private void Die()
    {
        Destroyed?.Invoke();
        Destroy(this.gameObject);
    }


    public bool CheckAlive => health > 0;
}
