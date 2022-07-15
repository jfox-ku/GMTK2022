using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [TitleGroup("Movement")] public Rigidbody RB;
    [TitleGroup("Movement")] public float MoveSpeed = 1f;

    [TitleGroup("Movement"), Range(0f, 1f)]
    public float RotationPercent;

    private bool usedDash;
    [SerializeField, TitleGroup("Dash")] private float DashCooldown;
    private float dashCooldownLeft;
    [SerializeField, TitleGroup("Dash")] private float DashDuration, DashSpeed;
    public AnimationCurve DashMoveCurve;
    [SerializeField] public ParticleSystem DashParticle;
    [SerializeField] private Vector3 DashParticleStartPositionOffset;

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
        RB.velocity = Vector3.Lerp(RB.velocity, input.normalized * MoveSpeed, 0.5f);
        if (RB.velocity.magnitude > 0.1f)
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
        while (Time.time < startTime + DashDuration)
        {
            var timeDiff = Time.time - startTime;
            var percent = Mathf.Clamp01(timeDiff / DashDuration);
            RB.velocity = RB.transform.forward * DashSpeed * DashMoveCurve.Evaluate(percent);
            UpdateFaceDirection(0.35f);
            yield return null;
        }

        UpdateVelocity(Vector3.forward);
    }
}