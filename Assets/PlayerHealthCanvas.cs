using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthCanvas : MonoBehaviour
{
    public Image Hearts;

    private void Awake()
    {
        SetFill(1f);
        Destroyable.PlayerPercentHealthLeft += SetFill;
    }

    private void OnDestroy()
    {
        Destroyable.PlayerPercentHealthLeft -= SetFill;
    }

    public void SetFill(float f)
    {
        Hearts.fillAmount = f;
    }
}
