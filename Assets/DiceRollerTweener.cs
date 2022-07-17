using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class DiceRollerTweener : MonoBehaviour
{
    public Transform Source;
    public Vector3 Rotation;
    public float DurationMultiplier = 1f;
    public AnimationCurve TweenCurve;
    
    [Button]
    public Tween Flip()
    {
        return Source.DORotate(Rotation, PlayerController.PlayerDashDuration * DurationMultiplier).SetRelative(true).SetEase(TweenCurve);
    }
}
