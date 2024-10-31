using System.IO;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float xInput;

    [Header("Collisions")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    private bool isFacingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        AnimationControllers();
        FlipController();

        CollisionChecks();

        xInput = Input.GetAxisRaw("Horizontal");
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    
    }

    private void AnimationControllers()
    {
        animator.SetFloat("xVelocity", rb.linearVelocity.x);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        if (isGrounded)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void FlipController() 
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x  < transform.position.x && isFacingRight)
            Flip();
        else if (mousePos.x > transform.position.x && !isFacingRight)
            Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
