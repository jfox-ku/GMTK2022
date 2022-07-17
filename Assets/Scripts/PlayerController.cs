using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController PlayerInstance;
    public static float PlayerDashDuration => PlayerInstance.DashDuration;
    
    [TitleGroup("Health")]
    public Destroyable Dest;

    [TitleGroup("Health")] public float ImmunityDuration, ImmunityCooldown, LastImmunityTime;
    [TitleGroup("Movement")] public Rigidbody RB;
    [TitleGroup("Movement")] public float MoveSpeed = 1f;

    [TitleGroup("Movement"), Range(0f, 1f)]
    public float RotationPercent;

    public DiceRollerTweener Dice;

    private bool usedDash;
    [SerializeField, TitleGroup("Dash")] private float DashCooldown;
    private float dashCooldownLeft;
    [SerializeField, TitleGroup("Dash")] private float DashDuration, DashSpeed;
    public AnimationCurve DashMoveCurve;
    [SerializeField] public ParticleSystem DashParticle;
    [SerializeField] private DiceNumberController DiceNumberController;

    public static Transform PlayerDice => PlayerInstance.RB.transform;
    
    [ShowInInspector]
    public bool IsImmune => LastImmunityTime + ImmunityDuration > Time.time;
    [ShowInInspector]
    public bool IsImmuneInCooldown => LastImmunityTime + ImmunityCooldown > Time.time;
    
    private void Awake()
    {
        //Dest.health = 100f;
        Dest.isPlayer = true;
        Dest.Destroyed += PlayerDied;
        PlayerInstance = this;
    }

    private void Start()
    {
        DiceNumberController.SetRandomTop();
    }

    public void PlayerDied()
    {
        Dest.Destroyed -= PlayerDied;
        
        DOVirtual.DelayedCall(2f, () =>
        {
            LevelManager.ReloadScene();
        });
    }

    void Update()
    {
        Vector3 moveInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveInput += Vector3.forward;
        if (Input.GetKey(KeyCode.A)) moveInput += Vector3.left;
        if (Input.GetKey(KeyCode.S)) moveInput += Vector3.back;
        if (Input.GetKey(KeyCode.D)) moveInput += Vector3.right;
        UpdateVelocity(moveInput);


        UpdateDashParticleDirection();

        if (dashCooldownLeft > 0) dashCooldownLeft -= Time.deltaTime;
        else usedDash = false;
        if (Input.GetKey(KeyCode.Space) && !usedDash)
        {
            if (dashCooldownLeft <= 0)
            {
                usedDash = true;
                dashCooldownLeft = DashCooldown;
                StartCoroutine(Dash());
            }
        }
    }

    public void UpdateVelocity(Vector3 input)
    {
        var savedY = RB.velocity.y;
        RB.velocity = Vector3.Lerp(RB.velocity, input.normalized * MoveSpeed, 0.5f);
        RB.velocity.ChangeY(savedY);
        if (RB.velocity.magnitude > 0.15f)
        {
            UpdateFaceDirection(RotationPercent);
        }
    }

    private void UpdateFaceDirection(float percent)
    {
        if (RB.velocity.magnitude > 0.05f)
        {
            RB.rotation = Quaternion.Lerp(RB.transform.rotation, Quaternion.LookRotation(RB.velocity), percent);
        }
    }

    private void UpdateDashParticleDirection()
    {
        DashParticle.transform.position = RB.position + -RB.transform.forward;
        if (RB.velocity.magnitude >= 0.05f) DashParticle.transform.rotation = Quaternion.LookRotation(-RB.velocity);
    }

    public IEnumerator Dash()
    {
        float startTime = Time.time;
        DashParticle.Play();
        Dice.Flip().OnComplete(()=>
        {
            DiceNumberController.SetRandomTop();
            Dice.Source.transform.localEulerAngles = Vector3.zero;
            var temp = transform.localEulerAngles;
            temp[0] = 0f;
            transform.localEulerAngles = temp;

        });
        
        while (Time.time < startTime + DashDuration)
        {
            var timeDiff = Time.time - startTime;
            var percent = Mathf.Clamp01(timeDiff / DashDuration);
            RB.velocity = RB.transform.forward * DashSpeed * DashMoveCurve.Evaluate(percent);
            UpdateFaceDirection(0.35f);
            yield return null;
        }
        
        UpdateVelocity(Vector3.zero);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            Dest.TakeDamage(5);
        }
    }
}