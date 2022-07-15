using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody RB;
    public float MoveSpeed = 1f;

    private bool usedDash;
    [SerializeField,TitleGroup("Dash")] private float DashCooldown;
    private float dashCooldownLeft;
    [SerializeField,TitleGroup("Dash")] private float DashDuration, DashSpeed;

    void Update()
    {
        Vector3 moveInput = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveInput += Vector3.forward;
        if (Input.GetKey(KeyCode.A)) moveInput += Vector3.left;
        if (Input.GetKey(KeyCode.S)) moveInput += Vector3.back;
        if (Input.GetKey(KeyCode.D)) moveInput += Vector3.right;

        UpdateVelocity(moveInput);


        if(dashCooldownLeft>0) dashCooldownLeft -= Time.deltaTime;
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
    }

    public IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + DashDuration)
        {
            RB.velocity = RB.velocity.normalized * DashSpeed;
            yield return null;
        }

        usedDash = false;
    }
}