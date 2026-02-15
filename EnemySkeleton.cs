using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Entity {

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [Space]
    [Header("Player Detection")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    private bool isAttacking;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        Movement();

        if (!isGrounded || isWallDetected) {
            Flip(); // change facingDirection
        }

        if (isPlayerDetected) {
            if (isPlayerDetected.distance > 1) {
                rb.velocity = new Vector2(1.5f * moveSpeed * facingDirection, rb.velocity.y);
                Debug.Log("I see a player!");
                isAttacking = false;
            }
            else {
                Debug.Log("Attack!");
                isAttacking = true;
            }
        }
    }

    private void Movement() {
        if (!isAttacking) {
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
        }
    }

    protected override void CollisionChecks() {
        base.CollisionChecks();

        isPlayerDetected = Physics2D.Raycast(playerCheck.position, Vector2.right, playerCheckDistance * facingDirection,
            whatIsPlayer);
    }

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(playerCheck.position,
            new Vector3(playerCheck.position.x + playerCheckDistance * facingDirection, playerCheck.position.y,
                playerCheck.position.z));
    }

}
