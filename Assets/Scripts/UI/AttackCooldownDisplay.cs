using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCooldownDisplay : MonoBehaviour
{
    public Image Bar;

    private void Start()
    {
        PlayerAttack.AttackCooldownLeftPercent += SetCooldownPercent;
    }

    private void OnDestroy()
    {
        PlayerAttack.AttackCooldownLeftPercent -= SetCooldownPercent;
    }

    public void SetCooldownPercent(float f)
    {
        Bar.fillAmount = 1-f;
    }
    
}
