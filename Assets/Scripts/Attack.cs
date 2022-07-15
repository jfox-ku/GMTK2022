using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Tweener Tweener;

    public void SetAttackTarget(Transform target)
    {
        Tweener.UpdateTarget(target);
    }


    private void OnDestroy()
    {
        
    }
}
