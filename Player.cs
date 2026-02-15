using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    [Header("Move Info")]
    [SerializeField] private float xSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;

    private float dashTime;
    private float dashCooldownTimer;

    private float xInput;

    [Header("Attack Info")]
    [SerializeField] private bool isAttacking;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        Movement();
        CheckInput();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

        AnimatorController();
    }

    public void AttackOver() {
        isAttacking = false;
    }

    private void CheckInput() {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            DashAbility();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            isAttacking = true;
        }
    }

    private void DashAbility() {
        // Debug.Log("Dash key down.");
        if (dashCooldownTimer < 0) {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    private void Movement() {
        // rb.velocity = new Vector2(xInput * xSpeed, rb.velocity.y);
        if (dashTime > 0) {
            rb.velocity = new Vector2(facingDirection * dashSpeed, 0);
        }
        else {
            rb.velocity = new Vector2(xInput * xSpeed, rb.velocity.y);
        }
    }

    private void Jump() {
        if (isGrounded && !isAttacking) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void AnimatorController() {
        bool isRunning = (rb.velocity.x != 0);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGround", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
    }
}
