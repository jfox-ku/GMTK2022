using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float health;
    public Action Destroyed;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (!CheckAlive) Die();
    }

    private void Die()
    {
        Destroyed?.Invoke();
        Destroy(this.gameObject);
    }


    public bool CheckAlive => health > 0;
}
