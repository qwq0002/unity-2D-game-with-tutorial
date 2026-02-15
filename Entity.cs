using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsWall;

    protected bool isWallDetected;
    protected bool isGrounded;

    [Header("Facing Info")]
    [SerializeField] protected int facingDirection = 1;
    [SerializeField] protected bool facingRight = true;

    // Start is called before the first frame update
    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (groundCheck == null) {
            groundCheck = transform;
        }

        if (wallCheck == null) {
            wallCheck = transform;
        }
    }

    // Update is called once per frame
    protected virtual void Update() {
        CollisionChecks();
        FlipController();
    }

    protected virtual void CollisionChecks() {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDirection,
            whatIsWall);
    }

    protected void Flip() {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    protected void FlipController() {
        if (rb.velocity.x > 0 && !facingRight) {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight) {
            Flip();
        }
    }

    protected virtual void OnDrawGizmos() {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + facingDirection * wallCheckDistance, wallCheck.position.y));
    }

}
