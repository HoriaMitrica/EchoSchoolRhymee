using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isGrounded = true;
    
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
        Debug.Log("isground "+ _isGrounded);
        // Get movement input
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Flip sprite based on movement direction
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }

        // Update Animator
        _animator.SetBool(isMoving, _horizontal != 0);
        _animator.SetBool(isJumping, !_isGrounded);
    }

    void FixedUpdate()
    {
        // Apply movement
        _rigidbody2D.linearVelocity = new Vector2(_horizontal * Speed, _rigidbody2D.linearVelocity.y);
    }

    void Jump()
    {
        _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpForce);
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Here");
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