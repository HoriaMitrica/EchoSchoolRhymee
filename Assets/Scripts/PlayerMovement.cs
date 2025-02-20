using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isGrounded;

    private static readonly int isMoving = Animator.StringToHash("isMoving");
    private static readonly int isJumping = Animator.StringToHash("isJumping");

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.freezeRotation = true;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _animator.SetBool(isJumping, false);
    }

    void Update()
    {
        // Handle movement input
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Jump input handled here to prevent missed inputs
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }

        // Update animator based on movement
        _animator.SetBool(isMoving, _horizontal != 0);
    }

    void FixedUpdate()
    {
        // Apply movement
        _rigidbody2D.linearVelocity = new Vector2(_horizontal * Speed, _rigidbody2D.linearVelocity.y);

        // Flip sprite direction based on movement
        if (_horizontal > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_horizontal < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    void Jump()
    {
        _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpForce);
        _isGrounded = false;
        _animator.SetBool(isJumping, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _animator.SetBool(isJumping, false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
